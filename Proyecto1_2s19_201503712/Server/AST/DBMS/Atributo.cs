using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.DBMS
{
    public class Atributo
    {
        public String id;
        public Object tipoDato;
        public Object valor;

        public Atributo(String id, Object tipoDato, Object valor) {
            this.id = id;
            this.tipoDato = tipoDato;
            this.valor = valor;
        }

        //VALIDAR TIPO
        public void setValor(Object valor, Object tipoDato, AST_CQL arbol) {
            if (this.tipoDato.Equals(tipoDato))
            {
                this.valor = valor;
            }
            else {
                arbol.addError("Atributo: "+id,"No se puede setear un valor de tipo: "+tipoDato+" a un atributo de tipo: "+this.tipoDato,0,0);
            }
        }
    }
}