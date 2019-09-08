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

    }
}