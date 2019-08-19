using Server.AST.ColeccionesCQL;
using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Reasignacion : Sentencia
    {
        String id;
        Expresion expresion;

        public Reasignacion(String id, Expresion expresion, int fila, int columna) {
            this.id = id;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (expresion is ValorColeccion) {
                ((ValorColeccion)expresion).tipoDato = (new Primitivo(id + " (Identifier)", fila, columna).getTipo(arbol));
            }
            arbol.entorno.reasignarVariable(id,expresion.getValor(arbol),expresion.getTipo(arbol),arbol,fila,columna);
            return null;
        }
    }
}