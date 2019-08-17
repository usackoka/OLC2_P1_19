using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Return : Sentencia
    {
        List<Expresion> expresiones;
        public Return(List<Expresion> expresiones, int fila, int columna) {
            this.expresiones = expresiones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (expresiones.Count == 0) {
                return null;
            }
            else if (expresiones.Count == 1)
            {
                return this.expresiones[0].getValor(arbol);
            }
            else {//para los procedures//pendiente
                return null; 
            }
        }
    }
}