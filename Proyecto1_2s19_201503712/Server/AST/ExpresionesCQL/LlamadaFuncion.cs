using Server.AST.CQL;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class LlamadaFuncion : Expresion
    {
        public String idLlamada;
        public List<Expresion> expresiones;
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
            if (tipoLlamada.Equals(TIPO_LLAMADA.CALL)) {
                Procedure procedure = arbol.dbms.getProcedure(this.idLlamada, getFirma(arbol),arbol);
                if (procedure != null)
                {  
                    return procedure.getTipo(arbol);
                }
                else
                {
                    //retorno el nullpointer de no encontrada la función
                    arbol.addError("EXCEPTION.NullPointerException", "No existe el procedure: " + this.idLlamada + " con firma: " + getFirma(arbol), fila, columna);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.NullPointerException, "No existe el procedure: " + this.idLlamada + " con firma: " + getFirma(arbol),fila,columna);
                }
            }

            foreach (Funcion funcion in arbol.funciones)
            {
                if (funcion.id.Equals(idLlamada) && getFirma(arbol).Equals(funcion.getFirma()))
                {
                    return funcion.getTipoDato();
                }
            }
            arbol.addError(idLlamada, "No se encontró función con la firma: " + getFirma(arbol), fila, columna);
            return new Null();
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
                        if (funcion.id.ToLower().Equals(idLlamada.ToLower()) && getFirma(arbol).Equals(funcion.getFirma()))
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
                    arbol.addError("EXCEPTION.NullPointerException","No existe la función: "+this.idLlamada+" con firma: "+getFirma(arbol),fila,columna);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.NullPointerException, "No existe la función: " + this.idLlamada + " con firma: " + getFirma(arbol), fila,columna);
                }
            }
            //======================================== CALL PROCEDURE ============================
            else {
                //pregunto si existe el procedure
                Procedure procedure = arbol.dbms.getProcedure(this.idLlamada, getFirma(arbol),arbol);
                if (procedure != null)
                {  //obtengo los parámetros
                    List<Object> valores = new List<object>();
                    foreach (Expresion expresion in expresiones)
                    {
                        valores.Add(expresion.getValor(arbol));
                    }

                    //paso los valores que tendrán los parámetros
                    procedure.valoresParametros = valores;
                    return procedure.Ejecutar(arbol);
                }
                else
                {
                    //retorno el nullpointer de no encontrada la función
                    arbol.addError("EXCEPTION.NullPointerException", "No existe el procedure: " + this.idLlamada + " con firma: " + getFirma(arbol), fila, columna);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.NullPointerException, "No existe el procedure: " + this.idLlamada + " con firma: " + getFirma(arbol),fila,columna);
                }
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

        public String getFirma(AST_CQL arbol) {
            String firma = "";
            foreach (Expresion kvp in expresiones)
            {
                Object tipo = kvp.getTipo(arbol);
                if (tipo is TipoList) {
                    tipo = new TipoList();
                } else if (tipo is TipoSet) {
                    tipo = new TipoSet();
                } else if (tipo is TipoMAP) {
                    tipo = new TipoMAP();
                }

                firma += "_" + tipo;
            }
            return firma;
        }
    }
}