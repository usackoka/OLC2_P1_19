using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class DropTable : Sentencia
    {
        public Boolean IfExists;
        public String id;
        public DropTable(Boolean IfExists, String id, int fila, int columna) {
            this.IfExists = IfExists;
            this.id = id;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.dropTable(this,arbol);
        }
    }
}