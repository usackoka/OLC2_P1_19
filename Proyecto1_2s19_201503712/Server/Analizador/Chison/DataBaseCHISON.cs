﻿using Server.AST.CQL;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
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

                if (compararNombre(cql_type, "object")) {
                    //es por que es un userType

                }
            }

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
            }

            return null;
        }

        Boolean compararNombre(String n1, String n2)
        {
            return n1.Equals(n2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}