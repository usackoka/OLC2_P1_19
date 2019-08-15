using Irony.Parsing;
using Server.AST;
using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;

namespace Server.Analizador
{
    public class RecorridoCQL
    {
        public AST_CQL ast { get; set; }

        public RecorridoCQL(ParseTreeNode padre)
        {
            ast = new AST_CQL();
            ast.nodos = (List<NodoCQL>)recorrido(padre);
        }

        Object recorrido(ParseTreeNode raiz)
        {
            if (CompararNombre(raiz, "BLOCK"))
            {
                List<NodoCQL> lista = new List<NodoCQL>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((NodoCQL)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz,"GLOBAL")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "INSTRUCCION")) {
                if (raiz.ChildNodes.Count == 4)
                {
                    return new Print((Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else {
                    ast.addError("","RecorridoCQL no soportado: " + raiz.ChildNodes.Count,0,0);
                    return "NULL";
                }
            }
            else if (CompararNombre(raiz, "E")) {
                if (raiz.ChildNodes.Count == 3)
                {
                    return new Binaria((Expresion)recorrido(raiz.ChildNodes[0]), getLexema(raiz, 1),
                    (Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else if (raiz.ChildNodes.Count == 5)
                {
                    return new Ternaria((Expresion)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]),
                        (Expresion)recorrido(raiz.ChildNodes[4]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else {
                    return recorrido(raiz.ChildNodes[0]);
                }
            }
            else if (CompararNombre(raiz, "TERMINO"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "PRIMITIVO"))
            {
                return new Primitivo(raiz.ChildNodes[0].ToString(), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "E_PARENT"))
            {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "OPERADOR"))
            {
                if (raiz.ChildNodes.Count == 2)
                {
                    return getLexema(raiz, 0) + getLexema(raiz, 1);
                }
                else
                {
                    return getLexema(raiz, 0);
                }
            }
            else if (CompararNombre(raiz, "UNARIO"))
            {
                return new Unario(getLexema(raiz, 0), (Expresion)recorrido(raiz.ChildNodes[1]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_E"))
            {
                List<Expresion> lista = new List<Expresion>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((Expresion)recorrido(nodo));
                }
                return lista;
            }
            else {
                ast.addError("","RecorridoCQL no soportado: " + raiz.ToString(),0,0);
                return "NULL";
            }
        }

        bool CompararNombre(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        string getLexema(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Text;
        }

        int getFila(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Line;
        }

        int getColumna(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Column;
        }
    }
}