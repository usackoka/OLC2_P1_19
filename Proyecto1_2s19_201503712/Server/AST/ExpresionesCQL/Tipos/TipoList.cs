using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL.Tipos
{
    public class TipoList
    {
        public Object tipo;

        public TipoList() {
            this.tipo = new Null();
        }

        public TipoList(Object tipo) {
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return "LIST<" + this.tipo+ ">";
        }
    }
}