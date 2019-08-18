using Server.AST.ExpresionesCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class SetCQL : Expresion
    {
        Object tipoDato;
        List<Object> valores;
        public List<Expresion> expresiones { get; set; }

        public SetCQL(Object tipoDato, int fila, int columna)
        {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.columna = columna;
            this.expresiones = new List<Expresion>();
            this.valores = new List<object>();
        }

        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.SET;
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
                return tipoDato;
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
                arbol.addError("List", "(" + idLlamada + ") no posee el metódo buscado", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }
        }

        Object insert(AST_CQL arbol)
        {

            if (this.expresiones.Count != 1)
            {
                arbol.addError("List", "(insert) debe tener exclusivamente 1 parámetro", fila, columna);
                return null;
            }
            Object valor = expresiones[0].getValor(arbol);
            if (this.valores.Contains(valor)) {
                arbol.addError("List", "(insert) ya contiene este valor: "+valor, fila, columna);
                return null;
            }

            this.valores.Add(valor);
            this.valores.Sort();
            return null;
        }

        Object size(AST_CQL arbol)
        {

            if (this.expresiones.Count != 0)
            {
                arbol.addError("List", "(size) debe tener exclusivamente 0 parámetros", fila, columna);
                return 0;
            }

            return this.valores.Count;
        }

        Object contains(AST_CQL arbol)
        {
            if (this.expresiones.Count != 1)
            {
                arbol.addError("List", "(contains) debe tener exclusivamente 1 parámetro", fila, columna);
                return false;
            }

            return this.valores.Contains(this.expresiones[0].getValor(arbol));
        }

        Object clear(AST_CQL arbol)
        {
            if (this.expresiones.Count != 0)
            {
                arbol.addError("List", "(clear) debe tener exclusivamente 0 parámetros", fila, columna);
                return 0;
            }

            this.valores.Clear();
            return null;
        }

        Object remove(AST_CQL arbol)
        {
            int index = 0;
            if (this.expresiones.Count != 1)
            {
                arbol.addError("List", "(remove) debe tener exclusivamente 1 parámetro", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }
            else
            {
                Object indexO = this.expresiones[0].getValor(arbol);
                if (indexO is Int32)
                {
                    index = Convert.ToInt32(indexO);
                }
                else
                {
                    arbol.addError("List", "(remove) el parámetro debe ser de valor entero", fila, columna);
                    return Primitivo.TIPO_DATO.NULL;
                }
            }

            this.valores.RemoveAt(index);
            return null;
        }

        Object get(AST_CQL arbol)
        {

            int index = 0;
            if (this.expresiones.Count != 1)
            {
                arbol.addError("List", "(get) debe tener exclusivamente 1 parámetro", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }
            else
            {
                Object indexO = this.expresiones[0].getValor(arbol);
                if (indexO is Int32)
                {
                    index = Convert.ToInt32(indexO);
                }
                else
                {
                    arbol.addError("List", "(get) el parámetro debe ser de valor entero", fila, columna);
                    return Primitivo.TIPO_DATO.NULL;
                }
            }

            //MANDAR EX si se pasa del límite
            return this.valores[index];
        }

        Object set(AST_CQL arbol)
        {
            int index = 0;
            if (this.expresiones.Count != 2)
            {
                arbol.addError("List", "(set) debe tener exclusivamente 2 parámetros", fila, columna);
            }
            else
            {
                Object indexO = this.expresiones[0].getValor(arbol);
                if (indexO is Int32)
                {
                    index = Convert.ToInt32(indexO);
                }
                else
                {
                    arbol.addError("List", "(set) el parámetro debe ser de valor entero", fila, columna);
                }
            }

            this.valores[index] = this.expresiones[1].getValor(arbol);

            return null;
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}