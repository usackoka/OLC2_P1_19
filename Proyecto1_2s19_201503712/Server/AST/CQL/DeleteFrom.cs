using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class DeleteFrom : Sentencia
    {
        public String idTabla;
        public Where where;

        public DeleteFrom(String idTabla, Where where, int fila, int columna) {
            this.idTabla = idTabla;
            this.where = where;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.deleteFrom(this,arbol);
        }
    }
}