using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Return : Sentencia
    {
        Expresion expresion;
        public Return(Expresion expresion, int fila, int columna) {
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return this.expresion.getValor(arbol);
        }
    }
}