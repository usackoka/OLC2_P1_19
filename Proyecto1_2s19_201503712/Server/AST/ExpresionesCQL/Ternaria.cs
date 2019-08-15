using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Ternaria : Expresion
    {
        Expresion condicion, expVerdadero, expFalso;

        public Ternaria(Expresion condicion, Expresion expVerdadero, Expresion expFalso, int fila, int columna) {
            this.condicion = condicion;
            this.expVerdadero = expVerdadero;
            this.expFalso = expFalso;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            if (Convert.ToBoolean(condicion.getValor(arbol)))
            {
                return expVerdadero.getTipo(arbol);
            }
            else
            {
                return expFalso.getTipo(arbol);
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            if (Convert.ToBoolean(condicion.getValor(arbol)))
            {
                return expVerdadero.getValor(arbol);
            }
            else {
                return expFalso.getValor(arbol);
            }
        }
    }
}