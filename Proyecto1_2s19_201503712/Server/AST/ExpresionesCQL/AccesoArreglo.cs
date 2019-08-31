using Server.AST.ColeccionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class AccesoArreglo : Expresion
    {
        public String id;
        public Expresion expresion;

        public AccesoArreglo(String id, Expresion expresion, int fila, int columna) {
            this.id = id;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            Object obj = arbol.entorno.getTipoVariable("$" + this.id, arbol, fila, columna);
            if (obj is TipoList) {
                return ((TipoList)obj).tipo;
            } else if (obj is TipoSet) {
                return ((TipoSet)obj).tipo;
            } else if (obj is TipoMAP) {
                return ((TipoMAP)obj).tipoValor;
            }
            else
            {
                arbol.addError("EXCEPTION.NullPointerException", "No se puede hacer un acceso de un tipo: " + obj, fila, columna);
                return obj;
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            Object obj = arbol.entorno.getValorVariable("$" + this.id, arbol, fila, columna);

            if (obj is ListCQL) {
                Object objIndex = expresion.getValor(arbol);
                if (!(objIndex is Int32 || objIndex is Double)) {
                    arbol.addError("EXCEPTION.NullPointerException","El índice del acceso debe de ser de tipo entero o double, vino: "+objIndex,fila,columna);
                    return Catch.EXCEPTION.NullPointerException;
                }
                int index = Convert.ToInt32(objIndex);
                return ((ListCQL)obj).valores[index];
            } else if (obj is SetCQL) {
                Object objIndex = expresion.getValor(arbol);
                if (!(objIndex is Int32 || objIndex is Double))
                {
                    arbol.addError("EXCEPTION.NullPointerException", "El índice del acceso debe de ser de tipo entero o double, vino: " + objIndex, fila, columna);
                    return Catch.EXCEPTION.NullPointerException;
                }
                int index = Convert.ToInt32(objIndex);
                return ((SetCQL)obj).valores[index];
            } else if (obj is MapCQL) {
                Object objIndex = expresion.getValor(arbol);
                if (!((MapCQL)obj).valores.ContainsKey(objIndex)) {
                    arbol.addError("EXCEPTION.NullPointerException", "El map no contiene la clave: " + objIndex, fila, columna);
                    return Catch.EXCEPTION.NullPointerException;
                }
                return ((MapCQL)obj).valores[objIndex];
            }
            else {
                arbol.addError("EXCEPTION.NullPointerException", "No se puede hacer un acceso de un tipo: "+obj,fila,columna);
                return Catch.EXCEPTION.NullPointerException;
            }
        }
    }
}