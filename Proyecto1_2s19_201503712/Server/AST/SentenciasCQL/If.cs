using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class If : Sentencia
    {
        Expresion condicion;
        List<NodoCQL> instrucciones;
        List<ElseIf> elseifs;
        Else else_;

        public If(Expresion condicion, List<NodoCQL> instrucciones, List<ElseIf> elseifs, Else else_, int fila, int columna) {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.elseifs = elseifs;
            this.else_ = else_;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (Convert.ToBoolean(this.condicion.getValor(arbol)))
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
            }
            //si no se cumple el if, preguntar por los if-else
            else {
                foreach (ElseIf elseif in elseifs) {
                    elseif.Ejecutar(arbol);
                    if (elseif.ejecutado) {
                        break;
                    }
                }
                if(else_!=null)
                    else_.Ejecutar(arbol);
            }

            return null;
        }
    }
}