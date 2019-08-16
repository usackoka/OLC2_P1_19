using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Primitivo : Expresion
    {
        private Object value;
        private TIPO_DATO tipoDato;

        public Primitivo(String value, int fila, int columna)
        {
            this.value = value;
            this.fila = fila;
            this.columna = columna;
            setTipoDato();
        }

        private void setTipoDato() {
            if (this.value.ToString().Contains(" (numero)"))
            {
                this.value = this.value.ToString().Replace(" (numero)", "");
                if (this.value.ToString().Contains("."))
                {
                    this.value = Convert.ToDouble(this.value);
                    this.tipoDato = TIPO_DATO.DOUBLE;
                }
                else
                {
                    this.value = Convert.ToInt32(this.value);
                    this.tipoDato = TIPO_DATO.INT;
                }
            }
            else if (this.value.ToString().Contains(" (Keyword)"))
            {
                this.value = this.value.ToString().Replace(" (Keyword)", "");
                if (ContainsString(this.value.ToString(), "true") || ContainsString(this.value.ToString(), "false"))
                {
                    this.value = ContainsString(this.value.ToString(), "true") ? true : false;
                    this.tipoDato = TIPO_DATO.BOOLEAN;
                }
                else
                {
                    this.value = TIPO_DATO.NULL;
                    this.tipoDato = TIPO_DATO.NULL;
                }
            }
            else if (this.value.ToString().Contains(" (Identifier)"))
            {
                this.value = this.value.ToString().Replace(" (Identifier)", ""); ;
                this.tipoDato = TIPO_DATO.ID;
            }
            else if (this.value.ToString().Contains(" (cadena)"))
            {
                this.value = this.value.ToString().Replace(" (cadena)", "").Replace(" (cadena2)", "");
                this.tipoDato = TIPO_DATO.STRING;
            }
            else if (this.value.ToString().Contains(" (cadena2)")) {
                this.value = this.value.ToString().Replace(" (cadena)", "").Replace(" (cadena2)", "");
                if (this.value.ToString().Contains("-") || this.value.ToString().Contains("/"))
                {
                    this.value = DateTime.Parse(this.value.ToString());
                    this.tipoDato = TIPO_DATO.DATE;
                }
                else {
                    this.value = DateTime.Parse(this.value.ToString());
                    this.tipoDato = TIPO_DATO.TIME;
                }
            }
        }

        public static Object getDefecto(Object tipoDato, AST_CQL arbol) {
            if (tipoDato is String) {
                return TIPO_DATO.NULL;
            }
            switch (tipoDato) {
                case TIPO_DATO.INT:
                    return 0;
                case TIPO_DATO.BOOLEAN:
                    return false;
                case TIPO_DATO.DOUBLE:
                    return 0.0;
                case TIPO_DATO.STRING:
                    return "";
                case TIPO_DATO.DATE:
                    return DateTime.Now;
                case TIPO_DATO.TIME:
                    return DateTime.Now;
                case TIPO_DATO.NULL:
                    return TIPO_DATO.NULL;
                default:
                    arbol.addError(tipoDato.ToString(),"No hay defecto para el tipo de dato: "+tipoDato,0,0);
                    return -1;
            }
        }

        public enum TIPO_DATO {
            INT, BOOLEAN, DOUBLE, STRING, DATE, TIME, NULL, ID, LIST, SET, MAP, COUNTER, STRUCT
        }

        public override object getTipo(AST_CQL arbol)
        {
            if (this.tipoDato.Equals(TIPO_DATO.ID))
            {
                return arbol.entorno.getTipoVariable(this.value.ToString(), arbol, fila, columna);
            }
            else {
                return this.tipoDato;
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            switch (this.tipoDato)
            {
                case TIPO_DATO.NULL:
                case TIPO_DATO.BOOLEAN:
                case TIPO_DATO.DOUBLE:
                case TIPO_DATO.INT:
                case TIPO_DATO.STRING:
                    return this.value;
                case TIPO_DATO.ID:
                    return arbol.entorno.getValorVariable(this.value.ToString(),arbol,fila,columna);
                default:
                    arbol.addError("","(Primitivo) no soportado tipo: "+this.tipoDato,fila,columna);
                    return "not supported yet";
            }
        }

        Boolean ContainsString(String match, String search) {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}