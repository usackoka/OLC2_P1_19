using Server.AST;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class DataBaseCHISON : Sentencia
    {
        public DataBaseCHISON() {
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }
    }
}