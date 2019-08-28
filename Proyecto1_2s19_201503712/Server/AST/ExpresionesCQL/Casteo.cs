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
            switch (tipoDato) {
                case Primitivo.TIPO_DATO.BOOLEAN:
                    return Convert.ToBoolean(expresion.getValor(arbol));
                case Primitivo.TIPO_DATO.STRING:
                    return expresion.getValor(arbol).ToString();
                case Primitivo.TIPO_DATO.DATE:
                case Primitivo.TIPO_DATO.TIME:
                    try
                    {
                        return DateTime.Parse(expresion.getValor(arbol).ToString());
                    }
                    catch (Exception ex) {
                        arbol.addError("Casteo-DATETIME","No se puede castear el: "+expresion.getValor(arbol).ToString()+" a DateTime", fila, columna);
                        return Catch.EXCEPTION.NullPointerException;
                    }
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