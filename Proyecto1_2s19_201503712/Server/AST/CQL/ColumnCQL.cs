using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class ColumnCQL
    {
        int fila, columna;
        public List<Object> valores;

        public String id;
        public Object tipoDato;
        public Boolean primaryKey;
        public ColumnCQL(String id, Object tipoDato, Boolean primaryKey ,int fila, int columna) {
            this.id = id;
            this.tipoDato = tipoDato;
            this.primaryKey = primaryKey;
            this.fila = fila;
            this.columna = columna;
            this.primaryKeys = null;
            this.valores = null;
        }

        public List<String> primaryKeys;
        public ColumnCQL(List<String> primaryKeys, int fila, int columna) {
            this.primaryKeys = primaryKeys;
            this.fila = fila;
            this.columna = columna;
        }

        //para la copia
        public ColumnCQL(ColumnCQL copia) {
            this.id = copia.id;
            this.tipoDato = copia.tipoDato;
            this.primaryKey = copia.primaryKey;
            this.fila = copia.fila;
            this.columna = copia.columna;
            this.primaryKeys = copia.primaryKeys;
            this.valores = copia.valores;
        }
    }
}