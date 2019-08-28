using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class CreateUser : Sentencia
    {
        String id;
        Expresion contraseña;

        public CreateUser(String id, Expresion contraseña, int fila, int columna) {
            this.id = id;
            this.contraseña = contraseña;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.createUser(this.id, contraseña.getValor(arbol).ToString(),arbol, fila, columna);
        }
    }
}