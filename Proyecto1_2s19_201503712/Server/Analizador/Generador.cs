using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador
{
    public class Generador
    {
        public List<clsToken> ListaErrores { get; set; }
        public ParseTree padre { get; set; }
        ParseTree arbol;

        public Generador()
        {
            ListaErrores = new List<clsToken>();
        }

        public bool esCadenaValida(string cadenaEntrada, Grammar gramatica)
        {
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser p = new Parser(lenguaje);

            arbol = p.Parse(cadenaEntrada);

            padre = arbol;

            Errores();

            if (arbol.Root != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void Errores()
        {
            for (int i = 0; i < arbol.ParserMessages.Count; i++)
            {
                String Tipo = "";
                int Linea = (arbol.ParserMessages.ElementAt(i).Location.Line + 1);
                int Columna = arbol.ParserMessages.ElementAt(i).Location.Column;
                String Descripcion = arbol.ParserMessages.ElementAt(i).Message;

                Descripcion = Descripcion.Replace("unexpected", "se-esperaba");
                if (Descripcion.Contains("Syntax error,"))
                {
                    Tipo = "Error Sintactico";
                    Descripcion = Descripcion.Replace("Syntax error,", "");
                }
                else
                {
                    Tipo = "Error Lexico";
                    Descripcion = Descripcion.Replace("Invalid character:", "");
                }
                clsToken error = new clsToken("", Descripcion, Linea, Columna, Tipo, "");
                ListaErrores.Add(error);
            }
        }
    }
}