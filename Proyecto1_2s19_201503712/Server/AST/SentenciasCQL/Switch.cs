using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Switch : Sentencia
    {
        List<Case> cases;
        Expresion match;
        Default def;

        public Switch(Expresion match, List<Case> cases, Object def, int fila, int columna) {
            this.match = match;
            this.cases = cases;
            this.fila = fila;
            this.columna = columna;
            this.def = null;
            if (def!=null) {
                this.def = (Default)def;
            }
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            bool ejecutarDef = true;

            Object matchValue = match.getValor(arbol);
            foreach (Case caso in cases) {
                caso.matchSource = matchValue;
                Object val = caso.Ejecutar(arbol);
                //pregunto si es un continue
                if (val != null) {
                    if (val.Equals(Corte.TIPO_CORTE.CONTINUE))
                    {
                        break;
                    }
                    else if (val.Equals(Corte.TIPO_CORTE.BREAK))
                    {
                        return null;
                    }
                    return val;
                }
                //con un caso que se cumpla, ya no se ejecuta el default
                if (caso.ejecutado)
                    ejecutarDef = false;
            }

            if (def!=null && ejecutarDef) {
                return def.Ejecutar(arbol);
            }
            return null;
        }
    }
}