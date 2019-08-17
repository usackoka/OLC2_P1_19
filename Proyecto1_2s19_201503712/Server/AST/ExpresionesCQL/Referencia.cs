using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Referencia : Expresion
    {
        List<Object> referencias;

        public Referencia(List<Object> referencias) {
            this.referencias = referencias;
        }

        public override object getTipo(AST_CQL arbol)
        {
            if (referencias.Count == 1)
            {
                if (referencias[0] is LlamadaFuncion)
                {
                    return ((LlamadaFuncion)referencias[0]).getTipo(arbol);
                }
            }

            return Primitivo.TIPO_DATO.NULL;
        }

        public override object getValor(AST_CQL arbol)
        {
            if (referencias.Count == 1) {
                if (referencias[0] is LlamadaFuncion) {
                    return ((LlamadaFuncion)referencias[0]).getValor(arbol);
                }
            }

            return Primitivo.TIPO_DATO.NULL;
        }
    }
}