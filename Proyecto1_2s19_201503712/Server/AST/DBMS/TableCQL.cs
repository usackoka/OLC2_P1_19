using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;

namespace Server.AST.DBMS
{
    public class TableCQL
    {
        public String id;
        List<ColumnCQL> data;

        public TableCQL(String id, List<ColumnCQL> columnDefinitions)
        {
            this.id = id;
            this.data = new List<ColumnCQL>();
            iniciarData(columnDefinitions);
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

        public Object insertValues(List<String> columnNames, List<Expresion> values, AST_CQL arbol)
        {
            if (columnNames != null)
            {
                //pregunto si existen todos los nombres de las columnas
                Boolean existe = false;
                foreach (String idColumna in columnNames)
                {
                    foreach (ColumnCQL columna in data)
                    {
                        //encuentra la columna
                        if (columna.id.Equals(idColumna))
                        {
                            continue;
                        }
                    }
                    if (!existe)
                    {
                        arbol.addError("ColumnName", "No existe la columna con nombre: " + idColumna + " para el insert en tabla: " + this.id, 0, 0);
                        return null;
                    }
                }

                int indexValor = 0;
                //hasta este punto ya pregunté por la cantidad de parámetros y y si existen los nombres
                foreach (String idColumna in columnNames)
                {
                    int indiceTupla = 0;
                    foreach (ColumnCQL columna in data)
                    {
                        //encuentra la columna
                        if (columna.id.Equals(idColumna))
                        {
                            columna.valores.Add(values[indexValor++].getValor(arbol));
                            indiceTupla = columna.valores.Count;
                        }
                    }

                    //lleno los campos que quedaron vacíos
                    foreach (ColumnCQL columna in data) {
                        if (columna.valores.Count < indiceTupla) {
                            columna.valores.Add(Primitivo.TIPO_DATO.NULL);
                        }
                    }
                }
            }
            else
            {
                if (this.data.Count != values.Count)
                {
                    arbol.addError("Insert: " + this.id, "No existe la misma cantidad de valores asignados a las columnas", 0, 0);
                    return null;
                }

                int indexValor = 0;
                foreach (ColumnCQL columna in data)
                {
                    columna.valores.Add(values[indexValor++].getValor(arbol));
                }
            }

            return null;
        }
    }
}