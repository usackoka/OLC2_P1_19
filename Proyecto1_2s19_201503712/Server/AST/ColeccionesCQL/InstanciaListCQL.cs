using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class InstanciaListCQL : Expresion
    {
        Object tipoDato;

        public InstanciaListCQL(Object tipoDato, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return new TipoList(this.tipoDato);
        }

        public override object getValor(AST_CQL arbol)
        {
            return new ListCQL(this.tipoDato, fila, columna);
        }
    }
}