using Server.AST.ColeccionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Referencia : Expresion
    {
        List<Object> referencias;

        public Referencia(List<Object> referencias) {
            this.referencias = referencias;
        }

        public override object getTipo(AST_CQL arbol)
        {
            //================= para las que entran con ambiguedad
            if (referencias.Count == 1)
            {
                if (referencias[0] is LlamadaFuncion)
                {
                    return ((LlamadaFuncion)referencias[0]).getTipo(arbol);
                }
            }

            return getTipoRecursivo(arbol);
        }

        public override object getValor(AST_CQL arbol)
        {
            //================= para las que entran con ambiguedad
            if (referencias.Count == 1) {
                if (referencias[0] is LlamadaFuncion) {
                    return ((LlamadaFuncion)referencias[0]).getValor(arbol);
                }
            }

            return getValorRecursivo(arbol);
        }

        Object getValorRecursivo(AST_CQL arbol) {

            Object valorRetorno = Primitivo.TIPO_DATO.NULL;

            foreach (Object obj in referencias) {
                //============== saco el tipo
                Object tipo = Primitivo.TIPO_DATO.NULL;
                if (obj is String)
                {
                    tipo = (new Primitivo(obj + " (Identifier)", fila, columna)).getTipo(arbol);
                    //================ verifico el tipo
                    if (tipo.Equals(Primitivo.TIPO_DATO.LIST))
                    {
                        valorRetorno = (ListCQL)(new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                    }
                } else if (obj is LlamadaFuncion) {
                    //verifico el valor de retono
                    if (valorRetorno is ListCQL) {
                        ListCQL list = (ListCQL)valorRetorno;
                        LlamadaFuncion llf = (LlamadaFuncion)obj;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                    }
                }
            }

            return valorRetorno;
        }

        Object getTipoRecursivo(AST_CQL arbol) {
            Object valorRetorno = Primitivo.TIPO_DATO.NULL;
            Object tipoRetorno = Primitivo.TIPO_DATO.NULL;

            foreach (Object obj in referencias)
            {
                //============== saco el tipo
                Object tipo = Primitivo.TIPO_DATO.NULL;
                if (obj is String)
                {
                    tipo = (new Primitivo(obj + " (Identifier)", fila, columna)).getTipo(arbol);
                    //================ verifico el tipo
                    if (tipo.Equals(Primitivo.TIPO_DATO.LIST))
                    {
                        valorRetorno = (ListCQL)(new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                        tipoRetorno = Primitivo.TIPO_DATO.LIST;
                    }
                }
                else if (obj is LlamadaFuncion)
                {
                    //verifico el valor de retono
                    if (valorRetorno is ListCQL)
                    {
                        ListCQL list = (ListCQL)valorRetorno;
                        LlamadaFuncion llf = (LlamadaFuncion)obj;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    }
                }
            }

            return tipoRetorno;
        }

        Boolean ContainsString(String match, String search)
        {
            return match.ToLower().Equals(search);
            //return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}