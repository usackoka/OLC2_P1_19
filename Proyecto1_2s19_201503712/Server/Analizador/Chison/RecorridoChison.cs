using Irony.Parsing;
using Server.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.Analizador.Chison
{
    public class RecorridoChison
    {
        AST_CQL arbol;

        public RecorridoChison(ParseTreeNode padre, AST_CQL arbol)
        {
            this.arbol = arbol;
            recorrido(padre);
        }

        Object recorrido(ParseTreeNode raiz)
        {
            if (CompararNombre(raiz, "S"))
            {
                // dolar + menor_que + DATABASES + coma + USERS + mayor_que + dolar
                arbol.nodosChison.AddRange((List<NodoCQL>)recorrido(raiz.ChildNodes[2]));
                arbol.nodosChison.AddRange((List<NodoCQL>)recorrido(raiz.ChildNodes[4]));
                return arbol;
            }
            else if (CompararNombre(raiz, "DATABASES"))
            {
                // res_databases + igual + l_corchete + LISTA_BASES + r_corchete;
                return recorrido(raiz.ChildNodes[3]);
            }
            else if (CompararNombre(raiz, "LISTA_BASES"))
            {
                List<NodoCQL> lista = new List<NodoCQL>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((NodoCQL)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "BASE"))
            {
                /*menor_que + res_name + igual + cadena + coma + 
                res_data + igual + l_corchete + LISTA_DATA_BASE + r_corchete + mayor_que;*/
                return new DataBaseCHISON(getLexema(raiz,3).Replace("\"",""),(List<List<Data_Base>>)recorrido(raiz.ChildNodes[8]),
                    getFila(raiz,0),getColumna(raiz,0));
            }
            else
            {
                return null;
            }
        }

        bool CompararNombre(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        string getLexema(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Text.ToLower();
        }

        int getFila(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Line;
        }

        int getColumna(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Column;
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}