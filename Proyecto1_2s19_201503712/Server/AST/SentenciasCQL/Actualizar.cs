using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Actualizar : Expresion
    {
        String id,operador;
        Expresion expresion;

        public Actualizar(String id, String operador, Expresion expresion, int fila, int columna) {
            this.id = id;
            this.operador = operador;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return new Primitivo(this.id + " (Identifier)", fila, columna).getTipo(arbol);
        }

        public override object getValor(AST_CQL arbol)
        {
            Object valor = new Primitivo(this.id + " (Identifier)", fila, columna).getValor(arbol);

            Reasignacion reasignacion = new Reasignacion(this.id, new Binaria(new Primitivo(this.id + " (Identifier)", fila, columna), operador,
                expresion, fila, columna), fila, columna);
            reasignacion.Ejecutar(arbol);

            return valor;
        }
    }
}