﻿using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class For : Sentencia
    {
        Sentencia fuente_for, actualizacion;
        Expresion condicion;
        List<NodoCQL> instrucciones;

        public For(Sentencia fuente_for, Expresion condicion, Sentencia actualizacion, List<NodoCQL> instrucciones,
            int fila, int columna) {
            this.fuente_for = fuente_for;
            this.condicion = condicion;
            this.actualizacion = actualizacion;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            arbol.entorno = new Entorno(arbol.entorno);

            //variable iteradora
            fuente_for.Ejecutar(arbol);
            while (Convert.ToBoolean(condicion.getValor(arbol))) {
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
                //actualizacion de la variable iteradora
                actualizacion.Ejecutar(arbol);
            }

            arbol.entorno = arbol.entorno.padre;
            return null;
        }
    }
}