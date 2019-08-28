using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class AlterTableDrop : Sentencia
    {
        public String idTabla;
        public List<String> atributos;

        public AlterTableDrop(String idTabla, List<String> atributos, int fila, int columna) {
            this.idTabla = idTabla;
            this.atributos = atributos;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.alterTableDrop(this,arbol);
        }
    }
}