using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class ClaseDateTime : Expresion
    {
        public List<Expresion> expresiones { get; set; }

        public ClaseDateTime() {
        }

        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.DATE;
        }

        public override object getValor(AST_CQL arbol)
        {
            return DateTime.Now;
        }

        public Object getTipoMetodo(String idMetodo, AST_CQL arbol)
        {
            if (idMetodo.ToLower().Equals("getyear"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else if (idMetodo.ToLower().Equals("getmonth"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else if (idMetodo.ToLower().Equals("getday"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else if (idMetodo.ToLower().Equals("gethour"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else if (idMetodo.ToLower().Equals("getminuts"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else if (idMetodo.ToLower().Equals("getseconds"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else
            {
                arbol.addError("Clase String", "No posee el método: " + idMetodo, 0, 0);
                return Primitivo.TIPO_DATO.NULL;
            }
        }

        public Object getMetodoDateTime(String idMetodo, DateTime value, AST_CQL arbol)
        {
            if (idMetodo.ToLower().Equals("getyear"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase DateTime, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Year;
            }
            else if (idMetodo.ToLower().Equals("getmonth"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase DateTime, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Month;
            }
            else if (idMetodo.ToLower().Equals("getday"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase DateTime, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Day;
            }
            else if (idMetodo.ToLower().Equals("gethour"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase DateTime, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Hour;
            }
            else if (idMetodo.ToLower().Equals("getminuts"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase DateTime, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Minute;
            }
            else if (idMetodo.ToLower().Equals("getseconds"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase DateTime, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Second;
            }
            else
            {
                arbol.addError("Clase DateTime", "No posee el método: " + idMetodo, 0, 0);
                return 0;
            }
        }
    }
}