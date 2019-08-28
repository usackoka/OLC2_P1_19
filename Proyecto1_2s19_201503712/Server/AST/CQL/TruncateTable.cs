using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class TruncateTable : Sentencia
    {
        String id;
        public TruncateTable(String id, int fila, int columna) {
            this.id = id;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.truncateTable(this.id,arbol, fila, columna);
        }
    }
}