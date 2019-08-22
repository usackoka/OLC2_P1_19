using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Grant : Sentencia
    {
        String idUser;
        String idBd;

        public Grant(String idUser, String idBd, int fila, int columna) {
            this.idUser = idUser;
            this.idBd = idBd;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.grantUser(idUser, idBd);
        }
    }
}