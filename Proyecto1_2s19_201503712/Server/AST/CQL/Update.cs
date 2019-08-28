using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Update : Sentencia
    {
        public List<AsignacionColumna> asignaciones;
        public String idTabla;
        public Where where;

        public Update(String idTabla, List<AsignacionColumna> asignaciones, Where where,int fila, int columna) {
            this.idTabla = idTabla;
            this.asignaciones = asignaciones;
            this.fila = fila;
            this.columna = columna;
            this.where = where;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.updateTable(this,arbol, fila, columna);
        }
    }
}