using Server.AST;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class Columna
    {
        Hashtable valores;

        public Columna()
        {
            this.valores = new Hashtable();
        }

        public Columna(String id, Object obj) {
            this.valores = new Hashtable();
            this.valores.Add(id.ToLower(), obj);
        }

        public void addRange(Columna p) {
            foreach (DictionaryEntry kvp in p.valores)
            {
                this.valores.Add(kvp.Key, kvp.Value);
            }
        }

        public String getName(Management dbms, int fila, int columna)
        {
            if (this.valores.ContainsKey("name"))
            {
                return this.valores["name"].ToString();
            }
            else
            {
                dbms.addError("LoadColumn-Chison", "La columna no contiene el atributo NAME", fila, columna);
                return "NULL";
            }
        }

        public Object getType(Management dbms, int fila, int columna)
        {
            if (this.valores.ContainsKey("type"))
            {
                String id = this.valores["type"].ToString();
                return Primitivo.getTipoString(id,dbms);
            }
            else
            {
                dbms.addError("LoadColumn-Chison", "La columna no contiene el atributo TYPE", fila, columna);
                return "NULL";
            }
        }

        public Boolean getPK(Management dbms, int fila, int columna)
        {
            if (this.valores.ContainsKey("pk"))
            {
                String pk = this.valores["pk"].ToString().ToLower();
                return pk.Equals("false")?false:true;
            }
            else
            {
                dbms.addError("LoadColumn-Chison", "La columna no contiene el atributo PK", fila, columna);
                return false;
            }
        }
    }
}