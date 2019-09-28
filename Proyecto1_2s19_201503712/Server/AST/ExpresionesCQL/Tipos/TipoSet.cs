using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL.Tipos
{
    public class TipoSet
    {
        public Object tipo;

        public TipoSet()
        {
            this.tipo = new Null();
        }

        public TipoSet(Object tipo) {
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return "SET<" + this.tipo + ">";
        }
    }
}