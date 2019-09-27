using Server.AST.ColeccionesCQL;
using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;

namespace Server.AST.DBMS
{
    public class TableCQL
    {
        public String id;
        public List<ColumnCQL> data;

        public TableCQL(String id, List<ColumnCQL> columnDefinitions)
        {
            this.id = id;
            this.data = new List<ColumnCQL>();
            iniciarData(columnDefinitions);
        }

        public override string ToString()
        {
            String trad = "";

            trad += "   <\n";
            trad += "   \"CQL-TYPE\" = \"TABLE\",\n";
            trad += "   \"NAME\" = \""+this.id+"\",\n";
            trad += "   \"COLUMNS\" = ["+getColumnas()+"],\n";
            trad += "   \"DATA\" = [" + getData() + "]\n";
            trad += "   >\n";

            return trad;
        }

        public string getColumnas() {
            String trad = "";
            foreach (ColumnCQL column in this.data) {
                trad += "\n"+column + ",";
            }
            trad = trad.TrimEnd(',');
            return trad;
        }

        public string getData() {
            String trad = "";
            int index = this.data.Count > 0 ? this.data[0].valores.Count : 0; //index de tuplas

            for (int i = 0; i < index; i++)
            {
                trad += "\n       <";
                foreach (ColumnCQL column in this.data)
                {
                    trad += "\n"+column.getData(i) + ",";
                }
                trad = trad.TrimEnd(',');
                trad += "\n"+"       >,";
            }
            trad = trad.TrimEnd(',');
            return trad;
        }

        void iniciarData(List<ColumnCQL> columns)
        {
            foreach (ColumnCQL column in columns)
            {
                this.data.Add(new ColumnCQL(column));
            }
        }

        public void restartTable()
        {
            foreach (ColumnCQL column in data)
            {
                column.valores = new List<object>();
            }
        }

        public Object Drop(List<String> atributos, AST_CQL arbol, int fila, int columna)
        {
            List<ColumnCQL> columnasEliminar = new List<ColumnCQL>();

            foreach (String idColumna in atributos)
            {
                foreach (ColumnCQL column in data)
                {
                    if (column.id.Equals(idColumna))
                    {
                        if (column.primaryKey)
                        {
                            arbol.addError("EXCEPTION.ColumnException: " + column.id, "No se puede eliminar una columna PrimaryKey", fila, columna);
                            return new ExceptionCQL(ExceptionCQL.EXCEPTION.ColumnException, "Columna: "+column.id + " No se puede eliminar una columna PrimaryKey", fila, columna);
                        }
                        columnasEliminar.Add(column);
                    }
                }
            }

            foreach (ColumnCQL column in columnasEliminar)
            {
                data.Remove(column);
            }

            return null;
        }

        public Object Add(List<ColumnCQL> atributos)
        {

            int indiceTupla = this.data.Count != 0 ? this.data[0].valores.Count : 0;

            foreach (ColumnCQL kvp in atributos)
            {
                //la lleno de valores nulos para las tuplas que ya estén ingresadas
                for (int i = 0; i < indiceTupla; i++)
                {
                    kvp.valores.Add(new Null());
                }
                this.data.Add(kvp);
            }

            return null;
        }

        public Object updateValues(List<AsignacionColumna> asignaciones, Where where, AST_CQL arbol, int fila, int col)
        {
            //creo un entorno para estas nuevas variables que llevarán el control del select
            arbol.entorno = new Entorno(arbol.entorno);

            //creo las variables iteradoras del select
            foreach (ColumnCQL column in data)
            {
                arbol.entorno.addVariable("$" + column.id, new Variable(Primitivo.getDefecto(column.tipoDato, arbol),
                    column.tipoDato), arbol,0,0);
            }

            //=========================== LLENAR LA TABLA DE RESULTADOS ===============================
            int indiceFor = data.Count != 0 ? data[0].valores.Count : 0; //Número de tuplas

            for (int i = 0; i < indiceFor; i++)
            {
                //doy valores a las variables de las columnas
                foreach (ColumnCQL column in data)
                {
                    arbol.entorno.reasignarVariable("$" + column.id, column.valores[i], column.tipoDato, arbol, fila, col);
                }

                //creo una tupla resultado si se cumple el where
                Boolean vale;
                if (where == null)
                {
                    vale = true;
                }
                else{
                    Object ret = where.getValor(arbol);
                    if (ret is Boolean)
                    {
                        vale = Convert.ToBoolean(ret);
                    }
                    else {
                        vale = false;
                    }
                }

                if (vale)
                {
                    foreach (ColumnCQL column in data)
                    {
                        foreach (AsignacionColumna asc in asignaciones)
                        {
                            if (asc.idColumna.Equals(column.id, StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (asc.acceso != null)
                                {
                                    if (column.valores[i] is ListCQL)
                                    {
                                        ((ListCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((ListCQL)column.valores[i]).expresiones.Add(asc.acceso.expresion);
                                        ((ListCQL)column.valores[i]).expresiones.Add(asc.expresion);
                                        ((ListCQL)column.valores[i]).set(arbol);
                                    }
                                    else if (column.valores[i] is SetCQL)
                                    {
                                        ((SetCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((SetCQL)column.valores[i]).expresiones.Add(asc.acceso.expresion);
                                        ((SetCQL)column.valores[i]).expresiones.Add(asc.expresion);
                                        ((SetCQL)column.valores[i]).set(arbol);
                                    }
                                    else if (column.valores[i] is MapCQL)
                                    {
                                        ((MapCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((MapCQL)column.valores[i]).expresiones.Add(asc.acceso.expresion);
                                        ((MapCQL)column.valores[i]).expresiones.Add(asc.expresion);
                                        ((MapCQL)column.valores[i]).set(arbol);
                                    }
                                    else
                                    {
                                        arbol.entorno = arbol.entorno.padre;
                                        arbol.addError("EXCEPTION.NullPointerException", "No se puede hacer un acceso a una columna de tipo: " + column.tipoDato, fila, col);
                                        return new ExceptionCQL(ExceptionCQL.EXCEPTION.NullPointerException, "No se puede hacer un acceso a una columna de tipo: " + column.tipoDato,fila,col);
                                    }
                                }
                                else if (asc.referencia != null) {
                                    asc.referencia.valor = asc.expresion;
                                    asc.referencia.getValor(arbol);
                                }
                                else
                                {
                                    column.valores[i] = asc.expresion.getValor(arbol);
                                }
                            }
                        }
                    }
                }
            }

            //regreso el entorno
            arbol.entorno = arbol.entorno.padre;

            return null;
        }

        public Object deleteFrom(AccesoArreglo acceso, Where where, AST_CQL arbol, int fila, int col)
        {
            if (where == null && acceso == null)
            {
                restartTable();
            }
            //================== hago el mismo for de los select para ver que eliminar
            else
            {
                //creo un entorno para estas nuevas variables que llevarán el control del select
                arbol.entorno = new Entorno(arbol.entorno);

                //creo las variables iteradoras del select
                foreach (ColumnCQL column in data)
                {
                    arbol.entorno.addVariable("$" + column.id, new Variable(Primitivo.getDefecto(column.tipoDato, arbol),
                        column.tipoDato),arbol,0,0);
                }

                //=========================== LLENAR LA TABLA DE RESULTADOS ===============================
                int indiceFor = data.Count != 0 ? data[0].valores.Count : 0; //Número de tuplas

                List<int> indicesDelete = new List<int>();
                for (int i = 0; i < indiceFor; i++)
                {
                    //doy valores a las variables de las columnas
                    foreach (ColumnCQL column in data)
                    {
                        arbol.entorno.reasignarVariable("$" + column.id, column.valores[i], column.tipoDato, arbol, fila, col);
                    }

                    //creo una tupla resultado si se cumple el where
                    Boolean vale;
                    if (where == null)
                    {
                        vale = true;
                    }
                    else
                    {
                        Object ret = where.getValor(arbol);
                        if (ret is Boolean)
                        {
                            vale = Convert.ToBoolean(ret);
                        }
                        else
                        {
                            vale = false;
                        }
                    }

                    if (vale)
                    {
                        foreach (ColumnCQL column in data)
                        {
                            if (acceso != null)
                            {
                                if (acceso.id.Equals(column.id, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (acceso.expresion == null)
                                    {
                                        column.setNulls();
                                    }
                                    else if (column.valores[i] is ListCQL)
                                    {
                                        ((ListCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((ListCQL)column.valores[i]).expresiones.Add(acceso.expresion);
                                        ((ListCQL)column.valores[i]).remove(arbol);
                                    }
                                    else if (column.valores[i] is SetCQL)
                                    {
                                        ((SetCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((SetCQL)column.valores[i]).expresiones.Add(acceso.expresion);
                                        ((SetCQL)column.valores[i]).remove(arbol);
                                    }
                                    else if (column.valores[i] is MapCQL)
                                    {
                                        ((MapCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((MapCQL)column.valores[i]).expresiones.Add(acceso.expresion);
                                        ((MapCQL)column.valores[i]).remove(arbol);
                                    }
                                    else
                                    {
                                        arbol.entorno = arbol.entorno.padre;
                                        arbol.addError("EXCEPTION.NullPointerException", "No se puede hacer un acceso a una columna de tipo: " + column.tipoDato, fila, col);
                                        return new ExceptionCQL(ExceptionCQL.EXCEPTION.NullPointerException, "No se puede hacer un acceso a una columna de tipo: " + column.tipoDato,fila,col);
                                    }
                                }
                            }
                            else
                            {
                                indicesDelete.Add(i - indicesDelete.Count);
                                break;
                            }
                        }
                    }
                }

                //elimino
                foreach (int index in indicesDelete)
                {
                    foreach (ColumnCQL column in data)
                    {
                        column.valores.RemoveAt(index);
                    }
                }

                //regreso el entorno
                arbol.entorno = arbol.entorno.padre;
            }

            return null;
        }

        public Object insertValues(List<String> columnNames, List<Expresion> values, AST_CQL arbol, int fila, int col)
        {
            if (columnNames != null)
            {
                //pregunto si existen todos los nombres de las columnas
                Boolean existe = false;
                foreach (String idColumna in columnNames)
                {
                    existe = false;
                    foreach (ColumnCQL columna in data)
                    {
                        //encuentra la columna
                        if (columna.id.Equals(idColumna))
                        {
                            if (columna.tipoDato.Equals(Primitivo.TIPO_DATO.COUNTER))
                            {
                                arbol.addError("EXCEPTION.CounterTypeException: " + this.id, "No puede asignar valor a la columna: " + columna.id + " ya que es de tipo Counter", fila, col);
                                return new ExceptionCQL(ExceptionCQL.EXCEPTION.CounterTypeException,"Table: "+this.id+" No puede asignar valor a la columna: " + columna.id + " ya que es de tipo Counter",fila,col);
                            }
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        arbol.addError("EXCEPTION.ColumnException", "No existe la columna con nombre: " + idColumna + " para el insert en tabla: " + this.id, fila, col);
                        return new ExceptionCQL(ExceptionCQL.EXCEPTION.ColumnException, "No existe la columna con nombre: " + idColumna + " para el insert en tabla: " + this.id,fila,col);
                    }
                }

                int indexValor = 0;
                int indiceTupla = 0;
                //hasta este punto ya pregunté por la cantidad de parámetros y y si existen los nombres
                foreach (String idColumna in columnNames)
                {
                    foreach (ColumnCQL columna in data)
                    {
                        //encuentra la columna
                        if (columna.id.Equals(idColumna, StringComparison.InvariantCultureIgnoreCase))
                        {
                            columna.valores.Add(values[indexValor++].getValor(arbol));
                            indiceTupla = columna.valores.Count;
                            break;
                        }
                    }
                }

                //lleno los campos que quedaron vacíos
                foreach (ColumnCQL columna in data)
                {
                    if (columna.valores.Count < indiceTupla)
                    {
                        if (columna.tipoDato.Equals(Primitivo.TIPO_DATO.COUNTER))
                        {
                            int cont = columna.valores.Count != 0 ? Convert.ToInt32(columna.valores[columna.valores.Count - 1]) : 0;
                            columna.valores.Add(cont+1);
                        }
                        else
                        {
                            columna.valores.Add(new Null());
                        }
                    }
                }
            }
            else
            {
                if (contieneColumnaCounter())
                {
                    arbol.addError("EXCEPTION.CounterTypeException: " + this.id, "La tabla contiene una columna de tipo Counter, debe usar la insercion especial", fila, col);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.CounterTypeException, "La tabla: "+this.id+" contiene una columna de tipo Counter, debe usar la insercion especial",fila,col);
                }

                if (this.data.Count != values.Count)
                {
                    arbol.addError("EXCEPTION.ValuesException: " + this.id, "No existe la misma cantidad de valores asignados a las columnas", fila, col);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.ValuesException, "No existe la misma cantidad de valores asignados a las columnas, en tabla: "+this.id,fila,col);
                }

                int indexValor = 0;
                foreach (ColumnCQL columna in data)
                {
                    columna.valores.Add(values[indexValor++].getValor(arbol));
                }
            }

            return null;
        }

        bool contieneColumnaCounter()
        {
            foreach (ColumnCQL column in data)
            {
                if (column.tipoDato.Equals(Primitivo.TIPO_DATO.COUNTER))
                {
                    return true;
                }
            }
            return false;
        }
    }
}