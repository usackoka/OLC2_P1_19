using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Select : Expresion
    {
        Select_Type selectType;
        String idTabla;
        Where where;
        OrderBy orderBy;
        Expresion limit;

        public Select(Select_Type selectType, String idTabla, Where where, OrderBy orderBy, 
            Expresion limit, int fila, int columna) {
            this.selectType = selectType;
            this.idTabla = idTabla;
            this.where = where;
            this.orderBy = orderBy;
            this.limit = limit;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return null;
        }

        public override object getValor(AST_CQL arbol)
        {
            List<ColumnCQL> resultado = new List<ColumnCQL>();

            //Select de todo primero
            resultado = selectType.getResult(this.idTabla,arbol);

            return resultado;
        }
    }
}