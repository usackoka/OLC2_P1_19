﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ExpresionesCQL.Tipos
{
    public class Date
    {
        public DateTime dateTime;

        public Date(String date) {
            this.dateTime = DateTime.Parse(date);
        }

        public Date(Expresion expresion, AST_CQL arbol, int fila, int columna) {
            try
            {
                this.dateTime = DateTime.Parse(expresion.getValor(arbol).ToString());
            }
            catch (Exception)
            {
                arbol.addError("Date","No se pudo castear de: "+expresion.getTipo(arbol)+" a Date", fila, columna);
                this.dateTime = DateTime.Now;
            }
        }

        public Date()
        {
            this.dateTime = DateTime.Now;
        }

        public override string ToString()
        {
            return this.dateTime.Year + "-" + this.dateTime.Month + "-" + this.dateTime.Day;
        }
    }
}