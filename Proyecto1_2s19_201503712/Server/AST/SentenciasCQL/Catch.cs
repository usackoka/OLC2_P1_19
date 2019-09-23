using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Catch : Sentencia
    {
        String idEx;
        ExceptionCQL.EXCEPTION exception;
        List<NodoCQL> instrucciones;
        public ExceptionCQL excCapturada;

        public Catch(String idEx, ExceptionCQL.EXCEPTION exception ,List<NodoCQL> instrucciones, int fila, int columna) {
            this.idEx = idEx;
            this.exception = exception;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {

            if (!excCapturada.ex.Equals(exception) && !excCapturada.ex.Equals(ExceptionCQL.EXCEPTION.Exception)) {
                arbol.addError("Catch-"+exception,"Se capturó una excepción: "+excCapturada+" y se esperaba: "+exception, fila, columna);
                return null;
            }

            arbol.entorno = new Entorno(arbol.entorno);

            //creo la variable excep
            arbol.entorno.addVariable(idEx,new Variable(excCapturada.getValor(arbol),Primitivo.TIPO_DATO.STRING),arbol,fila,columna);

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
            arbol.entorno = arbol.entorno.padre;

            return null;
        }

            /*
        public String getMensaje() {
            if (EXCEPTION.ArithmeticException == this.exception)
            {
                return this.exception + " - " + "Operación aritmética inválida";
            }
            else if (EXCEPTION.CounterTypeException == this.exception)
            {
                return this.exception + " - " + "No se puede actualizar una columna de tipo counter";
            }
            else if (EXCEPTION.UserAlreadyExists == this.exception)
            {
                return this.exception + " - " + "El userType que se intenta crear ya existe";
            }
            else if (EXCEPTION.UserDontExists == this.exception)
            {
                return this.exception + " - " + "El userType al que hace referencia no existe";
            }
            else if (EXCEPTION.ValuesException == this.exception)
            {
                return this.exception + " - " + "Los valores no coinciden o bien no hay suficientes valores";
            }
            else if (EXCEPTION.ColumnException == this.exception)
            {
                return this.exception + " - " + "No existe la columna referente a la tabla";
            }
            else if (EXCEPTION.BatchException == this.exception)
            {
                return this.exception + " - " + "No se pudo ejecutar todo el batch";
            }
            else if (EXCEPTION.IndexOutException == this.exception)
            {
                return this.exception + " - " + "El indice está fuera del intervalo de la coleccion";
            }
            else if (EXCEPTION.NullPointerException == this.exception)
            {
                return this.exception + " - " + "NullPointer - Se accedió o se obtuvo un valor NULL";
            }
            else if (EXCEPTION.NumberReturnsException == this.exception)
            {
                return this.exception + " - " + "La cantidad de variables declaradas en retorno no coincide con la cantidad de retornos";
            }
            else if (EXCEPTION.FunctionAlreadyExists == this.exception)
            {
                return this.exception + " - " + "La función declarada ya existe";
            }
            else if (EXCEPTION.ProcedureAlreadyExists == this.exception)
            {
                return this.exception + " - " + "El procedimiento declarado ya existe";
            }
            else if (EXCEPTION.ObjectAlreadyExists == this.exception)
            {
                return this.exception + " - " + "Ya existe el objeto instanciado";
            }
            else if (EXCEPTION.TypeAlreadyExists == this.exception)
            {
                return this.exception + " - " + "El userType ya existe";
            }
            else if (EXCEPTION.TypeDontExists == this.exception)
            {
                return this.exception + " - " + "El userType no existe";
            }
            else if (EXCEPTION.BDAlreadyExists == this.exception)
            {
                return this.exception + " - " + "La base de datos ya existe";
            }
            else if (EXCEPTION.BDDontExists == this.exception)
            {
                return this.exception + " - " + "La base de datos no Existe";
            }
            else if (EXCEPTION.UseBDException == this.exception)
            {
                return this.exception + " - " + "No se poseen los permisos para utilizar la base de datos";
            }
            else if (EXCEPTION.TableAlreadyExists == this.exception)
            {
                return this.exception + " - " + "La tabla ya existe";
            }
            else if (EXCEPTION.TableDontExists == this.exception)
            {
                return this.exception + " - " + "La tabla no existe";
            }
            else if (EXCEPTION.Exception == this.exception) {
                return this.exception + " - " + "Exception Captured";
            }
            else {
                return this.exception + " - " + "No hay mensaje personalizado para esta exception";
            }
        }
        */
    }
}