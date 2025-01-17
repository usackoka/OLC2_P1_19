﻿using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Where:Expresion
    {
        public Expresion condicion;

        public Where(Expresion expresion) {
            this.condicion = expresion;
        }

        public override object getTipo(AST_CQL arbol)
        {
            return this.condicion.getTipo(arbol);
        }

        public override object getValor(AST_CQL arbol)
        {
            if (condicion.getTipo(arbol).Equals(Primitivo.TIPO_DATO.BOOLEAN))
            {
                return condicion.getValor(arbol);
            }
            else {
                arbol.addError("Where","Se esperaba una expresión booleana, vino: "+condicion.getTipo(arbol),fila,columna);
                return false;
            }
        }
    }
}