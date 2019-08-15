using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public abstract class Sentencia:NodoCQL
    {
        public abstract Object Ejecutar(AST_CQL arbol);
    }
}