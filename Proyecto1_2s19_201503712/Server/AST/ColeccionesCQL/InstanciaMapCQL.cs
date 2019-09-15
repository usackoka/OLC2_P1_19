using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class InstanciaMapCQL : Expresion
    {
        Object tipoClave, tipoValor;
        public InstanciaMapCQL(Object tipoClave, Object tipoValor, int fila, int columna) {
            this.tipoClave = tipoClave;
            this.tipoValor = tipoValor;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return new TipoMAP(this.tipoClave,this.tipoValor);
        }

        public override object getValor(AST_CQL arbol)
        {
            return new MapCQL(this.tipoClave, this.tipoValor, fila, columna);
        }
    }
}