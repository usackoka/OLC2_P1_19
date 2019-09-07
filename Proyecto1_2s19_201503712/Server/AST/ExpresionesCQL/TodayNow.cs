using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class TodayNow : Expresion
    {
        TIPO tipoDato;

        public TodayNow(TIPO tipoDato, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.columna = columna;
        }

        public enum TIPO {
            DATE, TIME
        }

        public override object getTipo(AST_CQL arbol)
        {
            if (this.tipoDato.Equals(TIPO.TIME))
                return Primitivo.TIPO_DATO.TIME;

            return Primitivo.TIPO_DATO.DATE;
        }

        public override object getValor(AST_CQL arbol)
        {
            if (this.tipoDato.Equals(TIPO.TIME))
                return new TimeSpan(DateTime.Now.Hour,DateTime.Now.Minute, DateTime.Now.Second);

            return new Date();
        }
    }
}