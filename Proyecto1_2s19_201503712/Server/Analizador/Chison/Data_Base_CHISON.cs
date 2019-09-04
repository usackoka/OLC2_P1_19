using Server.AST;
using Server.AST.SentenciasCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    /*
     Esta clase será la encargada de crear todos los elementos de la base de datos
    */
    public class Data_Base_CHISON:Sentencia
    {
        Hashtable valores;
        
        public Data_Base_CHISON(String id, Object obj, int fila, int columna) {
            this.fila = fila;
            this.columna = columna;
            this.valores = new Hashtable();
            this.valores.Add(id.ToLower(),obj);
        }

        public Data_Base_CHISON() {

        }

        public void addRange(Data_Base_CHISON db) {
            foreach (DictionaryEntry kvp in db.valores) {
                this.valores.Add(kvp.Key,kvp.Value);
            }
        }
        

        public override object Ejecutar(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }
    }
}