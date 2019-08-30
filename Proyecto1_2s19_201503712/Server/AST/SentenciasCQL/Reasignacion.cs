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
            this.ids = null;
        }

        List<String> ids;
        public Reasignacion(List<String> ids, Expresion expresion, int fila, int columna) {
            this.ids = ids;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            Object valor = expresion.getValor(arbol);
            Object tipo = expresion.getTipo(arbol);

            //resultado de una procedure
            if (this.ids != null) {
                if (!(valor is List<Object>)) {
                    arbol.addError("EXCEPTION.NullPointerException", "Se esperaba una llamada a Procedure del lado derecho", fila, columna);
                    return Catch.EXCEPTION.NullPointerException;
                }

                List<Object> retornos = (List<Object>)valor;

                if (retornos.Count != this.ids.Count) {
                    arbol.addError("EXCEPTION.NullPointerException", "No existe la misma cantidad de identificadores (" + this.ids.Count + ") esperando los retornos (" + retornos.Count + ")", fila, columna);
                    return Catch.EXCEPTION.NullPointerException;
                }

                if (this.ids.Count != ((List<Object>)tipo).Count)
                {
                    arbol.addError("EXCEPTION.NullPointerException", "No existe la misma cantidad de identificadores (" + this.ids.Count + ") esperando los tipos (" + ((List<Object>)tipo) + ")", fila, columna);
                    return Catch.EXCEPTION.NullPointerException;
                }

                for (int i = 0; i < this.ids.Count; i++)
                {
                    arbol.entorno.reasignarVariable(ids[i], retornos[i], ((List<Object>)tipo)[i], arbol, fila, columna);
                }

                return null;
            }

            if (tipo is List<Object>) {
                tipo = ((List<Object>)tipo)[0];
            }

            arbol.entorno.reasignarVariable(id,valor,tipo,arbol,fila,columna);
            return null;
        }
    }
}