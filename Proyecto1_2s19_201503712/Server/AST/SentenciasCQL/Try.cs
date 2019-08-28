using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Try : Sentencia
    {
        List<NodoCQL> instrucciones;
        Catch catchh;

        public Try(List<NodoCQL> instrucciones, Catch catchh, int fila, int columna) {
            this.instrucciones = instrucciones;
            this.catchh = catchh;
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

                        if (val is Catch.EXCEPTION) {
                            return catchh.Ejecutar(arbol);
                        }
                        return val;
                    }
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