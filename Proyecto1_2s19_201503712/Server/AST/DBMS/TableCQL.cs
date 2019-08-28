using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
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

        public Object Drop(List<String> atributos) {
            List<ColumnCQL> columnasEliminar = new List<ColumnCQL>();

            foreach (String idColumna in atributos)
            {
                foreach (ColumnCQL column in data) {
                    if (column.id.Equals(idColumna)) {
                        columnasEliminar.Add(column);
                    }
                }
            }

            foreach (ColumnCQL column in columnasEliminar) {
                data.Remove(column);
            }

            return null;
        }

        public Object Add(List<ColumnCQL> atributos) {

            int indiceTupla = this.data.Count != 0 ? this.data[0].valores.Count : 0;

            foreach (ColumnCQL kvp in atributos) {
                //la lleno de valores nulos para las tuplas que ya estén ingresadas
                for (int i = 0; i < indiceTupla; i++)
                {
                    kvp.valores.Add(Primitivo.TIPO_DATO.NULL);
                }
                this.data.Add(kvp);
            }

            return null;
        }

        public Object updateValues(List<AsignacionColumna> asignaciones, Where where, AST_CQL arbol) {
            //creo un entorno para estas nuevas variables que llevarán el control del select
            arbol.entorno = new Entorno(arbol.entorno);

            //creo las variables iteradoras del select
            foreach (ColumnCQL column in data)
            {
                arbol.entorno.addVariable("$" + column.id, new Variable(Primitivo.getDefecto(column.tipoDato, arbol),
                    column.tipoDato));
            }

            //=========================== LLENAR LA TABLA DE RESULTADOS ===============================
            int indiceFor = data.Count != 0 ? data[0].valores.Count : 0; //Número de tuplas
            
            for (int i = 0; i < indiceFor; i++)
            {
                //doy valores a las variables de las columnas
                foreach (ColumnCQL column in data)
                {
                    arbol.entorno.reasignarVariable("$" + column.id, column.valores[i], column.tipoDato, arbol, 0, 0);
                }

                //creo una tupla resultado si se cumple el where
                if (where == null || Convert.ToBoolean(where.getValor(arbol)))
                {
                    foreach (ColumnCQL column in data)
                    {
                        foreach (AsignacionColumna asc in asignaciones) {
                            if (asc.idColumna.Equals(column.id)) {
                                column.valores[i] = asc.expresion.getValor(arbol);
                                break;
                            }
                        }
                    }
                }
            }

            //regreso el entorno
            arbol.entorno = arbol.entorno.padre;

            return null;
        }

        public Object deleteFrom(Where where, AST_CQL arbol) {
            if (where == null)
            {
                restartTable();
            }
            //================== hago el mismo for de los select para ver que eliminar
            else {
                //creo un entorno para estas nuevas variables que llevarán el control del select
                arbol.entorno = new Entorno(arbol.entorno);

                //creo las variables iteradoras del select
                foreach (ColumnCQL column in data)
                {
                    arbol.entorno.addVariable("$" + column.id, new Variable(Primitivo.getDefecto(column.tipoDato, arbol),
                        column.tipoDato));
                }

                //=========================== LLENAR LA TABLA DE RESULTADOS ===============================
                int indiceFor = data.Count != 0 ? data[0].valores.Count : 0; //Número de tuplas

                List<int> indicesDelete = new List<int>();
                for (int i = 0; i < indiceFor; i++)
                {
                    //doy valores a las variables de las columnas
                    foreach (ColumnCQL column in data)
                    {
                        arbol.entorno.reasignarVariable("$" + column.id, column.valores[i], column.tipoDato, arbol, 0, 0);
                    }

                    //creo una tupla resultado si se cumple el where
                    if (where == null || Convert.ToBoolean(where.getValor(arbol)))
                    {
                        indicesDelete.Add(i-indicesDelete.Count);
                    }
                }

                //elimino
                foreach (int index in indicesDelete) {
                    foreach (ColumnCQL column in data) {
                        column.valores.RemoveAt(index);
                    }
                }

                //regreso el entorno
                arbol.entorno = arbol.entorno.padre;
            }

            return null;
        }

        public Object insertValues(List<String> columnNames, List<Expresion> values, AST_CQL arbol)
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
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        arbol.addError("ColumnName", "No existe la columna con nombre: " + idColumna + " para el insert en tabla: " + this.id, 0, 0);
                        return Catch.EXCEPTION.ColumnException;
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
                        if (columna.id.Equals(idColumna))
                        {
                            columna.valores.Add(values[indexValor++].getValor(arbol));
                            indiceTupla = columna.valores.Count;
                        }
                    }
                }

                //lleno los campos que quedaron vacíos
                foreach (ColumnCQL columna in data)
                {
                    if (columna.valores.Count < indiceTupla)
                    {
                        columna.valores.Add(Primitivo.TIPO_DATO.NULL);
                    }
                }
            }
            else
            {
                if (this.data.Count != values.Count)
                {
                    arbol.addError("Insert: " + this.id, "No existe la misma cantidad de valores asignados a las columnas", 0, 0);
                    return Catch.EXCEPTION.ValuesException;
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