using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Unario : Expresion
    {
        Expresion unario;
        String operador;

        public Unario(String operador, Expresion unario, int fila, int columna) {
            this.unario = unario;
            this.operador = operador;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }

        public override object getValor(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }
    }
}