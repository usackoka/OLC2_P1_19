using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL.Tipos
{
    public class TipoMAP
    {
        public Object tipoClave;
        public Object tipoValor;

        public TipoMAP() {
            this.tipoValor = new Null();
            this.tipoClave = new Null();
        }

        public TipoMAP(Object tipoClave, Object tipoValor) {
            this.tipoClave = tipoClave;
            this.tipoValor = tipoValor;
        }

        public override string ToString()
        {
            return "MAP<" + this.tipoClave + "," + this.tipoValor+ ">";
        }
    }
}