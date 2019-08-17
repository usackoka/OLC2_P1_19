using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class LlamadaFuncion : Expresion
    {
        String idLlamada;
        List<Expresion> expresiones;
        TIPO_LLAMADA tipoLlamada;

        public LlamadaFuncion(String idLlamada, List<Expresion> expresiones, TIPO_LLAMADA tipoLlamada,
            int fila, int columna) {
            this.idLlamada = idLlamada;
            this.tipoLlamada = tipoLlamada;
            this.expresiones = expresiones;
            this.fila = fila;
            this.columna = columna;
        }

        public enum TIPO_LLAMADA {
            LLAMADA, CALL
        }

        public override object getTipo(AST_CQL arbol)
        {
            foreach (Funcion funcion in arbol.funciones)
            {
                if (funcion.id.Equals(idLlamada) && getFirma(arbol).Equals(funcion.getFirma()))
                {
                    return funcion.getTipoDato();
                }
            }
            arbol.addError(idLlamada, "No se encontró función con la firma: " + getFirma(arbol), fila, columna);
            return Primitivo.TIPO_DATO.NULL;
        }

        public override object getValor(AST_CQL arbol)
        {
            //============================ LLAMADA NORMAL ======================================
            if (this.tipoLlamada == TIPO_LLAMADA.LLAMADA)
            {
                //pregunto si existe la función con esta firma
                if (ExisteFuncion(arbol))
                {
                    //obtengo los parámetros
                    List<Object> valores = new List<object>();
                    foreach (Expresion expresion in expresiones)
                    {
                        valores.Add(expresion.getValor(arbol));
                    }

                    //busco la función
                    foreach (Funcion funcion in arbol.funciones)
                    {
                        if (funcion.id.Equals(idLlamada) && getFirma(arbol).Equals(funcion.getFirma()))
                        {
                            //paso los valores que tendrán los parámetros
                            funcion.valoresParametros = valores;
                            Object val = funcion.Ejecutar(arbol);
                            if (val != null)
                            {
                                return val;
                            }
                        }
                    }
                }
                else
                {
                    //retorno el nullpointer de no encontrada la función
                    return Primitivo.TIPO_DATO.NULL;
                }
            }
            //======================================== CALL PROCEDURE ============================
            else {
            }

            return null;
        }

        Boolean ExisteFuncion(AST_CQL arbol) {
            foreach (Funcion funcion in arbol.funciones) {
                if (funcion.id.Equals(idLlamada) && getFirma(arbol).Equals(funcion.getFirma())) {
                    return true;
                }
            }
            arbol.addError(idLlamada,"No se encontró función con la firma: "+getFirma(arbol),fila,columna);
            return false;
        }

        String getFirma(AST_CQL arbol) {
            String firma = "";
            foreach (Expresion kvp in expresiones)
            {
                firma += "_" + kvp.getTipo(arbol);
            }
            return firma;
        }
    }
}