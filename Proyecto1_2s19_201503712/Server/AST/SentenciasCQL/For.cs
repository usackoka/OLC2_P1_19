﻿using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;

namespace Server.AST.SentenciasCQL
{
    public class For : Sentencia
    {
        Sentencia fuente_for;
        NodoCQL actualizacion;
        Expresion condicion;
        List<NodoCQL> instrucciones;

        public For(Sentencia fuente_for, Expresion condicion, NodoCQL actualizacion, List<NodoCQL> instrucciones,
            int fila, int columna)
        {
            this.fuente_for = fuente_for;
            this.condicion = condicion;
            this.actualizacion = actualizacion;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            //entorno para la variable iteradora
            arbol.entorno = new Entorno(arbol.entorno);
            //variable iteradora
            fuente_for.Ejecutar(arbol);

            Object valcon = condicion.getValor(arbol);
            Boolean vale = false;
            if (valcon is Boolean)
            {
                vale = Convert.ToBoolean(valcon);
            }
            else
            {
                arbol.addError("While", "No se puede obtener el valor booleano de la condición, valor: " + valcon, fila, columna);
            }

            while (vale)
            {
                //nuevo entorno de la iteracion
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
                //regreso al entorno de la variable iteradora
                arbol.entorno = arbol.entorno.padre;
                //actualizacion de la variable iteradora
                if (actualizacion is Sentencia)
                {
                    ((Sentencia)actualizacion).Ejecutar(arbol);
                }
                else
                {
                    ((Expresion)actualizacion).getValor(arbol);
                }

                //===========evaluar condicion de nuevo
                valcon = condicion.getValor(arbol);
                vale = false;
                if (valcon is Boolean)
                {
                    vale = Convert.ToBoolean(valcon);
                }
                else
                {
                    arbol.addError("While", "No se puede obtener el valor booleano de la condición, valor: " + valcon, fila, columna);
                }
            }
            //salgo del entorno de la variable iteradora
            arbol.entorno = arbol.entorno.padre;
            return null;
        }
    }
}