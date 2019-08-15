using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Binaria : Expresion
    {
        Expresion izquierda, derecha;
        String operador;

        public Binaria(Expresion izquierda, String operador, Expresion derecha, int fila, int columna) {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.operador = operador;
            this.columna = columna;
            this.fila = fila;
        }

        public override object getTipo(AST_CQL arbol)
        {
            Object izq = izquierda.getTipo(arbol);
            Object der = derecha.getTipo(arbol);

            switch (operador) {
                case "+":
                    if (izq.Equals(Primitivo.TIPO_DATO.STRING) || der.Equals(Primitivo.TIPO_DATO.STRING))
                    {
                        return Primitivo.TIPO_DATO.STRING;
                    }
                    else if (izq.Equals(Primitivo.TIPO_DATO.DOUBLE) || der.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Primitivo.TIPO_DATO.DOUBLE;
                    }
                    else if (izq.Equals(Primitivo.TIPO_DATO.INT) && der.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Primitivo.TIPO_DATO.INT;
                    }
                    else {
                        arbol.mensajes.Add("(Binaria, getTipo, Suma) No soportado: "+izq+" y "+der);
                        return Primitivo.TIPO_DATO.NULL;
                    }
                case "-":
                case "*":
                case "%":
                case "/":
                    if (izq.Equals(Primitivo.TIPO_DATO.DOUBLE) || der.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Primitivo.TIPO_DATO.DOUBLE;
                    }
                    else if (izq.Equals(Primitivo.TIPO_DATO.INT) && der.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Primitivo.TIPO_DATO.INT;
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getTipo, Multiplicación, Resta, Módulo, División) No soportado: " + izq + " y " + der);
                        return Primitivo.TIPO_DATO.NULL;
                    }
                case "**":
                    if (izq.Equals(Primitivo.TIPO_DATO.DOUBLE) || der.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Primitivo.TIPO_DATO.DOUBLE;
                    }
                    else if (izq.Equals(Primitivo.TIPO_DATO.INT) || der.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Primitivo.TIPO_DATO.DOUBLE;
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getTipo, potencia) No soportado: " + izq + " y " + der);
                        return Primitivo.TIPO_DATO.NULL;
                    }
                case "<=":
                case ">=":
                case ">":
                case "<":
                case "!=":
                case "==":
                case "&&":
                case "||":
                case "^":
                    return Primitivo.TIPO_DATO.BOOLEAN;
                default:
                    arbol.mensajes.Add("(Binaria, getTipo, default) No soportado: "+operador);
                    return Primitivo.TIPO_DATO.STRING;
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            Object tipIzq = izquierda.getTipo(arbol);
            Object tipDer = derecha.getTipo(arbol);

            switch (operador) {
                case "+":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.STRING) || tipDer.Equals(Primitivo.TIPO_DATO.STRING))
                    {
                        return izquierda.getValor(arbol) + "" + derecha.getValor(arbol);
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) + Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) + Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, Suma) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "-":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) - Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) - Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, Resta) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "*":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) * Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) * Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, Multiplicación) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "%":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) % Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) % Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, Modular) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "/":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) / Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) / Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, División) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "**":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Math.Pow(Convert.ToDouble(izquierda.getValor(arbol)), Convert.ToDouble(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) || tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Math.Pow(Convert.ToDouble(izquierda.getValor(arbol)), Convert.ToDouble(derecha.getValor(arbol)));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getTipo, potencia) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "<=":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) <= Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) <= Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DATE) && tipDer.Equals(Primitivo.TIPO_DATO.DATE))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) <= Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) <= Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, <=) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case ">=":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) >= Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) >= Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DATE) && tipDer.Equals(Primitivo.TIPO_DATO.DATE))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) >= Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) >= Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, >=) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case ">":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) > Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) > Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DATE) && tipDer.Equals(Primitivo.TIPO_DATO.DATE))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) > Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) > Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, >) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "<":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)) < Convert.ToDouble(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)) < Convert.ToInt32(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DATE) && tipDer.Equals(Primitivo.TIPO_DATO.DATE))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) < Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)) < Convert.ToDateTime(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, <) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "!=":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.STRING) || tipDer.Equals(Primitivo.TIPO_DATO.STRING))
                    {
                        return !izquierda.getValor(arbol).Equals(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return !Convert.ToDouble(izquierda.getValor(arbol)).Equals(Convert.ToDouble(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return !Convert.ToInt32(izquierda.getValor(arbol)).Equals(Convert.ToInt32(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DATE) && tipDer.Equals(Primitivo.TIPO_DATO.DATE))
                    {
                        return !Convert.ToDateTime(izquierda.getValor(arbol)).Equals(Convert.ToDateTime(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                    {
                        return !Convert.ToDateTime(izquierda.getValor(arbol)).Equals(Convert.ToDateTime(derecha.getValor(arbol)));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, !=) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "==":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.STRING) || tipDer.Equals(Primitivo.TIPO_DATO.STRING))
                    {
                        return izquierda.getValor(arbol).Equals(derecha.getValor(arbol));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                    {
                        return Convert.ToDouble(izquierda.getValor(arbol)).Equals(Convert.ToDouble(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                    {
                        return Convert.ToInt32(izquierda.getValor(arbol)).Equals(Convert.ToInt32(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.DATE) && tipDer.Equals(Primitivo.TIPO_DATO.DATE))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)).Equals(Convert.ToDateTime(derecha.getValor(arbol)));
                    }
                    else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                    {
                        return Convert.ToDateTime(izquierda.getValor(arbol)).Equals(Convert.ToDateTime(derecha.getValor(arbol)));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Binaria, getValor, !=) No soportado: " + tipIzq + " y " + tipDer);
                        return -1;
                    }
                case "&&":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                    {
                        return Convert.ToBoolean(izquierda.getValor(arbol)) && Convert.ToBoolean(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Primitivo, getValor, &&) No soportado: " + operador);
                        return -1;
                    }
                case "||":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                    {
                        return Convert.ToBoolean(izquierda.getValor(arbol)) || Convert.ToBoolean(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Primitivo, getValor, ||) No soportado: " + operador);
                        return -1;
                    }
                case "^":
                    if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                    {
                        return Convert.ToBoolean(izquierda.getValor(arbol)) ^ Convert.ToBoolean(derecha.getValor(arbol));
                    }
                    else
                    {
                        arbol.mensajes.Add("(Primitivo, getValor, ^) No soportado: " + operador);
                        return -1;
                    }
                default:
                    arbol.mensajes.Add("(Primitivo, getValor, default) No soportado: "+operador);
                    return -1;
            }
        }
    }
}