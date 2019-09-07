using Server.AST;
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

        public Columna() { }

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

        public String getName(AST_CQL arbol, int fila, int columna)
        {
            if (this.valores.ContainsKey("name"))
            {
                return this.valores["name"].ToString();
            }
            else
            {
                arbol.addError("LoadColumn-Chison", "La columna no contiene el atributo NAME", fila, columna);
                return "NULL";
            }
        }

        public Object getType(AST_CQL arbol, int fila, int columna)
        {
            if (this.valores.ContainsKey("type"))
            {
                String id = this.valores["type"].ToString();
                return Primitivo.getTipoString(id,arbol);
            }
            else
            {
                arbol.addError("LoadColumn-Chison", "La columna no contiene el atributo TYPE", fila, columna);
                return "NULL";
            }
        }

        public Boolean getPK(AST_CQL arbol, int fila, int columna)
        {
            if (this.valores.ContainsKey("pk"))
            {
                return this.valores["pk"].ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase) ? true:false ;
            }
            else
            {
                arbol.addError("LoadColumn-Chison", "La columna no contiene el atributo PK", fila, columna);
                return false;
            }
        }
    }
}