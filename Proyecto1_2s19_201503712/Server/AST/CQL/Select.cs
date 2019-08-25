using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Select : Sentencia
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

        public override object Ejecutar(AST_CQL arbol)
        {
            List<ColumnCQL> resultado = new List<ColumnCQL>();

            //Select de todo primero
            Object o = selectType.getResult(this.idTabla, this.where ,arbol);
            if (o is List<ColumnCQL>)
            {
                resultado = (List<ColumnCQL>)o;
            }
            else {
                return o;
            }

            //agrego la lista a los print de tables
            arbol.res_consultas.Add(resultado);

            return resultado;
        }
        
    }
}