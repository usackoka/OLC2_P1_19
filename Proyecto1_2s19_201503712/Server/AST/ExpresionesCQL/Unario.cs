using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Unario : Expresion
    {
        Expresion unario;
        String operador;

        public Unario(String operador, Expresion unario, int fila, int columna) {
            this.unario = unario;
            this.operador = operador;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return unario.getTipo(arbol);
        }

        public override object getValor(AST_CQL arbol)
        {
            Object tipo = unario.getTipo(arbol);
            switch (operador) {
                case "-":
                    if (tipo.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return - Convert.ToInt32(unario.getValor(arbol));
                    }
                    else if (tipo.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return - Convert.ToDouble(unario.getValor(arbol));
                    }
                    else
                    {
                        arbol.addError("", "(Unario, -, no soportado: " + tipo + ")", fila, columna);
                        return 0;
                    }
                case "+":
                    return unario.getValor(arbol);
                case "!":
                    if (tipo.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                    {
                        return !Convert.ToBoolean(unario.getValor(arbol));
                    }
                    else {
                        arbol.addError("", "(Unario, !, no soportado: " + tipo + ")", fila, columna);
                        return 0;
                    }
                default:
                    arbol.addError("","(Unario, no soportado: "+operador+")",fila,columna);
                    return 0;
            }
        }
    }
}