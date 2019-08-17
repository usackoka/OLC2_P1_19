using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Corte : Sentencia
    {
        String corte;
        public Corte(String corte, int fila, int columna) {
            this.corte = corte;
            this.fila = fila;
            this.columna = columna;
        }

        public enum TIPO_CORTE {
            BREAK, CONTINUE
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            if (ContainsString(this.corte, "break"))
            {
                return TIPO_CORTE.BREAK;
            }
            else {
                return TIPO_CORTE.CONTINUE;
            }
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}