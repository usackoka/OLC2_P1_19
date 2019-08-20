using Irony.Parsing;
using Server.AST;
using Server.AST.ColeccionesCQL;
using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                    NodoCQL nod = (NodoCQL)recorrido(nodo);
                    if (nod is Funcion)
                    {
                        ast.funciones.Add((Funcion)nod);
                    }
                    else {
                        lista.Add(nod);
                    }
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
            else if (CompararNombre(raiz, "KEY_VALUE_LIST")) {
                List<KeyValuePair<Expresion, Expresion>> lista = new List<KeyValuePair<Expresion, Expresion>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add((KeyValuePair<Expresion, Expresion>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "LISTA_CQLTIPOS")) {
                List<KeyValuePair<String, Object>> list = new List<KeyValuePair<string, object>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    list.Add((KeyValuePair<String, Object>)recorrido(nodo));
                }
                return list;
            }
            else if (CompararNombre(raiz, "CQLTIPO")) {
                // id + TIPO;
                return new KeyValuePair<String, Object>(getLexema(raiz, 0), recorrido(raiz.ChildNodes[1]));
            }
            else if (CompararNombre(raiz, "INE")) {
                return raiz.ChildNodes.Count != 0 ? true : false;
            }
            else if (CompararNombre(raiz, "TYPES")) {
                /*res_create + res_type + INE + id + l_parent + LISTA_CQLTIPOS + r_parent
                | res_alter + res_type + id + res_add + l_parent + LISTA_CQLTIPOS + r_parent
                | res_alter + res_type + id + res_delete + l_parent + LISTA_IDS + r_parent
                | res_delete + res_type + id;*/
                if (ContainsString(getLexema(raiz, 0), "create"))
                {
                    return new CreateUserType(Convert.ToBoolean(recorrido(raiz.ChildNodes[2])), getLexema(raiz, 3),
                        (List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[5]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else {
                    return null;
                }
            }
            else if (CompararNombre(raiz, "KEY_VALUE")) {
                //E dospuntos E
                return new KeyValuePair<Expresion, Expresion>((Expresion)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]));
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
            else if (CompararNombre(raiz, "FUNCION")) {
                //TIPO + id + l_parent + LISTA_PARAMETROS + r_parent + l_llave + BLOCK + r_llave;
                return new Funcion(recorrido(raiz.ChildNodes[0]), getLexema(raiz, 1), (List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[3]),
                    (List<NodoCQL>)recorrido(raiz.ChildNodes[6]), getFila(raiz, 1), getColumna(raiz, 1));
            }
            else if (CompararNombre(raiz, "NATIVAS")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "DATE_NOW")) {
                if (getLexema(raiz, 0).ToLower().Contains("today"))
                {
                    return new TodayNow(Primitivo.TIPO_DATO.DATE, getFila(raiz, 1), getColumna(raiz, 1));
                }
                else {
                    return new TodayNow(Primitivo.TIPO_DATO.TIME, getFila(raiz, 1), getColumna(raiz, 1));
                }
            }
            else if (CompararNombre(raiz, "LLAMADA_FUNCION")) {
                /*id + l_parent + LISTA_E + r_parent
                | res_call + id + l_parent + LISTA_E + r_parent;*/
                if (raiz.ChildNodes.Count == 4)
                {
                    return new LlamadaFuncion(getLexema(raiz, 0), (List<Expresion>)recorrido(raiz.ChildNodes[2]),
                        LlamadaFuncion.TIPO_LLAMADA.LLAMADA, getFila(raiz, 0), getColumna(raiz, 0));
                }
                else {
                    return new LlamadaFuncion(getLexema(raiz, 1), (List<Expresion>)recorrido(raiz.ChildNodes[3]),
                        LlamadaFuncion.TIPO_LLAMADA.CALL, getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "LISTA_PARAMETROS")) {
                List<KeyValuePair<String, Object>> lista = new List<KeyValuePair<string, object>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add((KeyValuePair<string, object>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "UNPARAMETRO")) {
                //TIPO + arroba + id;
                return new KeyValuePair<String, Object>(getLexema(raiz, 2), recorrido(raiz.ChildNodes[0]));
            }
            else if (CompararNombre(raiz, "REFERENCIAS")) {
                List<Object> lista = new List<object>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add(recorrido(nodo));
                }
                return new Referencia(lista, null);
            } else if (CompararNombre(raiz, "REFERENCIA")) {
                //id | arroba + id | LLAMADA_FUNCION | ACCESO_ARR
                if (raiz.ChildNodes[0].ToString().Equals("LLAMADA_FUNCION") || raiz.ChildNodes[0].ToString().Equals("ACCESO_ARR"))
                {
                    return recorrido(raiz.ChildNodes[0]);
                }
                else {
                    return getLexema(raiz, raiz.ChildNodes.Count - 1);
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
                    return tipo;
                }
            }
            else if (CompararNombre(raiz, "INSTRUCCION")) {
                /*
                 INSTRUCCION.Rule = res_log + l_parent + E + r_parent
                        | LISTA_IDS_ARROBA + igual + E
                        //========== ver aquí ambiguedad entre referencias y reasignacion
                        | REFERENCIAS + igual + E
                        | res_return + LISTA_E
                        | REASIGNACION
                        | ACTUALIZACION2
                        | CORTE
                        | REFERENCIAS;*/
                if (raiz.ChildNodes.Count == 4)
                {
                    return new Print((Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else if (raiz.ChildNodes.Count == 3) {
                    List<Object> lista = new List<object>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes[0].ChildNodes)
                    {
                        lista.Add(recorrido(nodo));
                    }
                    return new Referencia(lista, (Expresion)recorrido(raiz.ChildNodes[2]));
                }
                else if (raiz.ChildNodes.Count == 2) {
                    return new Return((List<Expresion>)recorrido(raiz.ChildNodes[1]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (raiz.ChildNodes.Count == 1) {
                    return recorrido(raiz.ChildNodes[0]);
                }
                else {
                    ast.addError("", "RecorridoCQL no soportado: " + raiz.ChildNodes.Count, 0, 0);
                    return "NULL";
                }
            }
            else if (CompararNombre(raiz, "FOR")) {
                //res_for + l_parent + FUENTE_FOR + coma + E + coma + ACTUALIZACION2 + r_parent + l_llave + BLOCK + r_llave;
                return new For((Sentencia)recorrido(raiz.ChildNodes[2]), (Expresion)recorrido(raiz.ChildNodes[4]),
                    (Sentencia)recorrido(raiz.ChildNodes[6]), (List<NodoCQL>)recorrido(raiz.ChildNodes[9]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "COLECCION")) {
                if (CompararNombre(raiz.ChildNodes[1], "KEY_VALUE_LIST")) {
                    return new ValorColeccion((List<KeyValuePair<Expresion, Expresion>>)recorrido(raiz.ChildNodes[1]),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                return new ValorColeccion((List<Expresion>)recorrido(raiz.ChildNodes[1]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "INSTANCIA")) {
                /*res_new + id
                | res_new + res_list + menor_que + TIPO + mayor_que
                | res_new + res_set + menor_que + TIPO + mayor_que
                | res_new + res_map + menor_que + TIPO + coma + TIPO + mayor_que;*/
                if (raiz.ChildNodes.Count == 2) {
                    return new InstanciaUserType(getLexema(raiz,1), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (raiz.ChildNodes.Count == 5)
                {
                    if (ContainsString(getLexema(raiz, 1), "list"))
                    {
                        return new ListCQL(recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                    }
                    else {
                        return new SetCQL(recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                    }
                }
                else if (raiz.ChildNodes.Count == 7) {
                    //res_new + res_map + menor_que + TIPO + coma + TIPO + mayor_que
                    return new MapCQL(recorrido(raiz.ChildNodes[3]), recorrido(raiz.ChildNodes[5]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else {
                    return null;
                }
            }
            else if (CompararNombre(raiz, "ACTUALIZACION2")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "ACTUALIZACION")) {
                /*arroba + id + mas + mas
                | arroba + id + menos + menos;*/
                String operador = "-";
                if (getLexema(raiz, 2).Equals("+")) {
                    operador = "+";
                }
                return new Actualizar(getLexema(raiz, 1), operador, new Primitivo("1 (numero)", 0, 0)
                    , getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "FUENTE_FOR")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "ACTUALIZAR")) {
                //arroba + id + OPERADOR + igual + E;
                return new Actualizar(getLexema(raiz, 1), getLexema(raiz.ChildNodes[2], 0), (Expresion)recorrido(raiz.ChildNodes[4]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "CORTE")) {
                return new Corte(getLexema(raiz, 0), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "REASIGNACION")) {
                //arroba + id + igual + E;
                int fila = getFila(raiz, 0);
                int columna = getColumna(raiz, 0);
                return new Reasignacion(getLexema(raiz, 1), (Expresion)recorrido(raiz.ChildNodes[3]), fila, columna);
            }
            else if (CompararNombre(raiz, "E")) {
                if (raiz.ChildNodes.Count == 3)
                {
                    return new Binaria((Expresion)recorrido(raiz.ChildNodes[0]), getLexema(raiz, 1),
                    (Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else if (raiz.ChildNodes.Count == 5)
                {// E + interrogacion + E + dospuntos + E;
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
            else if (CompararNombre(raiz, "ELSE")) {
                return new Else((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "WHILE")) {
                if (raiz.ChildNodes.Count > 8)
                {
                    return new While((Expresion)recorrido(raiz.ChildNodes[6]), (List<NodoCQL>)recorrido(raiz.ChildNodes[2]),
                        While.TIPO_WHILE.WHILE, getFila(raiz, 0), getColumna(raiz, 0));
                }
                else {

                    return new While((Expresion)recorrido(raiz.ChildNodes[2]), (List<NodoCQL>)recorrido(raiz.ChildNodes[5]),
                        While.TIPO_WHILE.WHILE, getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "SWITCH")) {
                return new Switch((Expresion)recorrido(raiz.ChildNodes[2]), (List<Case>)recorrido(raiz.ChildNodes[5]),
                    recorrido(raiz.ChildNodes[6]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_CASOS")) {
                List<Case> lista = new List<Case>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add((Case)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "CASO_DEF")) {
                if (raiz.ChildNodes.Count != 0)
                {
                    return new Default((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                return null;
            }
            else if (CompararNombre(raiz, "CASO")) {
                return new Case((Expresion)recorrido(raiz.ChildNodes[1]), (List<NodoCQL>)recorrido(raiz.ChildNodes[3]),
                    getFila(raiz, 0), getColumna(raiz, 0));
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

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}