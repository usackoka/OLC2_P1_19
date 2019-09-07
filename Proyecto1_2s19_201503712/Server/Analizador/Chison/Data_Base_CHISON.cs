using Server.AST;
using Server.AST.CQL;
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
    public class Data_Base_CHISON
    {
        Hashtable valores;
        int fila , columna;
        
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

        public String getCqlType(AST_CQL arbol) {
            if (this.valores.ContainsKey("cql-type"))
            {
                return this.valores["cql-type"].ToString();
            }
            else
            {
                arbol.addError("LoadData-Chison", "La Data no contiene el atributo CQL-TYPE", fila, columna);
                return "NULL";
            }
        }

        public String getName(AST_CQL arbol)
        {
            if (this.valores.ContainsKey("name"))
            {
                return this.valores["name"].ToString();
            }
            else
            {
                arbol.addError("LoadData-Chison", "La Data no contiene el atributo NAME", fila, columna);
                return "NULL";
            }
        }

        public List<KeyValuePair<String, Object>> getData(AST_CQL arbol) {
            List<KeyValuePair<String, Object>> lista = new List<KeyValuePair<String, Object>>();

            if (this.valores.ContainsKey("data"))
            {
                Object o = this.valores["data"];

            }
            else
            {
                arbol.addError("LoadData-Chison", "La Data no contiene el atributo DATA", fila, columna);
            }

            return lista;
        }

        public List<ColumnCQL> getColumnsDefinitions(AST_CQL arbol) {
            if (this.valores.ContainsKey("columns"))
            {
                List<ColumnCQL> retornos = new List<ColumnCQL>();
                List<Columna> columns = (List<Columna>)this.valores["columns"];
                foreach (Columna c in columns) {
                    retornos.Add(new ColumnCQL(c.getName(arbol,fila,columna),
                        c.getType(arbol,fila,columna),
                        c.getPK(arbol,fila,columna),
                        fila,columna));
                }
                return retornos;
            }
            else
            {
                arbol.addError("LoadData-Chison", "La Data no contiene el atributo COLUMNS", fila, columna);
                return new List<ColumnCQL>();
            }
        }
    }
}