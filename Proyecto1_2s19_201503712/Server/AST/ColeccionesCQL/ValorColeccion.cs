using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class ValorColeccion:Expresion
    {
        List<Expresion> expresiones;
        List<KeyValuePair<Expresion, Expresion>> kvpList;
        String tipo;

        public ValorColeccion(String tipo, List<Expresion> expresiones ,int fila, int columna) {
            this.tipo = tipo;
            this.expresiones = expresiones;
            this.fila = fila;
            this.columna = columna;
        }

        public ValorColeccion(List<KeyValuePair<Expresion, Expresion>> kvpList, int fila, int columna) {
            this.kvpList = kvpList;
            this.fila = fila;
            this.columna = columna;
            this.tipo = null;
        }

        public override object getValor(AST_CQL arbol)
        {
            switch (tipo) {
                case "[":
                    List<Object> listReturn = new List<object>();
                    foreach (Expresion exp in expresiones)
                    {
                        listReturn.Add(exp.getValor(arbol));
                    }
                    ListCQL list = new ListCQL(expresiones[0].getTipo(arbol), fila, columna);
                    list.valores = listReturn;
                    return list;
                case "{":
                    List<Object> listReturn2 = new List<object>();
                    foreach (Expresion exp in expresiones)
                    {
                        listReturn2.Add(exp.getValor(arbol));
                    }
                    SetCQL list2 = new SetCQL(expresiones[0].getTipo(arbol), fila, columna);
                    list2.valores = listReturn2;
                    return list2;
                case null:
                    Hashtable listReturn3 = new Hashtable();
                    foreach (KeyValuePair<Expresion, Expresion> kvp in kvpList)
                    {
                        listReturn3.Add(kvp.Key.getValor(arbol), kvp.Value.getValor(arbol));
                    }
                    MapCQL list3 = new MapCQL(kvpList[0].Key.getTipo(arbol), kvpList[0].Value.getTipo(arbol), fila, columna);
                    list3.valores = listReturn3;
                    return list3;
                default:
                    UserType modeloUt = arbol.dbms.getUserType(tipo.ToString());
                    if (modeloUt == null)
                    {
                        arbol.addError("TypeDontExists", "No se encontró el UserType: " + tipo.ToString(), fila, columna);
                        return Catch.EXCEPTION.TypeDontExists;
                    }
                    return new UserType(modeloUt, expresiones, arbol);
            }
        }

        public override object getTipo(AST_CQL arbol)
        {
            switch (this.tipo) {
                case "[":
                    return new TipoList(expresiones[0].getTipo(arbol));
                case "{":
                    return new TipoSet(expresiones[0].getTipo(arbol));
                case null:
                    return new TipoMAP(kvpList[0].Key.getTipo(arbol), kvpList[0].Value.getTipo(arbol));
                default:
                    return this.tipo;
            }
        }
    }
}