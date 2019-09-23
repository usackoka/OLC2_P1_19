using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class ExceptionCQL : Expresion
    {
        public EXCEPTION ex;
        public String mensaje;

        public ExceptionCQL(EXCEPTION ex, String mensaje, int fila, int columna) {
            this.ex = ex;
            this.mensaje = mensaje;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.STRING;
        }

        public override object getValor(AST_CQL arbol)
        {
            return this.ex + " - " + this.mensaje;
        }

        public enum EXCEPTION
        {
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
            TableDontExists,
            Exception
        }
    }
}