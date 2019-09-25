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
            setColumns();
        }

        public void setColumns() {
            //saco las primaryKeys compuestas
            List<String> primarys = new List<string>();
            foreach (ColumnCQL column in this.columnDefinitions) {
                if (column.primaryKeys!=null) {
                    primarys = column.primaryKeys;
                    this.columnDefinitions.Remove(column);
                    break;
                }
            }

            //seteo las primary
            foreach (ColumnCQL column in this.columnDefinitions) {
                if (primarys.Contains(column.id.ToLower())) {
                    column.primaryKey = true;
                }
            }
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.createTable(this,arbol, fila, columna);
        }
    }
}