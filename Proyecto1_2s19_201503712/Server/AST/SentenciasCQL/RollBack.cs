using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class RollBack : Sentencia
    {
        public RollBack(int fila, int columna) {
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return null;
        }
    }
}