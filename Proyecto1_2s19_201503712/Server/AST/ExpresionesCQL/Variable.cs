using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Variable : Expresion
    {
        Object valor;
        Object tipoDato;

        public Variable(Object valor, Object tipoDato) {
            this.valor = valor;
            this.tipoDato = tipoDato;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return tipoDato;
        }

        public override object getValor(AST_CQL arbol)
        {
            return valor;
        }

        public void setValor(Object valor) {
            this.valor = valor;
        }
    }
}