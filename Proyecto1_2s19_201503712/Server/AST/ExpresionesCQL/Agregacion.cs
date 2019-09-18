using Server.AST.CQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Agregacion : Expresion
    {
        Select select;
        TIPO_AGR tipoAgr;

        public Agregacion(TIPO_AGR tipoAgr, Select select, int fila, int columna) {
            this.fila = fila;
            this.columna = columna;
            this.tipoAgr = tipoAgr;
            this.select = select;
        }

        public enum TIPO_AGR {
            COUNT, MIN, MAX, SUM, AVG
        }

        public override object getTipo(AST_CQL arbol)
        {
            if (this.tipoAgr.Equals(TIPO_AGR.AVG)) {
                return Primitivo.TIPO_DATO.DOUBLE;

            }
            return Primitivo.TIPO_DATO.INT;
        }

        public override object getValor(AST_CQL arbol)
        {
            //realizo el select
            Object o = select.Ejecutar(arbol);
            List<ColumnCQL> data;
            if (o is List<ColumnCQL>)
            {
                data = (List<ColumnCQL>)o;
            }
            else {
                return o;
            }

            arbol.result_consultas.RemoveAt(arbol.result_consultas.Count-1);

            switch (this.tipoAgr)
            {
                case TIPO_AGR.COUNT:
                    return data.Count != 0 ? data[0].valores.Count : 0;
                case TIPO_AGR.MIN:
                    if (data.Count!=1) {
                        arbol.addError("Agregacion MIN","Solo se debió seleccionar un campo de la tabla",fila,columna);
                        return Catch.EXCEPTION.ValuesException;
                    }
                    data[0].valores.Sort();
                    return data[0].valores[0];
                case TIPO_AGR.MAX:
                    if (data.Count != 1)
                    {
                        arbol.addError("Agregacion MAX", "Solo se debió seleccionar un campo de la tabla", fila, columna);
                        return Catch.EXCEPTION.ValuesException;
                    }
                    data[0].valores.Sort();
                    return data[0].valores[data[0].valores.Count-1];
                case TIPO_AGR.SUM:
                    if (data.Count != 1)
                    {
                        arbol.addError("Agregacion SUM", "Solo se debió seleccionar un campo de la tabla", fila, columna);
                        return Catch.EXCEPTION.ValuesException;
                    }

                    if (!(data[0].tipoDato.Equals(Primitivo.TIPO_DATO.INT) || data[0].tipoDato.Equals(Primitivo.TIPO_DATO.DOUBLE))) {
                        arbol.addError("Agregacion SUM","El campo seleccionado debía ser de tipo Numerico",fila,columna);
                        return Catch.EXCEPTION.ValuesException;
                    }

                    int sumaInt = 0;
                    double sumaDouble = 0;
                    foreach (Object dato in data[0].valores) {
                        if (data[0].tipoDato.Equals(Primitivo.TIPO_DATO.INT))
                        {
                            sumaInt += Convert.ToInt32(dato);
                        }
                        else {
                            sumaDouble += Convert.ToDouble(dato);
                        }
                    }

                    if (sumaInt == 0)
                    {
                        return sumaDouble;
                    }
                    else {
                        return sumaInt;
                    }

                default://avg
                    if (data.Count != 1)
                    {
                        arbol.addError("Agregacion AVG", "Solo se debió seleccionar un campo de la tabla", fila, columna);
                        return Catch.EXCEPTION.ValuesException;
                    }

                    if (!(data[0].tipoDato.Equals(Primitivo.TIPO_DATO.INT) || data[0].tipoDato.Equals(Primitivo.TIPO_DATO.DOUBLE)))
                    {
                        arbol.addError("Agregacion SUM", "El campo seleccionado debía ser de tipo Numerico", fila, columna);
                        return Catch.EXCEPTION.ValuesException;
                    }

                    sumaInt = 0;
                    sumaDouble = 0;
                    foreach (Object dato in data[0].valores)
                    {
                        if (data[0].tipoDato.Equals(Primitivo.TIPO_DATO.INT))
                        {
                            sumaInt += Convert.ToInt32(dato);
                        }
                        else
                        {
                            sumaDouble += Convert.ToDouble(dato);
                        }
                    }

                    if (sumaInt == 0)
                    {
                        return sumaDouble/data[0].valores.Count;
                    }
                    else
                    {
                        return sumaInt/data[0].valores.Count;
                    }
            }
        }
    }
}