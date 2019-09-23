using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Batch : Sentencia
    {
        List<NodoCQL> instrucciones;

        public Batch(List<NodoCQL> instrucciones, int fila, int columna) {
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            arbol.entorno = new Entorno(arbol.entorno);
            //commit para guardar si hay algún error y hacer rollback
            Commit c = new Commit(fila,columna);
            c.Ejecutar(arbol);

            foreach (NodoCQL nodo in this.instrucciones)
            {
                if (nodo is Sentencia)
                {
                    Object val = ((Sentencia)nodo).Ejecutar(arbol);
                    if (val != null)
                    {
                        arbol.entorno = arbol.entorno.padre;
                        if (val is ExceptionCQL)
                        {
                            //rollback
                            RollBack r = new RollBack(fila,columna);
                            r.Ejecutar(arbol);
                            return val;
                        }
                    }
                }
            }
            arbol.entorno = arbol.entorno.padre;

            return null;
        }
    }
}