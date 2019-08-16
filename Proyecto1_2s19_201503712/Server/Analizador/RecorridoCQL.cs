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
            else if (CompararNombre(raiz, "GLOBAL")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "DECLARACION")) {
                return new Declaracion(recorrido(raiz.ChildNodes[0]), (List<KeyValuePair<String, Expresion>>)recorrido(raiz.ChildNodes[1]), getFila(raiz.ChildNodes[0], 0), getColumna(raiz.ChildNodes[0], 0));
            }
            else if (CompararNombre(raiz, "LISTA_DECLARACION_E")) {
                List<KeyValuePair<String, Expresion>> lista = new List<KeyValuePair<String, Expresion>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add((KeyValuePair<String, Expresion>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "DECLARACION_E")) {
                if (raiz.ChildNodes.Count == 4)
                {
                    return new KeyValuePair<String, Expresion>(getLexema(raiz, 1), (Expresion)recorrido(raiz.ChildNodes[3]));
                }
                else {
                    return new KeyValuePair<String, Expresion>(getLexema(raiz, 1), null);
                }
            }
            else if (CompararNombre(raiz, "TIPO")) {
                String tipo = getLexema(raiz, 0);
                if (tipo.Equals("string", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.STRING;
                }
                else if (tipo.Equals("int", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.INT;
                }
                else if (tipo.Equals("boolean", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.BOOLEAN;
                }
                else if (tipo.Equals("double", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.DOUBLE;
                }
                else if (tipo.Equals("date", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.DATE;
                }
                else if (tipo.Equals("time", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.TIME;
                }
                else if (tipo.Equals("list", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.LIST;
                }
                else if (tipo.Equals("set", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.SET;
                }
                else if (tipo.Equals("map", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.MAP;
                }
                else if (tipo.Equals("counter", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.COUNTER;
                }
                else {
                    return Primitivo.TIPO_DATO.STRUCT;
                }
            }
            else if (CompararNombre(raiz, "INSTRUCCION")) {
                if (raiz.ChildNodes.Count == 4)
                {
                    return new Print((Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else {
                    ast.addError("", "RecorridoCQL no soportado: " + raiz.ChildNodes.Count, 0, 0);
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
            else if (CompararNombre(raiz, "SENTENCIA")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "IF")) {
                /*res_if + l_parent + E + r_parent + l_llave + BLOCK + r_llave + ELSEIFS + ELSE
                            | res_if + l_parent + E + r_parent + l_llave + BLOCK + r_llave + ELSEIFS;*/
                Else else_ = null;
                if (raiz.ChildNodes.Count == 9) {
                    else_ = (Else)recorrido(raiz.ChildNodes[8]);
                }
                return new If((Expresion)recorrido(raiz.ChildNodes[2]), (List<NodoCQL>)recorrido(raiz.ChildNodes[5]),
                    (List<ElseIf>)recorrido(raiz.ChildNodes[7]), else_,
                     getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "ELSEIFS")) {
                List<ElseIf> lista = new List<ElseIf>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add((ElseIf)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "ELSEIF")) {
                return new ElseIf((Expresion)recorrido(raiz.ChildNodes[2]), (List<NodoCQL>)recorrido(raiz.ChildNodes[5]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz,"ELSE")) {
                return new Else((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "PRIMITIVO"))
            {
                String valor = raiz.ChildNodes[0].ToString();
                if (raiz.ChildNodes.Count == 2) {
                    valor = raiz.ChildNodes[1].ToString();
                }
                return new Primitivo(valor, getFila(raiz, 0), getColumna(raiz, 0));
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
                ast.addError("", "RecorridoCQL no soportado: " + raiz.ToString(), 0, 0);
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