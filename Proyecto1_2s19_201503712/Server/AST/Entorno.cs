﻿using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST
{
    public class Entorno
    {
        public Entorno padre { get; set; }
        TablaSimbolos tablaSimbolos;

        public Entorno(Entorno padre) {
            this.padre = padre;
            this.tablaSimbolos = new TablaSimbolos();
        }

        public void addVariable(String key, Variable value, AST_CQL arbol, int fila, int columna) {
            if (this.tablaSimbolos.ContainsKey(key)) {
                arbol.addError("EXCEPTION","Ya existe la variable con id:"+key+" declarada en este ámbito",fila,columna);
                return;
            }
            this.tablaSimbolos.Add(key, value);
        }

        public Object getTipoVariable(String id, AST_CQL arbol, int fila, int columna)
        {
            if (this.tablaSimbolos.ContainsKey(id))
            {
                return ((Variable)this.tablaSimbolos[id]).getTipo(arbol);
            }
            else
            {
                if (this.padre == null)
                {
                    arbol.addError(id, "No se encontró la variable: " + id + " en ningún ambito (getTipoVariable)", fila, columna);
                    return new Null();
                }
                else
                {
                    return this.padre.getTipoVariable(id, arbol, fila, columna);
                }
            }
        }

        public Object getValorVariable(String id, AST_CQL arbol, int fila, int columna) {
            if (this.tablaSimbolos.ContainsKey(id))
            {
                return ((Variable)this.tablaSimbolos[id]).getValor(arbol);
            }
            else {
                if (this.padre == null)
                {
                    arbol.addError(id,"No se encontró la variable: "+id+" en ningún ambito (getValorVariable)",fila,columna);
                    return new Null();
                }
                else {
                    return this.padre.getValorVariable(id, arbol, fila, columna);
                }
            }
        }

        public Object getVariable(String id, AST_CQL arbol, int fila, int columna)
        {
            if (this.tablaSimbolos.ContainsKey(id))
            {
                return (this.tablaSimbolos[id]);
            }
            else
            {
                if (this.padre == null)
                {
                    arbol.addError(id, "No se encontró la variable: " + id + " en ningún ambito (getVariable)", fila, columna);
                    return new Null();
                }
                else
                {
                    return this.padre.getVariable(id, arbol, fila, columna);
                }
            }
        }

        public void reasignarVariable(String id, Object valor, Object tipo, AST_CQL arbol, int fila, int columna) {
            if (this.tablaSimbolos.ContainsKey(id))
            {
                Variable var = (Variable)this.tablaSimbolos[id];
                if (var.getTipo(arbol) is TipoMAP && tipo is TipoMAP) {
                    var.setValor(valor);
                }
                else if (var.getTipo(arbol) is TipoList && tipo is TipoList)
                {
                    var.setValor(valor);
                }
                else if (var.getTipo(arbol) is TipoSet && tipo is TipoSet)
                {
                    var.setValor(valor);
                }
                else if (var.getTipo(arbol).Equals(tipo))
                {
                    var.setValor(valor);
                }
                else {
                    arbol.addError(id, "No se puede castear implicitamente de " + tipo + " a " + var.getTipo(arbol), fila, columna);
                }
            }
            else
            {
                if (this.padre == null)
                {
                    arbol.addError(id, "No se encontró la variable: " + id + " en ningún ambito (reasignar variable)", fila, columna);
                }
                else
                {
                    this.padre.reasignarVariable(id,valor,tipo,arbol,fila,columna);
                }
            }
        }
    }
}