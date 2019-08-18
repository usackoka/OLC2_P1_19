using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class SetCQL : Expresion
    {
        Object tipoDato;
        List<Object> valores;

        public SetCQL(Object tipoDato, int fila, int columna)
        {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.columna = columna;
        }

        public void setValores(List<Object> valores)
        {
            this.valores = valores;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.SET;
        }

        public override object getValor(AST_CQL arbol)
        {
            return this;
        }
    }
}