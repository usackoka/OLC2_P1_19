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
            this.valores.Add(id, obj);
        }

        public void addRange(Columna p) {
            foreach (DictionaryEntry kvp in p.valores)
            {
                this.valores.Add(kvp.Key, kvp.Value);
            }
        }
    }
}