using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class InstanciaSetCQL : Expresion
    {
        Object tipoDato;

        public InstanciaSetCQL(Object tipoDato, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return new TipoSet(this.tipoDato);
        }

        public override object getValor(AST_CQL arbol)
        {
            return new SetCQL(this.tipoDato, fila, columna);
        }
    }
}