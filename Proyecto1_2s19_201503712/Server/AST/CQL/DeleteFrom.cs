using Server.AST.ExpresionesCQL;
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
        public AccesoArreglo acceso;

        public DeleteFrom(AccesoArreglo acceso, String idTabla, Where where, int fila, int columna) {
            this.acceso = acceso;
            this.idTabla = idTabla;
            this.where = where;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.deleteFrom(this,arbol, fila, columna);
        }
    }
}