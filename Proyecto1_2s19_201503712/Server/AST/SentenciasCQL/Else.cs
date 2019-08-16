using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Else : Sentencia
    {
        List<NodoCQL> instrucciones;

        public Else(List<NodoCQL> instrucciones, int fila, int columna) {
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            arbol.entorno = new Entorno(arbol.entorno);
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

            return null;
        }
    }
}