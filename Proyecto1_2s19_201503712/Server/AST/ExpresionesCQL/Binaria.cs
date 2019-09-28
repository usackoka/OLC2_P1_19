using Server.AST.ColeccionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
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

            if (izq.Equals(Primitivo.TIPO_DATO.COUNTER)) {
                izq = Primitivo.TIPO_DATO.INT;
            } else if (der.Equals(Primitivo.TIPO_DATO.COUNTER)) {
                der = Primitivo.TIPO_DATO.INT;
            }

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
                    else if (izq is TipoMAP && der is TipoMAP)
                    {
                        return new TipoMAP(((TipoMAP)izq).tipoClave, ((TipoMAP)izq).tipoValor);
                    }
                    else if (izq is TipoSet && der is TipoSet)
                    {
                        return new TipoSet(((TipoSet)izq).tipo);
                    }
                    else if (izq is TipoList && der is TipoList)
                    {
                        return new TipoList(((TipoList)izq).tipo);
                    }
                    else {
                        arbol.addError("", "(Binaria, getTipo, Suma) No soportado: " + izq + " y " + der, fila, columna);
                        return new Null();
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
                    else if (izq is TipoMAP && der is TipoSet)
                    {
                        return new TipoMAP(((TipoMAP)izq).tipoClave, ((TipoMAP)izq).tipoValor);
                    }
                    else if (izq is TipoSet && der is TipoSet)
                    {
                        return new TipoSet(((TipoSet)izq).tipo);
                    }
                    else if (izq is TipoList && der is TipoList)
                    {
                        return new TipoList(((TipoList)izq).tipo);
                    }
                    else
                    {
                        arbol.addError("","(Binaria, getTipo, Multiplicación, Resta, Módulo, División) No soportado: " + izq + " y " +der,fila,columna);
                        return new Null();
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
                        arbol.addError("","(Binaria, getTipo, potencia) No soportado: " + izq + " y " +der,fila,columna);
                        return new Null();
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
                case "in":
                    return Primitivo.TIPO_DATO.BOOLEAN;
                default:
                    arbol.addError("","(Binaria, getTipo, default) No soportado: "+operador, fila, columna);
                    return Primitivo.TIPO_DATO.STRING;
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            Object tipIzq = izquierda.getTipo(arbol);
            Object tipDer = derecha.getTipo(arbol);


            if (tipIzq.Equals(Primitivo.TIPO_DATO.COUNTER))
            {
                tipIzq = Primitivo.TIPO_DATO.INT;
            }
            else if (tipDer.Equals(Primitivo.TIPO_DATO.COUNTER))
            {
                tipDer = Primitivo.TIPO_DATO.INT;
            }

            try
            {
                switch (operador)
                {
                    case "+":
                        if (tipIzq.Equals(Primitivo.TIPO_DATO.STRING) || tipDer.Equals(Primitivo.TIPO_DATO.STRING))
                        {
                            return izquierda.getValor(arbol) + "" + derecha.getValor(arbol);
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                        {
                            Object value = Convert.ToDouble(izquierda.getValor(arbol)) + Convert.ToDouble(derecha.getValor(arbol));
                            return value;
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                        {
                            return Convert.ToInt32(izquierda.getValor(arbol)) + Convert.ToInt32(derecha.getValor(arbol));
                        }
                        else if (tipIzq is TipoMAP && tipDer is TipoMAP)
                        {
                            MapCQL mapIzq = (MapCQL)izquierda.getValor(arbol);
                            mapIzq.addRange((MapCQL)derecha.getValor(arbol));
                            return mapIzq;
                        }
                        else if (tipIzq is TipoSet && tipDer is TipoSet)
                        {
                            SetCQL setIzq = (SetCQL)izquierda.getValor(arbol);
                            setIzq.addRange((SetCQL)derecha.getValor(arbol), arbol);
                            return setIzq;
                        }
                        else if (tipIzq is TipoList && tipDer is TipoList)
                        {
                            ListCQL listIzq = (ListCQL)izquierda.getValor(arbol);
                            listIzq.addRange((ListCQL)derecha.getValor(arbol), arbol);
                            return listIzq;
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, Suma) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return -1;
                        }
                    case "-":
                        if (tipIzq.Equals(Primitivo.TIPO_DATO.DOUBLE) || tipDer.Equals(Primitivo.TIPO_DATO.DOUBLE))
                        {
                            Object value = Convert.ToDouble(izquierda.getValor(arbol)) - Convert.ToDouble(derecha.getValor(arbol));
                            return value;
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.INT) && tipDer.Equals(Primitivo.TIPO_DATO.INT))
                        {
                            Object value = Convert.ToInt32(izquierda.getValor(arbol)) - Convert.ToInt32(derecha.getValor(arbol));
                            return value;
                        }
                        else if (tipIzq is TipoSet && tipDer is TipoSet)
                        {
                            SetCQL setIzq = (SetCQL)izquierda.getValor(arbol);
                            setIzq.removeRange((SetCQL)derecha.getValor(arbol));
                            return setIzq;
                        }
                        else if (tipIzq is TipoMAP && tipDer is TipoSet)
                        {
                            MapCQL mapIzq = (MapCQL)izquierda.getValor(arbol);
                            mapIzq.removeRange((SetCQL)derecha.getValor(arbol));
                            return mapIzq;
                        }
                        else if (tipIzq is TipoList && tipDer is TipoList)
                        {
                            ListCQL listIzq = (ListCQL)izquierda.getValor(arbol);
                            listIzq.removeRange((ListCQL)derecha.getValor(arbol));
                            return listIzq;
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, Resta) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return -1;
                        }
                    case "in":
                        if (tipDer is TipoSet)
                        {
                            SetCQL setIzq = (SetCQL)derecha.getValor(arbol);
                            return setIzq.valores.Contains(izquierda.getValor(arbol));
                        }
                        else if (tipDer is TipoMAP)
                        {
                            MapCQL mapIzq = (MapCQL)derecha.getValor(arbol);
                            return mapIzq.valores.ContainsKey(izquierda.getValor(arbol));
                        }
                        else if (tipDer is TipoList)
                        {
                            ListCQL listIzq = (ListCQL)derecha.getValor(arbol);
                            return listIzq.valores.Contains(izquierda.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, IN) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
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
                            arbol.addError("", "(Binaria, getValor, Multiplicación) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
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
                            arbol.addError("", "(Binaria, getValor, Modular) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
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
                            arbol.addError("", "(Binaria, getValor, División) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
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
                            arbol.addError("", "(Binaria, getTipo, potencia) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
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
                            return ((Date)izquierda.getValor(arbol)).dateTime <= ((Date)derecha.getValor(arbol)).dateTime;
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                        {
                            return ((TimeSpan)izquierda.getValor(arbol)) <= ((TimeSpan)derecha.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, <=) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return false;
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
                            return ((Date)izquierda.getValor(arbol)).dateTime >= ((Date)derecha.getValor(arbol)).dateTime;
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                        {
                            return ((Date)izquierda.getValor(arbol)).dateTime >= ((Date)derecha.getValor(arbol)).dateTime;
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, >=) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return false;
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
                            return ((Date)izquierda.getValor(arbol)).dateTime > ((Date)derecha.getValor(arbol)).dateTime;
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                        {
                            return ((TimeSpan)izquierda.getValor(arbol)) > ((TimeSpan)derecha.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, >) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return false;
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
                            return ((Date)izquierda.getValor(arbol)).dateTime < ((Date)derecha.getValor(arbol)).dateTime;
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                        {
                            return ((TimeSpan)izquierda.getValor(arbol)) < ((TimeSpan)derecha.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, <) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return false;
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
                            return !((Date)izquierda.getValor(arbol)).dateTime.Equals(((Date)(derecha.getValor(arbol))).dateTime);
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                        {
                            return !((TimeSpan)izquierda.getValor(arbol)).Equals((TimeSpan)derecha.getValor(arbol));
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                        {
                            return !Convert.ToBoolean(izquierda.getValor(arbol)).Equals(Convert.ToBoolean(derecha.getValor(arbol)));
                        }
                        else if (tipIzq is Null || tipDer is Null)
                        {
                            return !izquierda.getValor(arbol).ToString().Equals(derecha.getValor(arbol).ToString());
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, !=) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return !izquierda.getValor(arbol).Equals(derecha.getValor(arbol));
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
                            return ((Date)izquierda.getValor(arbol)).dateTime.Equals(((Date)derecha.getValor(arbol)).dateTime);
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.TIME) && tipDer.Equals(Primitivo.TIPO_DATO.TIME))
                        {
                            return (izquierda.getValor(arbol)).Equals((derecha.getValor(arbol)));
                        }
                        else if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                        {
                            return Convert.ToBoolean(izquierda.getValor(arbol)).Equals(Convert.ToBoolean(derecha.getValor(arbol)));
                        }
                        else if (tipIzq is Null|| tipDer is Null)
                        {
                            return izquierda.getValor(arbol).ToString().Equals(derecha.getValor(arbol).ToString());
                        }
                        else
                        {
                            arbol.addError("", "(Binaria, getValor, ==) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return izquierda.getValor(arbol).Equals(derecha.getValor(arbol));
                        }
                    case "&&":
                        if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                        {
                            return Convert.ToBoolean(izquierda.getValor(arbol)) && Convert.ToBoolean(derecha.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Primitivo, getValor, &&) No soportado: " +tipIzq + " y " + tipDer, fila, columna);
                            return false;
                        }
                    case "||":
                        if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                        {
                            return Convert.ToBoolean(izquierda.getValor(arbol)) || Convert.ToBoolean(derecha.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Primitivo, getValor, ||) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return false;
                        }
                    case "^":
                        if (tipIzq.Equals(Primitivo.TIPO_DATO.BOOLEAN) && tipDer.Equals(Primitivo.TIPO_DATO.BOOLEAN))
                        {
                            return Convert.ToBoolean(izquierda.getValor(arbol)) ^ Convert.ToBoolean(derecha.getValor(arbol));
                        }
                        else
                        {
                            arbol.addError("", "(Primitivo, getValor, ^) No soportado: " + tipIzq + " y " + tipDer, fila, columna);
                            return false;
                        }
                    default:
                        arbol.addError("", "(Primitivo, getValor, default) No soportado: " + operador, fila, columna);
                        return -1;
                }
            }
            catch (Exception ex)
            {
                arbol.addError(ex.ToString(),"Error Binaria, Error de tipos",fila,columna);
                return new Null();
            }
        }
    }
}