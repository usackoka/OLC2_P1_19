using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class GramaticaChison : Grammar
    {
        public GramaticaChison() : base(false)
        {
            #region Expresiones Regulares
            var numero = new NumberLiteral("numero");
            var id = TerminalFactory.CreatePythonIdentifier("id");
            //StringLiteral caracter = new StringLiteral("caracter", "\'", StringOptions.IsChar);
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.AllowsAllEscapes);
            StringLiteral cadena2 = new StringLiteral("cadena2", "\'", StringOptions.AllowsAllEscapes);

            //TERMINALES DE COMENTARIO
            //CommentTerminal COMENTARIO_LINEA = new CommentTerminal("COMENTARIO_LINEA", "//", "\n", "\r");
            //CommentTerminal COMENTARIO_LINEAS = new CommentTerminal("COMENTARIO_LINEAS", "/*", "*/");
            //PARA QUE NO TOME LOS COMENTARIOS COMO TERMINALES
            //base.NonGrammarTerminals.Add(COMENTARIO_LINEA);
            //base.NonGrammarTerminals.Add(COMENTARIO_LINEAS);
            #endregion

            #region Terminales
            var coma = ToTerm(",");
            var punto = ToTerm(".");
            var l_corchete = ToTerm("[");
            var r_corchete = ToTerm("]");
            var l_llave = ToTerm("{");
            var r_llave = ToTerm("}");
            var igual = ToTerm("=");

            Terminal
            menor_que = ToTerm("<"),
            mayor_que = ToTerm(">"),
            dolar = ToTerm("$");

            KeyTerm
            res_true = ToTerm("TRUE"),
            res_false = ToTerm("FALSE"),
            res_null = ToTerm("NULL"),
            res_in = ToTerm("IN"),
            res_out = ToTerm("OUT"),
            res_databases = ToTerm("\"DATABASES\""),
            res_name = ToTerm("\"NAME\""),
            res_cql_type = ToTerm("\"CQL-TYPE\""),
            res_columns = ToTerm("\"COLUMNS\""),
            res_type = ToTerm("\"TYPE\""),
            res_pk = ToTerm("\"PK\""),
            res_data = ToTerm("\"DATA\""),
            res_attrs = ToTerm("\"ATTRS\""),
            res_as = ToTerm("\"AS\""),
            res_instr = ToTerm("\"INSTR\""),
            res_parameters = ToTerm("\"PARAMETERS\""),
            res_users = ToTerm("\"USERS\""),
            res_password = ToTerm("\"PASSWORD\""),
            res_chison = ToTerm("chison"),
            res_permissions = ToTerm("\"PERMISSIONS\"");

            MarkReservedWords("NULL","\"DATABASES\"", "\"NAME\"", "\"CQL-TYPE\"", "\"COLUMNS\"", "\"TYPE\"", "\"PK\"", "\"DATA\"",
                "\"ATTRS\"", "\"AS\"", "\"INSTR\"", "\"PARAMETERS\"", "\"USERS\"", "\"PASSWORD\"", "\"PERMISSIONS\"","chison","TRUE","FALSE");
            #endregion

            #region No Terminales
            var S = new NonTerminal("S");
            var E = new NonTerminal("E");
            var DATABASES = new NonTerminal("DATABASES");
            var USERS = new NonTerminal("USERS");
            var LISTA_BASES = new NonTerminal("LISTA_BASES");
            var BASE = new NonTerminal("BASE");
            var LISTA_CUERPO_DB = new NonTerminal("LISTA_CUERPO_DB");
            var CUERPO_DB = new NonTerminal("CUERPO_DB");
            var LISTA_COLUMNAS = new NonTerminal("LISTA_COLUMNAS");
            var COLUMNA = new NonTerminal("COLUMNA");
            var CARACT_COLUMNA = new NonTerminal("CARACT_COLUMNA");
            var PRIMITIVO = new NonTerminal("PRIMITIVO");
            var DATA_COLUMNAS = new NonTerminal("DATA_COLUMNAS");
            var LISTA_DATA_COLUMNAS = new NonTerminal("LISTA_DATA_COLUMNAS");
            var VALOR = new NonTerminal("VALOR");
            var LISTA = new NonTerminal("LISTA");
            var MAP = new NonTerminal("MAP");
            var LISTA_VALORES = new NonTerminal("LISTA_VALORES");
            var LISTA_KEY_VALUE_PAIR = new NonTerminal("LISTA_KEY_VALUE_PAIR");
            var KEY_VALUE_PAIR = new NonTerminal("KEY_VALUE_PAIR");
            var LISTA_USER = new NonTerminal("LISTA_USER");
            var USER = new NonTerminal("USER");
            var LISTA_DATA_USER = new NonTerminal("LISTA_DATA_USER");
            var DATA_USER = new NonTerminal("DATA_USER");
            var LISTA_PERMISOS = new NonTerminal("LISTA_PERMISOS");
            var PERMISO = new NonTerminal("PERMISO");
            var LISTA_ATTRS = new NonTerminal("LISTA_ATTRS");
            var ATTRS = new NonTerminal("ATTRS");
            var LISTA_PARAMETERS = new NonTerminal("LISTA_PARAMETERS");
            var LISTA_CARACT_PARAMETER = new NonTerminal("LISTA_CARACT_PARAMETER");
            var PARAMETER = new NonTerminal("PARAMETER");
            var CARACT_PARAMETER = new NonTerminal("CARACT_PARAMETER");
            var LISTA_DATA_BASE = new NonTerminal("LISTA_DATA_BASE");
            var DATA_BASE = new NonTerminal("DATA_BASE");
            var LISTA_CARACT_COLUMNA = new NonTerminal("LISTA_CARACT_COLUMNA");
            var LISTA_DATA_COLUMNAS1 = new NonTerminal("LISTA_DATA_COLUMNAS1");
            var DATA_COLUMNAS1 = new NonTerminal("DATA_COLUMNAS1");
            #endregion

            #region Gramatica
            S.Rule = dolar + menor_que + DATABASES + coma + USERS + mayor_que + dolar;

            //========================= USERS ==============================================
            USERS.Rule = res_users + igual + l_corchete + LISTA_USER + r_corchete;

            LISTA_USER.Rule = MakeStarRule(LISTA_USER, coma, USER);

            USER.Rule = menor_que + LISTA_DATA_USER + mayor_que;

            LISTA_DATA_USER.Rule = MakePlusRule(LISTA_DATA_USER, coma,DATA_USER);

            DATA_USER.Rule = res_name + igual + cadena
                | res_password + igual + cadena
                | res_permissions + igual + l_corchete + LISTA_PERMISOS + r_corchete;

            LISTA_PERMISOS.Rule = MakeStarRule(LISTA_PERMISOS,coma, PERMISO);

            PERMISO.Rule = menor_que + res_name + igual + cadena + mayor_que;

            //========================= DATABASES ==========================================

            DATABASES.Rule = res_databases + igual + l_corchete + LISTA_BASES + r_corchete;

            LISTA_BASES.Rule = MakeStarRule(LISTA_BASES,coma,BASE);

            BASE.Rule = menor_que + res_name + igual + cadena + coma + 
                res_data + igual + l_corchete + LISTA_DATA_BASE + r_corchete + mayor_que;

            LISTA_DATA_BASE.Rule = MakeStarRule(LISTA_DATA_BASE,coma,DATA_BASE);

            DATA_BASE.Rule = menor_que + LISTA_CUERPO_DB + mayor_que;

            LISTA_CUERPO_DB.Rule = MakePlusRule(LISTA_CUERPO_DB,coma,CUERPO_DB);

            CUERPO_DB.Rule = res_name + igual + cadena
                | res_cql_type + igual + cadena
                | res_columns + igual + l_corchete + LISTA_COLUMNAS + r_corchete
                | res_data + igual + l_corchete + LISTA_DATA_COLUMNAS1 + r_corchete
                | res_attrs + igual + l_corchete + LISTA_ATTRS + r_corchete
                | res_parameters + igual + l_corchete + LISTA_PARAMETERS + r_corchete
                | res_instr + igual + cadena;

            LISTA_PARAMETERS.Rule = MakeStarRule(LISTA_PARAMETERS,coma,PARAMETER);

            PARAMETER.Rule = menor_que + LISTA_CARACT_PARAMETER + mayor_que;

            LISTA_CARACT_PARAMETER.Rule = MakeStarRule(LISTA_CARACT_PARAMETER,coma,CARACT_PARAMETER);

            CARACT_PARAMETER.Rule = res_name + igual + cadena
                | res_type + igual + cadena
                | res_as + igual + res_in
                | res_as + igual + res_out;

            LISTA_ATTRS.Rule = MakeStarRule(LISTA_ATTRS,coma , ATTRS);

            ATTRS.Rule = menor_que + LISTA_KEY_VALUE_PAIR + mayor_que;

            LISTA_COLUMNAS.Rule = MakeStarRule(LISTA_COLUMNAS,coma,COLUMNA);

            COLUMNA.Rule = menor_que + LISTA_CARACT_COLUMNA + mayor_que;

            LISTA_CARACT_COLUMNA.Rule = MakePlusRule(LISTA_CARACT_COLUMNA,coma, CARACT_COLUMNA);

            CARACT_COLUMNA.Rule = res_name + igual + cadena
                | res_type + igual + cadena
                | res_pk + igual + PRIMITIVO;

            LISTA_DATA_COLUMNAS1.Rule = MakeStarRule(LISTA_DATA_COLUMNAS1,coma,DATA_COLUMNAS1);

            DATA_COLUMNAS1.Rule = menor_que + LISTA_DATA_COLUMNAS + mayor_que;

            LISTA_DATA_COLUMNAS.Rule = MakeStarRule(LISTA_DATA_COLUMNAS,coma,DATA_COLUMNAS);

            DATA_COLUMNAS.Rule = cadena + igual + VALOR;

            LISTA_VALORES.Rule = MakeStarRule(LISTA_VALORES, coma, VALOR);

            VALOR.Rule = PRIMITIVO
                | LISTA
                | MAP;

            LISTA.Rule = l_corchete + LISTA_VALORES + r_corchete;

            MAP.Rule = menor_que + LISTA_KEY_VALUE_PAIR + mayor_que;

            LISTA_KEY_VALUE_PAIR.Rule = MakeStarRule(LISTA_KEY_VALUE_PAIR,coma,KEY_VALUE_PAIR);

            KEY_VALUE_PAIR.Rule = VALOR + igual + VALOR;

            PRIMITIVO.Rule = numero
                | cadena
                | cadena2
                | res_true
                | res_false
                | res_null;

            this.Root = S;
            //RECUPERACION
            //BLOCK.ErrorRule = SyntaxError + puntocoma;
            //BLOCK.ErrorRule = SyntaxError + r_llave;
            #endregion

            //REPORTE DE ERRORES
            //AddToNoReportGroup("(");
            //AddToNoReportGroup(Eos);

        }//constructor
    }
}