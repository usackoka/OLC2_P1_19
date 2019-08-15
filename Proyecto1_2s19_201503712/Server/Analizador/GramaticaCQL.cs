﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irony.Parsing;

namespace Server.Analizador
{
    public class GramaticaCQL:Grammar
    {
        public GramaticaCQL() : base(false)
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
            var puntocoma = ToTerm(";");
            var dospuntos = ToTerm(":");
            var l_parent = ToTerm("(");
            var r_parent = ToTerm(")");
            var l_corchete = ToTerm("[");
            var r_corchete = ToTerm("]");
            var l_llave = ToTerm("{");
            var r_llave = ToTerm("}");
            var punto = ToTerm(".");
            var igual = ToTerm("=");
            var arroba = ToTerm("@");
            var interrogacion = ToTerm("?");

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
            and = ToTerm("&&"),
            or = ToTerm("||"),
            xor = ToTerm("^"),
            modular = ToTerm("%");

            KeyTerm
            res_int = ToTerm("int"),
            res_double = ToTerm("double"),
            res_string = ToTerm("string"),
            res_boolean = ToTerm("boolean"),
            res_date = ToTerm("date"),
            res_time = ToTerm("time"),
            res_null = ToTerm("null"),

            res_create = ToTerm("Create"),
            res_table = ToTerm("Table"),
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
            res_update = ToTerm("UPDATE"),
            res_map = ToTerm("map"),
            res_set = ToTerm("set"),
            res_list = ToTerm("list"),
            res_truncate = ToTerm("truncate"),

            //TCL
            res_commit = ToTerm("commit"),
            res_rollback = ToTerm("rollback"),

            //DCL
            res_with = ToTerm("with"),
            res_user = ToTerm("user"),
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
            res_order = ToTerm("order"),
            res_by = ToTerm("by"),
            res_limit = ToTerm("limit"),
            res_asc = ToTerm("asc"),
            res_desc = ToTerm("desc"),
            res_in = ToTerm("in"),
            res_true = ToTerm("true"),
            res_false = ToTerm("false"),
            res_today = ToTerm("Today"),
            res_now = ToTerm("now"),
            res_try = ToTerm("try"),
            res_elseif = ToTerm("else if"),

            //excepciones
            res_ArithmeticException = ToTerm("ArithmeticException"),
            res_TypeAlreadyExists = ToTerm("TypeAlreadyExists"),
            res_TypeDontExists = ToTerm("TypeDontExists"),
            res_BDAlreadyExists = ToTerm("BDAlreadyExists"),
            res_BDDontExists = ToTerm("BDDontExists"),
            res_UseBDException = ToTerm("UseBDException"),
            res_TableAlreadyExists = ToTerm("TableAlreadyExists"),
            res_TableDontExists = ToTerm("TableDontExists"),
            res_CounterTypeException = ToTerm("CounterTypeException"),
            res_UserAlreadyExists = ToTerm("UserAlreadyExists"),
            res_UserDontExists = ToTerm("UserDontExists"),
            res_ValuesException = ToTerm("ValuesException"),
            res_ColumnException = ToTerm("ColumnException"),
            res_BatchException = ToTerm("BatchException"),
            res_IndexOutException = ToTerm("IndexOutException"),
            res_NullPointerException = ToTerm("NullPointerException"),
            res_NumberReturnsException = ToTerm("NumberReturnsException"),
            res_FunctionAlreadyExists = ToTerm("FunctionAlreadyExists"),
            res_ProcedureAlreadyExists = ToTerm("ProcedureAlreadyExists"),
            res_ObjectAlreadyExists = ToTerm("ObjectAlreadyExists");

            MarkReservedWords("ArithmeticException", "TableAlreadyExists", "UseBDException", "BDDontExists", "BDAlreadyExists",
                        "TypeDontExists", "TypeAlreadyExists", "TableDontExists", "CounterTypeException","UserAlreadyExists",
                        "UserDontExists","ValuesException","ColumnException","BatchException","IndexOutException","NullPointerException",
                        "NumberReturnsException","FunctionAlreadyExists","ProcedureAlreadyExists","ObjectAlreadyExists",
                        "int", "double", "string", "boolean", "date", "time",
                        "Create", "type", "res_User_Type", "new", "alter", "add", "delete",
                        "database", "if", "not", "exists", "use", "drop", "counter", "primary", "key", "update", "map", "set", "list", "truncate",
                        "commit", "rollback", "null","table","user","order","by","limit","asc","desc",
                        "with", "password", "grant", "on", "revoke", "in", "Today", "now",
                        "insert", "into", "values", "where", "from", "select", "begin", "batch", "apply", "count", "min", "max", "sum", "avg",
                        "else", "switch", "case", "default", "while", "do", "for", "procedure", "call", "break", "continue",
                        "return", "cursor", "is", "each", "open", "close", "log", "throw", "catch", "try", "else if");
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
            var LISTA_PARAMETROS = new NonTerminal("LISTA_PARAMETROS");
            var LISTA_E = new NonTerminal("LISTA_E");
            var FUNCION = new NonTerminal("FUNCION");
            var LLAMADA_FUNCION = new NonTerminal("LLAMADA_FUNCION");
            var CURSOR = new NonTerminal("CURSOR");
            var INSTRUCCION = new NonTerminal("INSTRUCCION");
            var ELSEIF = new NonTerminal("ELSEIF");
            var ELSEIFS = new NonTerminal("ELSEIFS");
            var ELSE = new NonTerminal("ELSE");
            var PRIMITIVO = new NonTerminal("PRIMITIVO");
            var NATIVAS = new NonTerminal("NATIVAS");
            var FUENTE_FOR = new NonTerminal("FUENTE_FOR");
            var DECLARACION = new NonTerminal("DECLARACION");
            var COLECCION = new NonTerminal("COLECCION");
            var REFERENCIAS = new NonTerminal("REFERENCIAS");
            var REFERENCIA = new NonTerminal("REFERENCIA");
            var LISTA_CORCHETES = new NonTerminal("LISTA_CORCHETES");
            var ACCESO_ARR = new NonTerminal("ACCESO_ARR");
            var CORCHETES = new NonTerminal("CORCHETES");
            var TIPO = new NonTerminal("TIPO");
            var UNPARAMETRO = new NonTerminal("UNPARAMETRO");
            var LISTA_IDS_ARROBA = new NonTerminal("LISTA_IDS_ARROBA");
            var LISTA_IDS = new NonTerminal("LISTA_IDS");
            var TYPES = new NonTerminal("TYPES");
            var INE = new NonTerminal("INE");
            var IE = new NonTerminal("IE");
            var CASTEOS = new NonTerminal("CASTEOS");
            var DDL = new NonTerminal("DDL");
            var LISTA_COLDEF = new NonTerminal("LISTA_COLDEF");
            var COLDEF = new NonTerminal("COLDEF");
            var LISTA_CQLTIPOS = new NonTerminal("LISTA_CQLTIPOS");
            var CQLTIPO = new NonTerminal("CQLTIPO");
            var TCL = new NonTerminal("TCL");
            var DCL = new NonTerminal("DCL");
            var DML = new NonTerminal("DML");
            var ASIG_CQL = new NonTerminal("ASIG_CQL");
            var LISTA_ASIG_CQL = new NonTerminal("LISTA_ASIG_CQL");
            var WHERE = new NonTerminal("WHERE");
            var ORDERBY = new NonTerminal("ORDERBY");
            var LIMIT = new NonTerminal("LIMIT");
            var ORDER = new NonTerminal("ORDER");
            var WHERE_Q = new NonTerminal("WHERE_Q");
            var LIMIT_Q = new NonTerminal("LIMIT_Q");
            var ORDERBY_Q = new NonTerminal("ORDERBY_Q");
            var LISTA_ORDER = new NonTerminal("LISTA_ORDER");
            var SELECT_TYPE = new NonTerminal("SELECT_TYPE");
            var SELECT = new NonTerminal("SELECT");
            var BATCH = new NonTerminal("BATCH");
            var LISTA_DML = new NonTerminal("LISTA_DML");
            var FUN_AGR = new NonTerminal("FUN_AGR");
            var TIPO_AGR = new NonTerminal("TIPO_AGR");
            var KEY_VALUE_LIST = new NonTerminal("KEY_VALUE_LIST");
            var KEY_VALUE = new NonTerminal("KEY_VALUE");
            var INSTANCIA = new NonTerminal("INSTANCIA");
            var PROCEDURE = new NonTerminal("PROCEDURE");
            var EXCEPTION = new NonTerminal("EXCEPTION");
            var LISTA_SELECT = new NonTerminal("LISTA_SELECT");
            var SEL = new NonTerminal("SEL");
            var LISTA_CASOS = new NonTerminal("LISTA_CASOS");
            var CASOS = new NonTerminal("CASOS");
            var CASO_DEF = new NonTerminal("CASO_DEF");
            var CASO = new NonTerminal("CASO");
            #endregion

            #region Gramatica
            BLOCK.Rule = MakePlusRule(BLOCK, GLOBAL);

            GLOBAL.Rule = SENTENCIA
                        | FUNCION
                        | PROCEDURE
                        | BATCH
                        | TYPES + puntocoma
                        | FUN_AGR + puntocoma
                        | DECLARACION + puntocoma
                        | INSTRUCCION + puntocoma
                        | DML + puntocoma
                        | DCL + puntocoma
                        | TCL + puntocoma
                        | DDL + puntocoma
                        | CURSOR + puntocoma
                        | SELECT + puntocoma;

            CURSOR.Rule = res_cursor + arroba + id + res_is + SELECT
                | res_open + arroba + id
                | res_close + arroba + id;

            PROCEDURE.Rule = res_procedure + id + l_parent + LISTA_PARAMETROS + r_parent + coma + 
                l_parent + LISTA_PARAMETROS + r_parent + l_llave + BLOCK + r_llave;

            FUNCION.Rule = TIPO + id + l_parent + LISTA_PARAMETROS + r_parent + l_llave + BLOCK + r_llave;
            //FUNCION.NodeCaptionTemplate = "def #{1}(...)";

            //=================== OTROS CQL =====================================
            BATCH.Rule = res_begin + res_bath + LISTA_DML + res_apply + res_bath;

            FUN_AGR.Rule = TIPO_AGR + l_parent + menor_que + SELECT + mayor_que + r_parent;

            TIPO_AGR.Rule = res_count
                | res_min
                | res_max
                | res_sum
                | res_avg;

            //===================== DDL ==========================================
            DDL.Rule = res_create + res_database + INE + id
                | res_create + res_table + INE + id + l_parent + LISTA_COLDEF + r_parent
                | res_alter + res_table + id + res_add + LISTA_CQLTIPOS
                | res_alter + res_table + id + res_drop + LISTA_IDS
                | res_drop + res_table + IE + id
                | res_truncate + res_table + id
                | res_use + id
                | res_drop + res_database + id;

            //================== DML =============================================
            LISTA_DML.Rule = MakeStarRule(LISTA_DML, DML);

            DML.Rule = res_insert + res_into + id + res_values + l_parent + LISTA_E + r_parent
                | res_insert + res_into + id + l_parent + LISTA_IDS_ARROBA + r_parent + res_values + l_parent + LISTA_E + r_parent
                | res_update + id + res_set + LISTA_ASIG_CQL + WHERE_Q
                | res_delete + res_from + id + WHERE_Q;

            SELECT.Rule = res_select + SELECT_TYPE + res_from + id + WHERE_Q + ORDERBY_Q + LIMIT_Q;

            SELECT_TYPE.Rule = LISTA_SELECT
                | l_parent + por + r_parent;

            LISTA_SELECT.Rule = MakePlusRule(LISTA_SELECT, coma, SEL);

            SEL.Rule = id + punto + id
                | id;

            LIMIT_Q.Rule = LIMIT
                | Empty;

            LIMIT.Rule = res_limit + E;

            ORDERBY_Q.Rule = ORDERBY
                | Empty;

            ORDERBY.Rule = res_order + res_by + LISTA_ORDER;

            LISTA_ORDER.Rule = MakePlusRule(LISTA_ORDER, coma, ORDER);

            ORDER.Rule = id
                | id + res_desc
                | id + res_asc;

            WHERE_Q.Rule = WHERE
                | Empty;

            WHERE.Rule = res_where + E; 

            LISTA_ASIG_CQL.Rule = MakePlusRule(LISTA_ASIG_CQL,coma,ASIG_CQL);

            ASIG_CQL.Rule = id + igual + E;

            //=================== DCL =============================================
            DCL.Rule = res_create + res_user + id + res_with + res_password + E
                | res_grant + id + res_on + id
                | res_revoke + id + res_on + id;

            //=================== TCL ============================================
            TCL.Rule = res_commit
                | res_rollback;

            //=================== TYPES =====================================
            TYPES.Rule = res_create + res_type + INE + id + l_parent + LISTA_CQLTIPOS + r_parent
                | res_alter + res_type + id + res_add + l_parent + LISTA_CQLTIPOS + r_parent
                | res_alter + res_type + id + res_delete + l_parent + LISTA_IDS + r_parent
                | res_delete + res_type + id;

            INE.Rule = res_if + res_not + res_exists
                | Empty;

            IE.Rule = res_if + res_exists
                | Empty;

            LISTA_COLDEF.Rule = MakeStarRule(LISTA_COLDEF, coma, COLDEF);

            COLDEF.Rule = id + TIPO + res_primary + res_key
                | id + TIPO
                | res_primary + res_key + l_parent + LISTA_IDS + r_parent;

            //============ lista CQL TIPOS
            LISTA_CQLTIPOS.Rule = MakeStarRule(LISTA_CQLTIPOS, coma, CQLTIPO);

            CQLTIPO.Rule = id + TIPO;

            //============ lista parametros
            LISTA_PARAMETROS.Rule = MakeStarRule(LISTA_PARAMETROS, coma, UNPARAMETRO);

            UNPARAMETRO.Rule = TIPO + arroba + id;

            //============ LISTA IDS CON ARROBA
            LISTA_IDS_ARROBA.Rule = MakeStarRule(LISTA_IDS_ARROBA, coma, arroba + id);

            //============ LISTA IDS
            LISTA_IDS.Rule = MakeStarRule(LISTA_IDS, coma, id);

            DECLARACION.Rule = TIPO + LISTA_IDS_ARROBA;

            INSTRUCCION.Rule = TIPO + arroba + id + igual + E
                        | arroba + id + OPERADOR + igual + E
                        | res_log + l_parent + E + r_parent
                        | LISTA_IDS_ARROBA + igual + E
                        //| id + igual + E ===> se incluye en REFERENCIAS
                        | REFERENCIAS + igual + E
                        | res_return + LISTA_E
                        | res_break
                        | res_continue
                        | REFERENCIAS;

            OPERADOR.Rule = mas | menos | modular | por | div;

            TIPO.Rule = res_int
                | res_boolean
                | res_double
                | res_string
                | res_counter
                | res_set
                | res_map
                | res_list
                | id
                | res_date
                | res_time;

            SENTENCIA.Rule = res_if + l_parent + E + r_parent + l_llave + BLOCK + r_llave + ELSEIFS + ELSE
                            | res_if + l_parent + E + r_parent + l_llave + BLOCK + r_llave + ELSEIFS
                            | res_for + l_parent + r_parent + l_llave + BLOCK + r_llave
                            | res_while + l_parent + E + r_parent + l_llave + BLOCK + r_llave
                            | res_switch + l_parent + E + r_parent + l_llave + LISTA_CASOS + CASO_DEF + r_llave
                            | res_do + l_llave + BLOCK + r_llave + res_while + l_parent + E + r_parent + puntocoma
                            | res_try + l_llave + BLOCK + r_llave + res_catch + l_parent + EXCEPTION + arroba 
                                    + id + r_parent + l_llave + BLOCK + r_llave;

            LISTA_CASOS.Rule = MakePlusRule(LISTA_CASOS,CASO);

            CASO.Rule = res_case + E + dospuntos + BLOCK;

            CASO_DEF.Rule = Empty
                | res_default + dospuntos + BLOCK;

            EXCEPTION.Rule = res_ArithmeticException
                | res_CounterTypeException
                | res_UserAlreadyExists
                | res_UserDontExists
                | res_ValuesException
                | res_ColumnException
                | res_BatchException
                | res_IndexOutException
                | res_NullPointerException
                | res_NumberReturnsException
                | res_FunctionAlreadyExists
                | res_ProcedureAlreadyExists
                | res_ObjectAlreadyExists
                | res_TypeAlreadyExists
                | res_TypeDontExists
                | res_BDAlreadyExists
                | res_BDDontExists
                | res_UseBDException
                | res_TableAlreadyExists
                | res_TableDontExists;

            /*
            FUENTE_FOR.Rule = res_range + l_parent + LISTA_E + r_parent
                            | id;
            */

            ELSEIFS.Rule = MakeStarRule(ELSEIFS, ELSEIF);

            ELSEIF.Rule = res_elseif + l_parent + E + r_parent + l_llave + BLOCK + r_llave;

            ELSE.Rule = res_else + l_llave  + BLOCK + r_llave;

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
                | E + and + E
                | E + res_in + E
                | E + interrogacion + E + dospuntos + E;

            TERMINO.Rule = PRIMITIVO | E_PARENT | NATIVAS | COLECCION | REFERENCIAS | CASTEOS | INSTANCIA;

            INSTANCIA.Rule = res_new + id;

            CASTEOS.Rule = l_parent + TIPO + r_parent + E;

            NATIVAS.Rule = res_today + l_parent + r_parent
                | res_now + l_parent + r_parent
                | res_throw + res_new + EXCEPTION;

            REFERENCIA.Rule = id | arroba + id | LLAMADA_FUNCION | ACCESO_ARR;

            REFERENCIAS.Rule = MakePlusRule(REFERENCIAS, punto, REFERENCIA);

            ACCESO_ARR.Rule = id + LISTA_CORCHETES;

            LISTA_CORCHETES.Rule = MakePlusRule(LISTA_CORCHETES, CORCHETES);

            CORCHETES.Rule = l_corchete + E + r_corchete;

            //TUPLAS, LISTAS, CONJUNTOS, DICCIONARIOS // PAG 66
            COLECCION.Rule = l_parent + LISTA_E + r_parent | l_corchete + LISTA_E + r_corchete 
                | l_llave + LISTA_E + r_llave | l_llave + KEY_VALUE_LIST + r_llave;

            KEY_VALUE_LIST.Rule = MakePlusRule(KEY_VALUE_LIST, coma, KEY_VALUE);

            KEY_VALUE.Rule = E + dospuntos + E;

            E_PARENT.Rule = l_parent + E + r_parent;

            UNARIO.Rule = mas + E | menos + E | res_not + E;

            PRIMITIVO.Rule = id | numero | cadena | cadena2 | res_true | res_false | res_null | arroba + id;

            //========================================================================

            LISTA_E.Rule = MakeStarRule(LISTA_E, coma, E);

            LLAMADA_FUNCION.Rule = id + l_parent + LISTA_E + r_parent
                | res_call + id + l_parent + LISTA_E + r_parent;
            //LLAMADA_FUNCION.NodeCaptionTemplate = "Llamada #{0}(...)";

            this.Root = BLOCK;
            #endregion

            NonGrammarTerminals.Add(ToTerm(@"\"));

            RegisterOperators(1, Associativity.Left, or);
            RegisterOperators(2, Associativity.Left, and);
            RegisterOperators(3, Associativity.Left, xor);
            RegisterOperators(4, Associativity.Left, igual_igual, not_igual);
            RegisterOperators(6, Associativity.Left, mayor_que, menor_que);
            RegisterOperators(7, Associativity.Left, mas, menos);
            RegisterOperators(8, Associativity.Left, por, div);
            RegisterOperators(9, Associativity.Right, pot, modular);
            RegisterOperators(10, Associativity.Right, interrogacion);
            RegisterOperators(11, Associativity.Right, not);

            //SIGNOS DE PUNTUACION
            /*MarkPunctuation("(", ")", ":");
            RegisterBracePair("(", ")");*/

            //RECUPERACION
            BLOCK.ErrorRule = SyntaxError + puntocoma;
            BLOCK.ErrorRule = SyntaxError + r_llave;

            //REPORTE DE ERRORES
            AddToNoReportGroup("(");
            AddToNoReportGroup(Eos);

        }//constructor
    }
}