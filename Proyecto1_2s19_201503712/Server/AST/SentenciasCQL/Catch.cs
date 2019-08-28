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
        EXCEPTION exception;
        List<NodoCQL> instrucciones;
        public EXCEPTION excCapturada;

        public Catch(String idEx,EXCEPTION exception ,List<NodoCQL> instrucciones, int fila, int columna) {
            this.idEx = idEx;
            this.exception = exception;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {

            if (!excCapturada.Equals(exception)) {
                arbol.addError("Catch-"+exception,"Se capturó una excepción: "+excCapturada+" y se esperaba: "+exception, fila, columna);
                return null;
            }

            arbol.entorno = new Entorno(arbol.entorno);

            //creo la variable excep
            arbol.entorno.addVariable(idEx,new Variable(getMensaje(),Primitivo.TIPO_DATO.STRING));

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

        public enum EXCEPTION {
            ArithmeticException,
            CounterTypeException,
            UserAlreadyExists,
            UserDontExists,
            ValuesException,
            ColumnException,
            BatchException,
            IndexOutException,
            NullPointerException,
            NumberReturnsException,
            FunctionAlreadyExists,
            ProcedureAlreadyExists,
            ObjectAlreadyExists,
            TypeAlreadyExists,
            TypeDontExists,
            BDAlreadyExists,
            BDDontExists,
            UseBDException,
            TableAlreadyExists,
            TableDontExists
        }

        public String getMensaje() {
            if (EXCEPTION.ArithmeticException == this.exception)
            {
                return "Operación aritmética inválida";
            }
            else if (EXCEPTION.CounterTypeException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.UserAlreadyExists == this.exception)
            {
                return "El userType que se intenta crear ya existe";
            }
            else if (EXCEPTION.UserDontExists == this.exception)
            {
                return "El userType al que hace referencia no existe";
            }
            else if (EXCEPTION.ValuesException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.ColumnException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.BatchException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.IndexOutException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.NullPointerException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.NumberReturnsException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.FunctionAlreadyExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.ProcedureAlreadyExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.ObjectAlreadyExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.TypeAlreadyExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.TypeDontExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.BDAlreadyExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.BDDontExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.UseBDException == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.TableAlreadyExists == this.exception)
            {
                return "";
            }
            else if (EXCEPTION.TableDontExists == this.exception)
            {
                return "";
            }
            else {
                return "No hay mensaje personalizado para esta exception";
            }
        }
    }
}