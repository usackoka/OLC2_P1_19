using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public abstract class Expresion:NodoCQL
    {
        public abstract Object getValor(AST_CQL arbol);
        public abstract Object getTipo(AST_CQL arbol);
    }
}