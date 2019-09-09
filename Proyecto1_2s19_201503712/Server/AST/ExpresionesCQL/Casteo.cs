using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Casteo : Expresion
    {
        Object tipoDato;
        Expresion expresion;

        public Casteo(Object tipoDato, Expresion expresion, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return this.tipoDato;
        }

        public override object getValor(AST_CQL arbol)
        {
            if (tipoDato is Date) {
                return new Date(expresion, arbol, fila, columna);
            } else if (tipoDato is TimeSpan) {
                Object value = expresion.getValor(arbol);
                String[] arr = value.ToString().Split(':');
                if (arr.Length == 3)
                {
                    return new TimeSpan(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]),
                        Convert.ToInt32(arr[2]));
                }
                else
                {
                    arbol.addError("Time","No se puede castear de tipo: "+expresion.getTipo(arbol)+" a Time",fila,columna);
                    return new TimeSpan();
                }
            }

            switch (tipoDato) {
                case Primitivo.TIPO_DATO.BOOLEAN:
                    return Convert.ToBoolean(expresion.getValor(arbol));
                case Primitivo.TIPO_DATO.DATE:
                    return new Date(expresion.getValor(arbol).ToString());
                case Primitivo.TIPO_DATO.TIME:
                    String[] arr = this.expresion.getValor(arbol).ToString().Split(':');
                    if (arr.Length == 3)
                    {
                        return new TimeSpan(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[2]));
                    }
                    else
                    {
                        return new TimeSpan();
                    }
                case Primitivo.TIPO_DATO.STRING:
                    return expresion.getValor(arbol).ToString();
                case Primitivo.TIPO_DATO.DOUBLE:
                    return Convert.ToDouble(expresion.getValor(arbol));
                case Primitivo.TIPO_DATO.INT:
                    return Convert.ToInt32(expresion.getValor(arbol));
                default:
                    arbol.addError("Casteo-"+tipoDato,"No existe un casteo explicito para este tipo",fila,columna);
                    return Catch.EXCEPTION.NullPointerException;
            }
        }
    }
}