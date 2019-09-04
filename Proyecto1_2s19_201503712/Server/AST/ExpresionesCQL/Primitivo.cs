﻿using Server.AST.ExpresionesCQL.Tipos;
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
        private Object tipoDato;

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
                this.value = this.value.ToString().Replace(" (Identifier)", "").ToLower();
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
                    this.value = new Date(this.value.ToString());
                    this.tipoDato = new Date();
                }
                else {
                    String[] arr = this.value.ToString().Split(':');
                    if (arr.Length == 3)
                    {
                        this.value = new TimeSpan(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]),
                            Convert.ToInt32(arr[2]));
                    }
                    else {
                        this.value = new TimeSpan();
                    }
                    this.tipoDato = new TimeSpan();
                }
            }
        }

        public static Object getDefecto(Object tipoDato, AST_CQL arbol) {

            if (tipoDato is String || tipoDato is TipoMAP || tipoDato is TipoList || tipoDato is TipoSet) {
                return TIPO_DATO.NULL;
            } else if (tipoDato is Date) {
                return new Date();
            } else if (tipoDato is TimeSpan) {
                return new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }

            switch (tipoDato) {
                case TIPO_DATO.INT:
                case TIPO_DATO.COUNTER:
                    return 0;
                case TIPO_DATO.BOOLEAN:
                    return false;
                case TIPO_DATO.DOUBLE:
                    return 0.0;
                case TIPO_DATO.STRING:
                case TIPO_DATO.NULL:
                case TIPO_DATO.STRUCT:
                case TIPO_DATO.CURSOR:
                    return TIPO_DATO.NULL;
                default:
                    arbol.addError(tipoDato.ToString(),"No hay defecto para el tipo de dato: "+tipoDato,0,0);
                    return -1;
            }
        }

        public enum TIPO_DATO {
            INT, BOOLEAN, DOUBLE, STRING, NULL, ID, COUNTER, STRUCT, CURSOR
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
            if (this.tipoDato is TimeSpan || this.tipoDato is Date) {
                return this.value;
            }

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
                    return this.value;
            }
        }

        Boolean ContainsString(String match, String search) {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}