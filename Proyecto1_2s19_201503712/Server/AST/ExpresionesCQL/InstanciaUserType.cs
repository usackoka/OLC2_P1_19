using Server.AST.DBMS;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class InstanciaUserType : Expresion
    {
        String id;

        public InstanciaUserType(String id, int fila, int columna) {
            this.id = id;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return this.id;
        }

        public override object getValor(AST_CQL arbol)
        {
            UserType modeloUt = arbol.dbms.getUserType(this.id,arbol);
            if (modeloUt==null) {
                arbol.addError("UserType","No se encontró el UserType: "+id,fila,columna);
                return Catch.EXCEPTION.TypeDontExists;
            }

            return new UserType(modeloUt, arbol);
        }
    }
}