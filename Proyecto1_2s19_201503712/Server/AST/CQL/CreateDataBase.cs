using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class CreateDataBase:Sentencia
    {
        public Boolean IfNotExists;
        public String id;

        public CreateDataBase(Boolean IfNotExists, String id, int fila, int columna) {
            this.IfNotExists = IfNotExists;
            this.id = id;
            this.columna = columna;
            this.fila = fila;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.createDataBase(this,arbol);
        }
    }
}