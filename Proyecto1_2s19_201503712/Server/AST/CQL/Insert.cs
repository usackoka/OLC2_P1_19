using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Insert : Sentencia
    {
        public List<String> columnNames;
        public List<Expresion> values;
        public String idTabla;

        public Insert(String idTabla,List<String> columnNames, List<Expresion> values, int fila , int columna) {
            this.idTabla = idTabla;
            this.columnNames = columnNames;
            this.values = values;
            this.fila = fila;
            this.columna = columna;
        }

        public Insert(String idTabla, List<Expresion> values, int fila, int columna) {
            this.idTabla = idTabla;
            this.values = values;
            this.fila = fila;
            this.columna = columna;
            this.columnNames = null;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.insertInto(this, arbol);
        }
    }
}