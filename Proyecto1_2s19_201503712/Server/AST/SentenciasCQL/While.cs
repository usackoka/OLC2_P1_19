using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class While:Sentencia
    {
        Expresion condicion;
        List<NodoCQL> instrucciones;
        TIPO_WHILE tipo;

        public While(Expresion condicion, List<NodoCQL> instrucciones, TIPO_WHILE tipo , int fila, int columna) {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
            this.tipo = tipo;
        }

        public enum TIPO_WHILE {
            WHILE, DO_WHILE
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            //========================== WHILE ===========================================
            if (tipo.Equals(TIPO_WHILE.WHILE))
            {
                while (Convert.ToBoolean(this.condicion.getValor(arbol)))
                {
                    arbol.entorno = new Entorno(arbol.entorno);

                    foreach (NodoCQL nodo in this.instrucciones)
                    {
                        if (nodo is Sentencia)
                        {
                            Object val = ((Sentencia)nodo).Ejecutar(arbol);
                            if (val != null)
                            {
                                //pregunto si es un continue
                                if (val.Equals(Corte.TIPO_CORTE.CONTINUE))
                                {
                                    break;
                                }
                                else if (val.Equals(Corte.TIPO_CORTE.BREAK))
                                {
                                    arbol.entorno = arbol.entorno.padre;
                                    return null;
                                }

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
                }
            }
            //============================= DO WHILE =====================================
            else {
                do {
                    arbol.entorno = new Entorno(arbol.entorno);
                    foreach (NodoCQL nodo in this.instrucciones)
                    {
                        if (nodo is Sentencia)
                        {
                            Object val = ((Sentencia)nodo).Ejecutar(arbol);
                            if (val != null)
                            {
                                //pregunto si es un continue
                                if (val.Equals(Corte.TIPO_CORTE.CONTINUE))
                                {
                                    break;
                                }
                                else if (val.Equals(Corte.TIPO_CORTE.BREAK))
                                {
                                    arbol.entorno = arbol.entorno.padre;
                                    return null;
                                }

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
                } while (Convert.ToBoolean(this.condicion.getValor(arbol)));
            }
            
            
            return null;
        }
    }
}