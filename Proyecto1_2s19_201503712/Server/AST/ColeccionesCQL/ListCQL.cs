using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class ListCQL:Expresion
    {
        Object tipoDato;
        public List<Expresion> expresiones { get; set; }
        public List<Object> valores;

        public ListCQL(Object tipoDato, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.fila = fila;
            this.expresiones = new List<Expresion>();
            this.valores = new List<object>();
            this.columna = columna;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return Primitivo.TIPO_DATO.LIST;
        }

        public object getTipoMetodo(String id) {
            if (id.ToLower().Equals("insert"))
            {
                return null;
            }
            else if (id.ToLower().Equals("get"))
            {
                return tipoDato;
            }
            else if (id.ToLower().Equals("size"))
            {
                return Primitivo.TIPO_DATO.INT;
            }
            else {
                return Primitivo.TIPO_DATO.NULL;
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            return this;
        }

        public Object getMetodo(AST_CQL arbol, String idLlamada) {
            if (ContainsString(idLlamada, "insert"))
            {
                return insert(arbol);
            }
            else if (ContainsString(idLlamada, "get"))
            {
                return get(arbol);
            }
            else if (ContainsString(idLlamada,"size")) {
                return size(arbol);
            }
            else {
                arbol.addError("List", "(" + idLlamada + ") no posee el metódo buscado", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }
        }

        Object insert(AST_CQL arbol) {

            if (this.expresiones.Count != 1) {
                arbol.addError("List","(insert) debe tener exclusivamente 1 parámetro",fila,columna);
                return null;
            }

            this.valores.Add(expresiones[0].getValor(arbol));
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

        Object get(AST_CQL arbol) {

            int index = 0;
            if (this.expresiones.Count != 1)
            {
                arbol.addError("List", "(get) debe tener exclusivamente 1 parámetro", fila, columna);
                return Primitivo.TIPO_DATO.NULL;
            }
            else {
                Object indexO = this.expresiones[0].getValor(arbol);
                if (indexO is Int32) {
                    index = Convert.ToInt32(indexO);
                }
                else {
                    arbol.addError("List", "(get) el parámetro debe ser de valor entero", fila, columna);
                    return Primitivo.TIPO_DATO.NULL;
                }
            }

            //MANDAR EX si se pasa del límite
            return this.valores[index];
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}