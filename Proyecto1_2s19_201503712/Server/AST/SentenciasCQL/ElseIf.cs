using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class ElseIf : Sentencia
    {
        Expresion condicion;
        List<NodoCQL> instrucciones;
        public Boolean ejecutado { get; set; }

        public ElseIf(Expresion condicion, List<NodoCQL> instrucciones, int fila, int columna) {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
            this.ejecutado = false;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (Convert.ToBoolean(condicion.getValor(arbol)))
            {
                arbol.entorno = new Entorno(arbol.entorno);
                this.ejecutado = true;
                foreach (NodoCQL nodo in this.instrucciones)
                {
                    if (nodo is Sentencia)
                    {
                        ((Sentencia)nodo).Ejecutar(arbol);
                    }
                    else
                    {
                        ((Expresion)nodo).getValor(arbol);
                    }
                }
                arbol.entorno = arbol.entorno.padre;
            }

            return null;
        }
    }
}