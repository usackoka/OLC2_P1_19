using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Commit : Sentencia
    {
        public bool batch;

        public Commit(int fila, int columna, Boolean batch) {
            this.fila = fila;
            this.columna = columna;
            this.batch = batch;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (batch)
            {
                arbol.dbms.createChisons("batch");
            }
            else
            {
                arbol.dbms.createChisons("");
            }
            return null;
        }
    }
}