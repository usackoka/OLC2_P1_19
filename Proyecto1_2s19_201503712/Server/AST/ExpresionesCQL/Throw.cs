using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Throw : Sentencia
    {
        ExceptionCQL.EXCEPTION exception;

        public Throw(ExceptionCQL.EXCEPTION exception, int fila, int columna) {
            this.exception = exception;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return new ExceptionCQL(this.exception,"",fila,columna);
        }
    }
}