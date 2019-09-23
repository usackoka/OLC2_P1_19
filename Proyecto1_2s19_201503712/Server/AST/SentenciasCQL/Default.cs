using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Default : Sentencia
    {
        List<NodoCQL> instrucciones;

        public Default(List<NodoCQL> instrucciones, int fila, int columna) {
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

            return null;
        }
    }
}