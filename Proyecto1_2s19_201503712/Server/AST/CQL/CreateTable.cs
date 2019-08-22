using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class CreateTable : Sentencia
    {
        public Boolean IfNotExists;
        public List<ColumnCQL> columnDefinitions;
        public String id;

        public CreateTable(Boolean IfNotExists, String id, List<ColumnCQL> columnDefinitions, 
            int fila, int columna) {
            this.IfNotExists = IfNotExists;
            this.id = id;
            this.columnDefinitions = columnDefinitions;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.createTable(this);
        }
    }
}