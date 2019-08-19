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
                if (obj is String)
                {
                    valorRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                }
                else if (obj is LlamadaFuncion)
                {
                    LlamadaFuncion llf = (LlamadaFuncion)obj;
                    //verifico el valor de retono
                    if (valorRetorno is String) {
                        ClaseString cs = new ClaseString();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoString(llf.idLlamada, valorRetorno.ToString(), arbol);
                    }
                    else if (valorRetorno is DateTime)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoDateTime(llf.idLlamada, (DateTime)valorRetorno, arbol);
                    }
                    else if (valorRetorno is ListCQL) {
                        ListCQL list = (ListCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                    }
                    else if (valorRetorno is SetCQL)
                    {
                        SetCQL list = (SetCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                    }
                    else if (valorRetorno is MapCQL)
                    {
                        MapCQL list = (MapCQL)valorRetorno;
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
                if (obj is String)
                {
                    tipoRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getTipo(arbol);
                    valorRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                }
                else if (obj is LlamadaFuncion)
                {
                    LlamadaFuncion llf = (LlamadaFuncion)obj;
                    //verifico el valor de retono
                    if (valorRetorno is String)
                    {
                        ClaseString cs = new ClaseString();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoString(llf.idLlamada, valorRetorno.ToString(), arbol);
                        tipoRetorno = cs.getTipoMetodo(llf.idLlamada, arbol);
                    }
                    else if (valorRetorno is ListCQL)
                    {
                        ListCQL list = (ListCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    } else if (valorRetorno is SetCQL) {
                        SetCQL list = (SetCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    } else if (valorRetorno is MapCQL)
                    {
                        MapCQL list = (MapCQL)valorRetorno;
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