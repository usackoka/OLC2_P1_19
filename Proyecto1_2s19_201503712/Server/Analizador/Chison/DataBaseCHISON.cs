using Server.AST;
using Server.AST.CQL;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;

namespace Server.Analizador.Chison
{
    public class DataBaseCHISON
    {
        String name;
        List<Data_Base_CHISON> data;
        int fila, columna;

        public DataBaseCHISON(String name, List<Data_Base_CHISON> data, int fila, int columna)
        {
            this.name = name;
            this.data = data;
            this.fila = fila;
            this.columna = columna;
        }

        public object Ejecutar(Management dbms)
        {
            //creo la base de datos
            dbms.createDataBase(this.name, fila, columna);

            //creo los userTypes
            foreach (Data_Base_CHISON contenido in this.data)
            {
                String cql_type = contenido.getCqlType(dbms);

                if (compararNombre(cql_type, "object"))
                {
                    //es por que es un userType
                    String name = contenido.getName(dbms);
                    List<List<KeyValuePair<String, String>>> attrs = contenido.getAttrs(dbms);

                    //creo los atributos
                    List<KeyValuePair<String, Object>> atributos = new List<KeyValuePair<string, object>>();
                    foreach (List<KeyValuePair<String, String>> lista in attrs)
                    {
                        String nombre = "NULL";
                        Object tipo = new Null();
                        foreach (KeyValuePair<String, String> kvp in lista)
                        {
                            if (kvp.Key.Equals("name"))
                            {
                                nombre = kvp.Value;
                            }
                            else
                            {
                                tipo = Primitivo.getTipoString(kvp.Value, dbms);
                            }
                        }
                        atributos.Add(new KeyValuePair<String, Object>(nombre, tipo));
                    }

                    dbms.getDataBase(this.name).userTypes.Add(new UserType(name, atributos));
                }
            }

            dbms.system = dbms.getDataBase(this.name);
            //creo toda la data de la base de datos
            foreach (Data_Base_CHISON contenido in this.data)
            {
                String cql_type = contenido.getCqlType(dbms);

                if (compararNombre(cql_type, "table"))
                {
                    String name = contenido.getName(dbms);
                    List<ColumnCQL> columns = contenido.getColumnsDefinitions(dbms);
                    List<List<KeyValuePair<String, Object>>> values = contenido.getData(dbms);
                    TableCQL tabla = new TableCQL(name, columns);

                    foreach (ColumnCQL column in tabla.data)
                    {
                        List<Object> valoresColumna = new List<object>();

                        foreach (List<KeyValuePair<String, Object>> lista in values)
                        {
                            foreach (KeyValuePair<string, object> kvp in lista)
                            {
                                if (kvp.Key.Equals(column.id))
                                {
                                    Object valor = kvp.Value;
                                    valor = Primitivo.getObjectByList(valor, column.tipoDato, dbms);
                                    valoresColumna.Add(valor);
                                }
                            }
                        }
                        column.valores = valoresColumna;
                    }
                    dbms.getDataBase(this.name).tables.Add(tabla);
                }
                else if (compararNombre(cql_type, "procedure"))
                {
                    String name = contenido.getName(dbms);
                    List<Parametro> pars = contenido.getParameters(dbms);
                    String instrucciones = contenido.getInstrucciones(dbms);

                    //mando a jalar la lista de nodos
                    List<NodoCQL> instr = getInstrucciones(instrucciones,dbms);

                    List<KeyValuePair<String, Object>> parametros = new List<KeyValuePair<string, object>>();
                    List<KeyValuePair<String, Object>> retornos = new List<KeyValuePair<string, object>>();

                    //creo los parametros y los retornos
                    foreach (Parametro parametro in pars)
                    {
                        KeyValuePair<String, Object> atributo = new KeyValuePair<string, object>(parametro.getName(dbms), parametro.getCqlType(dbms));
                        if (parametro.getAS(dbms))
                        {
                            parametros.Add(atributo);
                        }
                        else
                        {
                            retornos.Add(atributo);
                        }
                    }

                    dbms.getDataBase(this.name).procedures.Add(new Procedure(name, parametros, retornos, instr, instrucciones, 0, 0));

                }
            }
            dbms.system = null;

            return null;
        }

        List<NodoCQL> getInstrucciones(String cadena, Management dbms)
        {
            Generador parserCQL = new Generador();
            if (parserCQL.esCadenaValida(cadena, new GramaticaCQL()))
            {
                if (parserCQL.padre.Root != null)
                {
                    //Graficar.ConstruirArbol(parserCQL.padre.Root, "AST_CQL", "");
                    RecorridoCQL recorrido = new RecorridoCQL(parserCQL.padre.Root, dbms);
                    return recorrido.RecorridoDesdeChison();
                }
            }
            dbms.addError("Error-LoadChison","Error al recorrer las instrucciones del procedure",0,0);
            return new List<NodoCQL>();
        }

        Boolean compararNombre(String n1, String n2)
        {
            return n1.Equals(n2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}