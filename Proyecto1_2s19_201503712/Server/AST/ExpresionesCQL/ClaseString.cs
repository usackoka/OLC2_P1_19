using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class ClaseString : Expresion
    {
        public List<Expresion> expresiones { get; set; }

        public ClaseString() {
        }

        public Object getTipoMetodo(String idMetodo,AST_CQL arbol) {
            if (idMetodo.ToLower().Equals("length"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else if (idMetodo.ToLower().Equals("tolowercase"))
            {
                return Primitivo.TIPO_DATO.STRING;
            }
            else if (idMetodo.ToLower().Equals("touppercase"))
            {
                return Primitivo.TIPO_DATO.STRING;
            }
            else if (idMetodo.ToLower().Equals("startswith"))
            {
                return Primitivo.TIPO_DATO.BOOLEAN;
            }
            else if (idMetodo.ToLower().Equals("endswith"))
            {
                return Primitivo.TIPO_DATO.BOOLEAN;
            }
            else if (idMetodo.ToLower().Equals("substring"))
            {
                return Primitivo.TIPO_DATO.STRING;
            }
            else if (idMetodo.ToLower().Equals("message")) {
                return Primitivo.TIPO_DATO.STRING;
            }
            else
            {
                arbol.addError("Clase String", "No posee el método: " + idMetodo, 0, 0);
                return new Null();
            }
        }

        public Object getMetodoString(String idMetodo, String value, AST_CQL arbol) {
            if (idMetodo.ToLower().Equals("length")) {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase String, "+idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Length;
            } else if (idMetodo.ToLower().Equals("tolowercase")) {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase String, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return "";
                }
                return value.ToLower();
            }
            else if (idMetodo.ToLower().Equals("message"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase String, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return "";
                }
                return value;
            }
            else if (idMetodo.ToLower().Equals("touppercase")) {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase String, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return "";
                }
                return value.ToUpper();
            } else if (idMetodo.ToLower().Equals("startswith")) {
                if (this.expresiones.Count != 1)
                {
                    arbol.addError("Clase String, " + idMetodo, "Se esperaba exclusivamente 1 parametros", 0, 0);
                    return false;
                }
                return value.StartsWith(expresiones[0].getValor(arbol).ToString());
            } else if (idMetodo.ToLower().Equals("endswith")) {
                if (this.expresiones.Count != 1)
                {
                    arbol.addError("Clase String, " + idMetodo, "Se esperaba exclusivamente 1 parametros", 0, 0);
                    return false;
                }
                return value.EndsWith(expresiones[0].getValor(arbol).ToString());
            } else if (idMetodo.ToLower().Equals("substring")) {
                int start, end;
                if (this.expresiones.Count != 2)
                {
                    arbol.addError("Clase String, " + idMetodo, "Se esperaba exclusivamente 2 parametros", 0, 0);
                    return "";
                }
                else {
                    Object o1 = expresiones[0].getValor(arbol);
                    Object o2 = expresiones[1].getValor(arbol);
                    if (o1 is Int32 && o2 is Int32)
                    {
                        start = Convert.ToInt32(o1);
                        end = Convert.ToInt32(o2);
                    }
                    else {
                        arbol.addError("Clase String, " + idMetodo, "Los parámetros deben de ser de tipo Int", 0, 0);
                        return "";
                    }
                }
                return value.Substring(start,end);
            } else {
                arbol.addError("Clase String","No posee el método: "+idMetodo, 0, 0);
                return "";
            }
        }
        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.STRING;
        }

        public override object getValor(AST_CQL arbol)
        {
            return "";
        }
    }
}