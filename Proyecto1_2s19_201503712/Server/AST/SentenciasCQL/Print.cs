using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Print : Sentencia
    {
        Expresion exp;

        public Print(Expresion exp, int fila, int columna) {
            this.exp = exp;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            Object value = exp.getValor(arbol);
            arbol.mensajes.Add(""+value);
            return null;
        }
    }
}