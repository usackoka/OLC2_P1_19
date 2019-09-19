using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class Parametro
    {

        Hashtable valores;

        public Parametro(String id, String value) {
            this.valores = new Hashtable();
            this.valores.Add(id.ToLower(),value);
        }

        public Parametro()
        {
            this.valores = new Hashtable();
        }

        public void addRange(Parametro p) {
            foreach (DictionaryEntry kvp in p.valores)
            {
                this.valores.Add(kvp.Key, kvp.Value);
            }
        }

        public String getName(Management dbms)
        {
            if (this.valores.ContainsKey("name"))
            {
                return this.valores["name"].ToString();
            }
            else
            {
                dbms.addError("LoadData-Chison", "El parametro no contiene el atributo NAME", 0, 0);
                return "NULL";
            }
        }

        public Object getCqlType(Management dbms)
        {
            if (this.valores.ContainsKey("type"))
            {
                return Primitivo.getTipoString(this.valores["type"].ToString(),dbms);
            }
            else
            {
                dbms.addError("LoadData-Chison", "El parametro no contiene el atributo TYPE", 0, 0);
                return new Null();
            }
        }

        public Boolean getAS(Management dbms)
        {
            if (this.valores.ContainsKey("as"))
            {
                return Convert.ToBoolean(this.valores["as"].Equals("in"));
            }
            else
            {
                dbms.addError("LoadData-Chison", "El parametro no contiene el atributo AS", 0, 0);
                return false;
            }
        }
    }
}