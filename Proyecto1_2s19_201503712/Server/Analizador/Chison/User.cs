using Server.AST;
using Server.AST.DBMS;
using Server.AST.SentenciasCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class User
    {
        Hashtable valores;
        int fila, columna;
        public User() { this.valores = new Hashtable(); }
        public User(String key, Object value, int fila, int columna) {
            this.valores = new Hashtable();
            this.valores.Add(key,value);
            this.fila = fila;
            this.columna = columna;
        }

        public void addRange(User user) {
            this.fila = user.fila;
            this.columna = user.columna;
            foreach (DictionaryEntry kvp in user.valores)
            {
                    this.valores.Add(kvp.Key, kvp.Value);
            }
        }

        public String getName(Management dbms) {
            if (this.valores.ContainsKey("name"))
            {
                return this.valores["name"].ToString().Replace("\"","");
            }
            else {
                dbms.addError("LoadUser-Chison","El user no contiene el atributo NAME",fila,columna);
                return "NULL";
            }
        }

        public String getPassword(Management dbms)
        {
            if (this.valores.ContainsKey("password"))
            {
                return this.valores["password"].ToString().Replace("\"","");
            }
            else
            {
                dbms.addError("LoadUser-Chison", "El user no contiene el atributo PASSWORD", fila, columna);
                return "NULL";
            }
        }

        public List<String> getGrants(Management dbms) {
            if (this.valores.ContainsKey("permissions"))
            {
                return (List<String>)this.valores["permissions"];
            }
            else
            {
                dbms.addError("LoadUser-Chison", "El user no contiene el atributo PERMISSIONS", fila, columna);
                return new List<string>();
            }
        }
    }
}