using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class TodayNow : Expresion
    {
        Object tipoDato;

        public TodayNow(Object tipoDato, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return tipoDato;
        }

        public override object getValor(AST_CQL arbol)
        {
            return DateTime.Now;
        }
    }
}