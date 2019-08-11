using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irony.Parsing;

namespace Server.Analizador
{
    public class GramaticaCQL:Grammar
    {
        public GramaticaCQL() : base(true)
        {
            #region Expresiones Regulares
            var numero = new NumberLiteral("numero");
            var id = TerminalFactory.CreatePythonIdentifier("id");
            //StringLiteral caracter = new StringLiteral("caracter", "\'", StringOptions.IsChar);
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.AllowsAllEscapes);
            StringLiteral cadena2 = new StringLiteral("cadena2", "\'", StringOptions.AllowsAllEscapes);

            //TERMINALES DE COMENTARIO
            CommentTerminal COMENTARIO_LINEA = new CommentTerminal("COMENTARIO_LINEA", "//", "\n", "\r");
            CommentTerminal COMENTARIO_LINEAS = new CommentTerminal("COMENTARIO_LINEAS", "/*", "*/");
            //PARA QUE NO TOME LOS COMENTARIOS COMO TERMINALES
            base.NonGrammarTerminals.Add(COMENTARIO_LINEA);
            base.NonGrammarTerminals.Add(COMENTARIO_LINEAS);
            #endregion

            #region Terminales
            var coma = ToTerm(",");
            var dospuntos = ToTerm(":");
            var l_parent = ToTerm("(");
            var r_parent = ToTerm(")");
            var l_corchete = ToTerm("[");
            var r_corchete = ToTerm("]");
            var l_llave = ToTerm("{");
            var r_llave = ToTerm("}");
            var punto = ToTerm(".");
            var igual = ToTerm("=");

            Terminal
            mas = ToTerm("+"),
            menos = ToTerm("-"),
            por = ToTerm("*"),
            div = ToTerm("/"),
            pot = ToTerm("**"),
            igual_igual = ToTerm("=="),
            not_igual = ToTerm("!="),
            not = ToTerm("!"),
            menor_que = ToTerm("<"),
            mayor_que = ToTerm(">"),
            mayor_igual = ToTerm(">="),
            menor_igual = ToTerm("<="),
            and = ToTerm("&"),
            or = ToTerm("|"),
            xor = ToTerm("^"),
            modular = ToTerm("%");

            KeyTerm
            res_int = ToTerm("int"),
            res_double = ToTerm("double"),
            res_string = ToTerm("string"),
            res_boolean = ToTerm("boolean"),
            res_date = ToTerm("date"),
            res_time = ToTerm("time"),

            res_create = ToTerm("Create"),
            res_type = ToTerm("type"),
            res_user_type = ToTerm("User_Type"),
            res_new = ToTerm("new"),
            res_alter = ToTerm("alter"),
            res_add = ToTerm("add"),
            res_delete = ToTerm("delete"),

            //DDL
            res_database = ToTerm("database"),
            res_if = ToTerm("if"),
            res_not = ToTerm("not"),
            res_exists = ToTerm("exists"),
            res_use = ToTerm("use"),
            res_drop = ToTerm("drop"),
            res_counter = ToTerm("counter"),
            res_primary = ToTerm("primary"),
            res_key = ToTerm("key"),
            res_update = ToTerm("update"),
            res_map = ToTerm("map"),
            res_set = ToTerm("set"),
            res_list = ToTerm("list"),
            res_truncate = ToTerm("truncate"),

            //TCL
            res_commit = ToTerm("commit"),
            res_rollback = ToTerm("rollback"),

            //DCL
            res_with = ToTerm("with"),
            res_password = ToTerm("password"),
            res_grant = ToTerm("grant"),
            res_on = ToTerm("on"),
            res_revoke = ToTerm("revoke"),

            //DML
            res_insert = ToTerm("insert"),
            res_into = ToTerm("into"),
            res_values = ToTerm("values"),
            res_where = ToTerm("where"),
            res_from = ToTerm("from"),
            res_select = ToTerm("select"),
            res_begin = ToTerm("begin"),
            res_bath = ToTerm("batch"),
            res_apply = ToTerm("apply"),
            res_count = ToTerm("count"),
            res_min = ToTerm("min"),
            res_max = ToTerm("max"),
            res_sum = ToTerm("sum"),
            res_avg = ToTerm("avg"),

            //FCL
            res_else = ToTerm("else"),
            res_switch = ToTerm("switch"),
            res_case = ToTerm("case"),
            res_default = ToTerm("default"),
            res_while = ToTerm("while"),
            res_do = ToTerm("do"),
            res_for = ToTerm("for"),
            res_procedure = ToTerm("procedure"),
            res_call = ToTerm("call"),
            res_break = ToTerm("break"),
            res_continue = ToTerm("continue"),
            res_return = ToTerm("return"),
            res_cursor = ToTerm("cursor"),
            res_is = ToTerm("is"),
            res_each = ToTerm("each"),
            res_open = ToTerm("open"),
            res_close = ToTerm("close"),
            res_log = ToTerm("log"),
            res_throw = ToTerm("throw"),
            res_catch = ToTerm("catch"),
            res_try = ToTerm("try");

            MarkReservedWords("int", "double", "string", "boolean", "date", "time",
                        "Create", "type", "res_User_Type", "new", "alter", "add", "delete",
                        "database", "if", "not", "exists", "use", "drop", "counter", "primary", "key", "update", "map", "set", "list", "truncate",
                        "commit", "rollback",
                        "with", "password", "grant", "on", "revoke",
                        "insert", "into", "values", "where", "from", "select", "begin", "batch", "apply", "count", "min", "max", "sum", "avg",
                        "else", "switch", "case", "default", "while", "do", "for", "procedure", "call", "break", "continue",
                        "return", "cursor", "is", "each", "open", "close", "log", "throw", "catch", "try");
            #endregion

            #region No Terminales
            var E = new NonTerminal("E");
            var TERMINO = new NonTerminal("TERMINO");
            var BINARIA = new NonTerminal("BINARIA");
            var E_PARENT = new NonTerminal("E_PARENT");
            var UNARIO = new NonTerminal("UNARIO");
            var OPERADOR = new NonTerminal("OPERADOR", "operator");
            var SENTENCIA = new NonTerminal("SENTENCIA");
            var GLOBAL = new NonTerminal("GLOBAL");
            var BLOCK = new NonTerminal("BLOCK");
            var LISTA_GLOBAL = new NonTerminal("LISTA_GLOBAL");
            var LISTA_IDS = new NonTerminal("LISTA_IDS");
            var LISTA_E = new NonTerminal("LISTA_E");
            var FUNCION = new NonTerminal("FUNCION");
            var LLAMADA_FUNCION = new NonTerminal("LLAMADA_FUNCION");
            var CLASE = new NonTerminal("CLASE");
            var INSTRUCCION = new NonTerminal("INSTRUCCION");
            var ELSEIF = new NonTerminal("ELSEIF");
            var ELSEIFS = new NonTerminal("ELSEIFS");
            var ELSE = new NonTerminal("ELSE");
            var PRIMITIVO = new NonTerminal("PRIMITIVO");
            var NATIVAS = new NonTerminal("NATIVAS");
            var FUENTE_FOR = new NonTerminal("FUENTE_FOR");
            var DECLARACION = new NonTerminal("DECLARACION");
            var COLECCION = new NonTerminal("COLECCION");
            var KEY_VALUE_LIST = new NonTerminal("KEY_VALUE_LIST");
            var KEY_VALUE = new NonTerminal("KEY_VALLUE");
            var REFERENCIAS = new NonTerminal("REFERENCIAS");
            var REFERENCIA = new NonTerminal("REFERENCIA");
            var IMPORT = new NonTerminal("IMPORT");
            var LISTA_CORCHETES = new NonTerminal("LISTA_CORCHETES");
            var ACCESO_ARR = new NonTerminal("ACCESO_ARR");
            var CORCHETES = new NonTerminal("CORCHETES");
            var TIPO = new NonTerminal("TIPO");
            var UNPARAMETRO = new NonTerminal("UNPARAMETRO");
            var LISTA_IDS2 = new NonTerminal("LISTA_IDS2");
            #endregion

            /*
            #region Gramatica
            LISTA_GLOBAL.Rule = MakePlusRule(LISTA_GLOBAL, GLOBAL);

            GLOBAL.Rule = SENTENCIA
                        | FUNCION
                        | DECLARACION + Eos
                        | CLASE
                        | INSTRUCCION + Eos
                        | IMPORT + Eos;

            CLASE.Rule = res_class + id + dospuntos + Eos + BLOCK;

            BLOCK.Rule = Indent + LISTA_GLOBAL + Dedent;

            FUNCION.Rule = TIPO + id + l_parent + LISTA_IDS + r_parent + dospuntos + Eos + BLOCK;
            //FUNCION.NodeCaptionTemplate = "def #{1}(...)";

            LISTA_IDS.Rule = MakeStarRule(LISTA_IDS, coma, UNPARAMETRO);

            LISTA_IDS2.Rule = MakeStarRule(LISTA_IDS2, coma, id);

            UNPARAMETRO.Rule = TIPO + id | res_self;

            DECLARACION.Rule = TIPO + LISTA_IDS2;

            INSTRUCCION.Rule = TIPO + id + igual + E
                        | id + OPERADOR + igual + E
                        | res_print + l_parent + E + r_parent
                        | id + igual + E
                        | REFERENCIAS + igual + E
                        | res_return + E
                        | res_return
                        | REFERENCIAS
                        | res_break
                        | res_continue
                        | res_pass;

            OPERADOR.Rule = mas | menos | modular | por | div | pot;

            TIPO.Rule = res_int
                | res_boolean
                | res_double
                | res_string
                | id
                | res_list + menor_que + TIPO + mayor_que
                | res_tup + menor_que + TIPO + mayor_que
                | res_dic + menor_que + TIPO + coma + TIPO + mayor_que;

            SENTENCIA.Rule = res_if + E + dospuntos + Eos + BLOCK + ELSEIFS + ELSE
                            | res_if + E + dospuntos + Eos + BLOCK + ELSEIFS
                            | res_for + id + res_in + FUENTE_FOR + dospuntos + Eos + BLOCK
                            | res_for + id + res_in + FUENTE_FOR + dospuntos + Eos + BLOCK + ELSE
                            | res_while + E + dospuntos + Eos + BLOCK;

            FUENTE_FOR.Rule = res_range + l_parent + LISTA_E + r_parent
                            | id;

            ELSEIFS.Rule = MakeStarRule(ELSEIFS, ELSEIF);

            ELSEIF.Rule = res_elif + E + dospuntos + Eos + BLOCK;

            ELSE.Rule = res_else + dospuntos + Eos + BLOCK;

            //=========================== EXPRESIONES ===============================================
            E.Rule = TERMINO | UNARIO
                | E + mas + E
                | E + menos + E
                | E + por + E
                | E + div + E
                | E + pot + E
                | E + modular + E
                | E + igual_igual + E
                | E + not_igual + E
                | E + mayor_igual + E
                | E + menor_igual + E
                | E + mayor_que + E
                | E + menor_que + E
                | E + xor + E
                | E + or + E
                | E + and + E;

            TERMINO.Rule = PRIMITIVO | E_PARENT | LLAMADA_FUNCION | NATIVAS | COLECCION | ACCESO_ARR | REFERENCIAS;

            NATIVAS.Rule = res_len + l_parent + E + r_parent
                        | res_bool + l_parent + E + r_parent
                        | res_chr + l_parent + E + r_parent
                        | res_str + l_parent + E + r_parent
                        | res_float + l_parent + E + r_parent
                        | res_type + l_parent + E + r_parent
                        | res_int + l_parent + E + r_parent
                        | res_input + l_parent + E + r_parent;

            REFERENCIA.Rule = id | res_self | LLAMADA_FUNCION | ACCESO_ARR;

            REFERENCIAS.Rule = MakePlusRule(REFERENCIAS, punto, REFERENCIA);

            ACCESO_ARR.Rule = id + LISTA_CORCHETES;

            LISTA_CORCHETES.Rule = MakePlusRule(LISTA_CORCHETES, CORCHETES);

            CORCHETES.Rule = "[" + E + "]";

            //TUPLAS, LISTAS, CONJUNTOS, DICCIONARIOS // PAG 66
            COLECCION.Rule = l_parent + LISTA_E + r_parent | "[" + LISTA_E + "]" | "{" + LISTA_E + "}" | "{" + KEY_VALUE_LIST + "}";

            KEY_VALUE_LIST.Rule = MakePlusRule(KEY_VALUE_LIST, coma, KEY_VALUE);

            KEY_VALUE.Rule = E + dospuntos + E;

            E_PARENT.Rule = l_parent + E + r_parent;

            UNARIO.Rule = mas + E | menos + E | res_not + E;

            PRIMITIVO.Rule = id | numero | cadena | cadena2 | res_true | res_false | res_self | res_none;

            //========================================================================

            LISTA_E.Rule = MakeStarRule(LISTA_E, coma, E);

            LLAMADA_FUNCION.Rule = id + l_parent + LISTA_E + r_parent;
            //LLAMADA_FUNCION.NodeCaptionTemplate = "Llamada #{0}(...)";

            this.Root = LISTA_GLOBAL;
            #endregion
            */

            NonGrammarTerminals.Add(ToTerm(@"\"));

            RegisterOperators(1, Associativity.Left, or);
            RegisterOperators(2, Associativity.Left, and);
            RegisterOperators(3, Associativity.Left, xor);
            RegisterOperators(4, Associativity.Left, igual_igual, not_igual);
            RegisterOperators(6, Associativity.Left, mayor_que, menor_que);
            RegisterOperators(7, Associativity.Left, mas, menos);
            RegisterOperators(8, Associativity.Left, por, div);
            RegisterOperators(9, Associativity.Right, pot, modular);
            RegisterOperators(10, Associativity.Right, not);

            //SIGNOS DE PUNTUACION
            /*MarkPunctuation("(", ")", ":");
            RegisterBracePair("(", ")");*/

            //RECUPERACION
            GLOBAL.ErrorRule = SyntaxError + Eos;
            FUNCION.ErrorRule = SyntaxError + Dedent;

            //REPORTE DE ERRORES
            AddToNoReportGroup("(");
            AddToNoReportGroup(Eos);

        }//constructor
    }
}