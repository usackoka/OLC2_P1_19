using Server.AST.ExpresionesCQL.Tipos;
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
            return new Date();
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
                arbol.addError("Clase DateTime", "No posee el método: " + idMetodo, 0, 0);
                return new Null();
            }
        }

        public Object getMetodoTime(String idMetodo, TimeSpan value, AST_CQL arbol, int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;

            if (idMetodo.ToLower().Equals("gethour"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase Time, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Hours;
            }
            else if (idMetodo.ToLower().Equals("getminuts"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase Time, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Minutes;
            }
            else if (idMetodo.ToLower().Equals("getseconds"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase Time, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.Seconds;
            }
            else
            {
                arbol.addError("Clase Time", "No posee el método: " + idMetodo, 0, 0);
                return 0;
            }
        }

        public Object getMetodoDateTime(String idMetodo, Date value, AST_CQL arbol, int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            if (idMetodo.ToLower().Equals("getyear"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase Date, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.dateTime.Year;
            }
            else if (idMetodo.ToLower().Equals("getmonth"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase Date, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.dateTime.Month;
            }
            else if (idMetodo.ToLower().Equals("getday"))
            {
                if (this.expresiones.Count != 0)
                {
                    arbol.addError("Clase Date, " + idMetodo, "Se esperaba exclusivamente 0 parametros", 0, 0);
                    return 0;
                }
                return value.dateTime.Day;
            }
            else
            {
                arbol.addError("Clase Date", "No posee el método: " + idMetodo, 0, 0);
                return 0;
            }
        }
    }
}