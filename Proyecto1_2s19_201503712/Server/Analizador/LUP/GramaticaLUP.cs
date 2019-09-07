using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.LUP
{
    public class GramaticaLUP:Grammar
    {
        public GramaticaLUP() : base(false)
        {
            #region Expresiones Regulares
            var id = TerminalFactory.CreateCSharpIdentifier("id");
            RegexBasedTerminal res_data = new RegexBasedTerminal("res_data","\\[\\+DATA\\](.|\\s)*\\[-DATA\\]");
            RegexBasedTerminal res_pass = new RegexBasedTerminal("res_pass", "\\[\\+PASS\\](.|\\s)*\\[-PASS\\]");
            #endregion

            #region Terminales
            KeyTerm
            res_queryOpen = ToTerm("[+QUERY]"),
            res_queryClose = ToTerm("[-QUERY]"),
            res_loginOpen = ToTerm("[+LOGIN]"),
            res_loginClose = ToTerm("[-LOGIN]"),
            res_logoutOpen = ToTerm("[+LOGOUT]"),
            res_logoutClose = ToTerm("[-LOGOUT]"),
            res_structOpen = ToTerm("[+STRUCT]"),
            res_structClose = ToTerm("[-STRUCT]"),
            res_userOpen = ToTerm("[+USER]"),
            res_userClose = ToTerm("[-USER]");

            //MarkReservedWords("[+QUERY]", "[-QUERY]", "[+LOGIN]", "[-LOGIN]", "[+LOGOUT]", "[-LOGOUT]", "[+STRUCT]", "[-STRUCT]");

            #endregion

            #region No Terminales
            var S = new NonTerminal("S");
            var LOGIN = new NonTerminal("LOGIN");
            var LOGOUT = new NonTerminal("LOGOUT");
            var QUERY = new NonTerminal("QUERY");
            var STRUCT = new NonTerminal("STRUCT");
            #endregion

            #region Gramatica
            S.Rule = LOGIN
                | LOGOUT
                | STRUCT
                | QUERY;

            LOGIN.Rule = res_loginOpen + res_userOpen + id + res_userClose + res_pass + res_loginClose;

            LOGOUT.Rule = res_logoutOpen + res_userOpen + id + res_userClose + res_logoutClose;

            QUERY.Rule = res_queryOpen + res_userOpen + id + res_userClose + res_data + res_queryClose;

            STRUCT.Rule = res_structOpen + res_userOpen + id + res_userClose + res_structClose;

            this.Root = S;
            #endregion

        }//constructor
    }
}