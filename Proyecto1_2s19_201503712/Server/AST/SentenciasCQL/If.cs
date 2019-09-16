﻿using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;

namespace Server.AST.SentenciasCQL
{
    public class If : Sentencia
    {
        Expresion condicion;
        List<NodoCQL> instrucciones;
        List<ElseIf> elseifs;
        Else else_;

        public If(Expresion condicion, List<NodoCQL> instrucciones, List<ElseIf> elseifs, Else else_, int fila, int columna)
        {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.elseifs = elseifs;
            this.else_ = else_;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            Object valcon = condicion.getValor(arbol);
            Boolean vale = false;
            if (valcon is Boolean)
            {
                vale = Convert.ToBoolean(valcon);
            }
            else
            {
                arbol.addError("If", "No se puede obtener el valor booleano de la condición, valor: " + valcon, fila, columna);
            }

            if (vale)
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
            }
            //si no se cumple el if, preguntar por los if-else
            else
            {
                foreach (ElseIf elseif in elseifs)
                {
                    Object val = elseif.Ejecutar(arbol);
                    if (val != null) {
                        return val;
                    }

                    if (elseif.ejecutado)
                    {
                        return null;
                    }
                }

                if (else_ != null)
                {
                    Object val = else_.Ejecutar(arbol);
                    if (val != null) {
                        return val;
                    }
                }
            }

            return null;
        }
    }
}