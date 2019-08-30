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

        public Object Drop(List<String> atributos, AST_CQL arbol, int fila, int columna) {
            List<ColumnCQL> columnasEliminar = new List<ColumnCQL>();

            foreach (String idColumna in atributos)
            {
                foreach (ColumnCQL column in data) {
                    if (column.id.Equals(idColumna)) {
                        if (column.primaryKey) {
                            arbol.addError("EXCEPTION.ColumnException: " + column.id,"No se puede eliminar una columna PrimaryKey",fila,columna);
                            return Catch.EXCEPTION.ColumnException;
                        }
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

        public Object updateValues(List<AsignacionColumna> asignaciones, Where where, AST_CQL arbol, int fila, int col) {
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
                    arbol.entorno.reasignarVariable("$" + column.id, column.valores[i], column.tipoDato, arbol,fila,col);
                }

                //creo una tupla resultado si se cumple el where
                if (where == null || Convert.ToBoolean(where.getValor(arbol)))
                {
                    foreach (ColumnCQL column in data)
                    {
                        foreach (AsignacionColumna asc in asignaciones) {
                            if (asc.idColumna.Equals(column.id)) {
                                //pregunto si es un acceso
                                if (asc.acceso != null) {
                                    if (column.tipoDato is TipoMAP)
                                    {
                                        ((MapCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((MapCQL)column.valores[i]).expresiones.Add(asc.acceso);
                                        ((MapCQL)column.valores[i]).expresiones.Add(asc.expresion);
                                        ((MapCQL)column.valores[i]).set(arbol);
                                    }
                                    else if (column.tipoDato is TipoSet) {
                                        ((SetCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((SetCQL)column.valores[i]).expresiones.Add(asc.acceso);
                                        ((SetCQL)column.valores[i]).expresiones.Add(asc.expresion);
                                        ((SetCQL)column.valores[i]).set(arbol);
                                    }
                                    else if (column.tipoDato is TipoList)
                                    {
                                        ((ListCQL)column.valores[i]).expresiones = new List<Expresion>();
                                        ((ListCQL)column.valores[i]).expresiones.Add(asc.acceso);
                                        ((ListCQL)column.valores[i]).expresiones.Add(asc.expresion);
                                        ((ListCQL)column.valores[i]).set(arbol);
                                    }
                                    else {
                                        arbol.addError("EXCEPTION.ValuesException", "La columna: " + asc.idColumna + " no es de tipo map para hacer un acceso", fila, col);
                                        arbol.entorno = arbol.entorno.padre;
                                        return Catch.EXCEPTION.ValuesException;
                                    }
                                    break;
                                }
                                else
                                {
                                    column.valores[i] = asc.expresion.getValor(arbol);
                                    break;
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

        public Object deleteFrom(Where where, AST_CQL arbol, int fila, int col) {
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
                        arbol.entorno.reasignarVariable("$" + column.id, column.valores[i], column.tipoDato, arbol,fila,col);
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
                                arbol.addError("EXCEPTION.CounterTypeException: " + this.id, "No puede asignar valor a la columna: "+columna.id+" ya que es de tipo Counter",fila,col);
                                return Catch.EXCEPTION.CounterTypeException;
                            }
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        arbol.addError("EXCEPTION.ColumnException", "No existe la columna con nombre: " + idColumna + " para el insert en tabla: " + this.id,fila,col);
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
                            columna.valores.Add(columna.counter++);
                        }
                        else
                        {
                            columna.valores.Add(Primitivo.TIPO_DATO.NULL);
                        }
                    }
                }
            }
            else
            {
                if (contieneColumnaCounter()) {
                    arbol.addError("EXCEPTION.CounterTypeException: " + this.id, "La tabla contiene una columna de tipo Counter, debe usar la insercion especial",fila,col);
                    return Catch.EXCEPTION.CounterTypeException;
                }

                if (this.data.Count != values.Count)
                {
                    arbol.addError("EXCEPTION.ValuesException: " + this.id, "No existe la misma cantidad de valores asignados a las columnas",fila,col);
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

        bool contieneColumnaCounter() {
            foreach (ColumnCQL column in data) {
                if (!column.tipoDato.Equals(Primitivo.TIPO_DATO.COUNTER)) {
                    return true;
                }
            }
            return false;
        }
    }
}