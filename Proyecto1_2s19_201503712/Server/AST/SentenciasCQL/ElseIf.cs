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
        //public Boolean ejecutado { get; set; }

        public ElseIf(Expresion condicion, List<NodoCQL> instrucciones, int fila, int columna) {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
            //this.ejecutado = false;
        }

        public Boolean ejectuarCondicion(AST_CQL arbol) {
            Object valcon = condicion.getValor(arbol);
            Boolean vale = false;
            if (valcon is Boolean)
            {
                vale = Convert.ToBoolean(valcon);
            }
            else
            {
                arbol.addError("ElseIf", "No se puede obtener el valor booleano de la condición, valor: " + valcon, fila, columna);
            }
            //this.ejecutado = false;
            return vale;
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
                    ((Expresion)nodo).getValor(arbol);
                }
            }

            arbol.entorno = arbol.entorno.padre;

            return null;
        }
    }
}