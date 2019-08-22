using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Revoke : Sentencia
    {
        String idUser;
        String idBd;
        public Revoke(String idUser, String idBd, int fila, int columna) {
            this.idUser = idUser;
            this.idBd = idBd;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.revokeUser(idUser, idBd);
        }
    }
}