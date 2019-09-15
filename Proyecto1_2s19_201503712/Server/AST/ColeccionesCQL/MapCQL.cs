using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class MapCQL : Expresion
    {
        Object tipoClave;
        Object tipoValor;
        public List<Expresion> expresiones { get; set; }
        public Hashtable valores { get; set; }

        public MapCQL(Object tipoClave, Object tipoValor, int fila, int columna) {

            this.valores = new Hashtable();
            this.expresiones = new List<Expresion>();
            this.fila = fila;
            this.columna = columna;
            this.tipoValor = tipoValor;
            this.tipoClave = tipoClave;
        }

        public override string ToString()
        {
            String trad = "<";
            foreach (DictionaryEntry pair in this.valores)
            {
                //clave
                if (pair.Key is String)
                {
                    trad += "\"" + pair.Key + "\"=";
                }
                else if (pair.Key is Date)
                {
                    trad += "'" + pair.Key + "'=";
                }
                else if (pair.Key is TimeSpan)
                {
                    trad += "'" + pair.Key + "'=";
                }
                else if (pair.Key is UserType)
                {
                    trad += ((UserType)pair.Key).getData();
                }
                else
                {
                    trad += pair.Key + "=";
                }

                //valor
                if (pair.Value is String)
                {
                    trad += "\"" + pair.Value + "\",";
                }
                else if (pair.Value is Date)
                {
                    trad += "'" + pair.Value + "',";
                }
                else if (pair.Value is TimeSpan) {
                    trad += "'" + pair.Value + "',";
                }
                else if (pair.Value is UserType)
                {
                    trad += ((UserType)pair.Value).getData();
                }
                else
                {
                    trad += pair.Value + ",";
                }
            }
            trad = trad.TrimEnd(',');
            trad += ">";
            return trad;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return new TipoMAP(this.tipoClave,this.tipoValor);
        }

        public override object getValor(AST_CQL arbol)
        {
            return this;
        }

        public void addRange(MapCQL map) {
            foreach (DictionaryEntry pair in map.valores) {
                this.valores.Add(pair.Key,pair.Value);
            }
        }

        public void removeRange(SetCQL map) {
            foreach (Object obj in map.valores) {
                if (this.valores.ContainsKey(obj)) {
                    this.valores.Remove(obj);
                }
            }
        }

        public object getTipoMetodo(String id)
        {
            if (id.ToLower().Equals("insert"))
            {
                return null;
            }
            else if (id.ToLower().Equals("get"))
            {
                return tipoValor;
            }
            else if (id.ToLower().Equals("set"))
            {
                return null;
            }
            else if (id.ToLower().Equals("contains"))
            {
                return Primitivo.TIPO_DATO.BOOLEAN;
            }
            else if (id.ToLower().Equals("clear"))
            {
                return null;
            }
            else if (id.ToLower().Equals("remove"))
            {
                return null;
            }
            else if (id.ToLower().Equals("size"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else
            {
                return null;
            }
        }

        public Object getMetodo(AST_CQL arbol, String idLlamada)
        {
            if (idLlamada.ToLower().Equals("insert"))
            {
                return insert(arbol);
            }
            else if (idLlamada.ToLower().Equals("get"))
            {
                return get(arbol);
            }
            else if (idLlamada.ToLower().Equals("size"))
            {
                return size(arbol);
            }
            else if (idLlamada.ToLower().Equals("set"))
            {
                return set(arbol);
            }
            else if (idLlamada.ToLower().Equals("contains"))
            {
                return contains(arbol);
            }
            else if (idLlamada.ToLower().Equals("clear"))
            {
                return clear(arbol);
            }
            else if (idLlamada.ToLower().Equals("remove"))
            {
                return remove(arbol);
            }
            else
            {
                arbol.addError("Map", "(" + idLlamada + ") no posee el metódo buscado", fila, columna);
                return new Null();
            }
        }

        public Object insert(AST_CQL arbol)
        {

            if (this.expresiones.Count != 2)
            {
                arbol.addError("Map", "(insert) debe tener exclusivamente 2 parámetros", fila, columna);
                return null;
            }

            Object valor = expresiones[1].getValor(arbol);
            Object clave = expresiones[0].getValor(arbol);
            if (this.valores.ContainsKey(clave))
            {
                arbol.addError("Map", "(insert) ya contiene esta clave: " + clave, fila, columna);
                return null;
            }

            this.valores.Add(clave,valor);
            return null;
        }

        Object size(AST_CQL arbol)
        {

            if (this.expresiones.Count != 0)
            {
                arbol.addError("Map", "(size) debe tener exclusivamente 0 parámetros", fila, columna);
                return 0;
            }

            return this.valores.Count;
        }

        Object contains(AST_CQL arbol)
        {
            if (this.expresiones.Count != 1)
            {
                arbol.addError("Map", "(contains) debe tener exclusivamente 1 parámetro", fila, columna);
                return false;
            }

            return this.valores.ContainsKey(this.expresiones[0].getValor(arbol));
        }

        Object clear(AST_CQL arbol)
        {
            if (this.expresiones.Count != 0)
            {
                arbol.addError("Map", "(clear) debe tener exclusivamente 0 parámetros", fila, columna);
                return 0;
            }

            this.valores.Clear();
            return null;
        }

        public Object remove(AST_CQL arbol)
        {
            if (this.expresiones.Count != 1)
            {
                arbol.addError("Map", "(remove) debe tener exclusivamente 1 parámetro", fila, columna);
                return new Null();
            }

            Object key = this.expresiones[0].getValor(arbol);
            if (this.valores.ContainsKey(key))
            {
                this.valores.Remove(key);
                return null;
            }
            else
            {
                arbol.addError("EXCEPTION.IndexOutException", "(REMOVE, MAP), no contiene la clave: " + key, fila, columna);
                return Catch.EXCEPTION.IndexOutException;
            }
        }

        Object get(AST_CQL arbol)
        {
            if (this.expresiones.Count != 1)
            {
                arbol.addError("Map", "(get) debe tener exclusivamente 1 parámetro", fila, columna);
                return new Null();
            }

            //MANDAR EX si se pasa del límite

            Object key = this.expresiones[0].getValor(arbol);
            if (this.valores.ContainsKey(key))
            {
                return this.valores[key];
            }
            else
            {
                arbol.addError("EXCEPTION.IndexOutException", "(GET, MAP), no contiene la clave: "+key, fila, columna);
                return Catch.EXCEPTION.IndexOutException;
            }
            
        }

        public Object set(AST_CQL arbol)
        {
            if (this.expresiones.Count != 2)
            {
                arbol.addError("Map", "(set) debe tener exclusivamente 2 parámetros", fila, columna);
            }

            Object key = this.expresiones[0].getValor(arbol);
            if (this.valores.ContainsKey(key))
            {
                this.valores[key] = this.expresiones[1].getValor(arbol);
            }
            else
            {
                arbol.addError("EXCEPTION.IndexOutException", "(SET, MAP), no contiene la clave: " + key, fila, columna);
                return Catch.EXCEPTION.IndexOutException;
            }

            return null;
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }

        
    }
}