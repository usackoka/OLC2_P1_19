using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class AlterTableAdd : Sentencia
    {
        public String idTabla;
        public List<ColumnCQL> atributos;

        public AlterTableAdd(String idTabla, List<ColumnCQL> atributos, int fila, int columna) {
            this.idTabla = idTabla;
            this.atributos = atributos;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.alterTableAdd(this, arbol);
        }
    }
}