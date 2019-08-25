﻿using Server.AST.ExpresionesCQL;
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

        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.MAP;
        }

        public override object getValor(AST_CQL arbol)
        {
            return this;
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
                return Primitivo.TIPO_DATO.NULL;
            }
        }

        Object insert(AST_CQL arbol)
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

        Object remove(AST_CQL arbol)
        {
            if (this.expresiones.Count != 1)
            {
                arbol.addError("Map", "(remove) debe tener exclusivamente 1 parámetro", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }

            this.valores.Remove(this.expresiones[0].getValor(arbol));
            return null;
        }

        Object get(AST_CQL arbol)
        {
            if (this.expresiones.Count != 1)
            {
                arbol.addError("Map", "(get) debe tener exclusivamente 1 parámetro", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }

            //MANDAR EX si se pasa del límite
            return this.valores[this.expresiones[0].getValor(arbol)];
        }

        Object set(AST_CQL arbol)
        {
            if (this.expresiones.Count != 2)
            {
                arbol.addError("Map", "(set) debe tener exclusivamente 2 parámetros", fila, columna);
            }

            this.valores[this.expresiones[0].getValor(arbol)] = this.expresiones[1].getValor(arbol);

            return null;
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}