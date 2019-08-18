using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Actualizar : Sentencia
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

        public override object Ejecutar(AST_CQL arbol)
        {
            Reasignacion reasignacion = new Reasignacion(this.id, new Binaria(new Primitivo(this.id+ " (Identifier)", fila,columna), operador,
                expresion,fila,columna),fila,columna);
            reasignacion.Ejecutar(arbol);

            return null;
        }
    }
}