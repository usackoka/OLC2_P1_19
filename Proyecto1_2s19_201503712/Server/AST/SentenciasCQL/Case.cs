using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Case : Sentencia
    {
        Expresion match;
        List<NodoCQL> instrucciones;
        public Object matchSource { get; set; }
        public bool ejecutado { get; set; }

        public Case(Expresion match, List<NodoCQL> instrucciones, int fila, int columna) {
            this.match = match;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
            this.ejecutado = false;
            this.matchSource = null;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (match.getValor(arbol).Equals(matchSource)) {
                ejecutado = true;
                arbol.entorno = new Entorno(arbol.entorno);
                foreach (NodoCQL nodo in this.instrucciones)
                {
                    if (nodo is Sentencia)
                    {
                        Object val = ((Sentencia)nodo).Ejecutar(arbol);
                        if (val != null)
                        {
                            arbol.entorno = arbol.entorno.padre;
                            return val;
                        }
                    }
                    else
                    {
                        Object val = ((Expresion)nodo).getValor(arbol);
                        if (val is ExceptionCQL)
                        {
                            return val;
                        }
                    }
                }
                arbol.entorno = arbol.entorno.padre;
            }

            return null;
        }
    }
}