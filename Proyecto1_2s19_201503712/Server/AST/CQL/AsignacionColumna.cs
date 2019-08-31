using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class AsignacionColumna
    {
        public String idColumna;
        public Expresion expresion;
        public AsignacionColumna(String idColumna, Expresion expresion) {
            this.idColumna = idColumna;
            this.expresion = expresion;
            this.acceso = null;
            this.referencia = null;
        }

        public AccesoArreglo acceso;
        public AsignacionColumna(AccesoArreglo acceso, Expresion expresion) {
            this.acceso = acceso;
            this.referencia = null;
            this.idColumna = acceso.id;
            this.expresion = expresion;
        }

        public Referencia referencia;
        public AsignacionColumna(Referencia referencia, Expresion expresion) {
            this.acceso = null;
            this.referencia = referencia;
            this.idColumna = referencia.referencias[0].ToString().Replace("$","");
            //this.referencia.referencias[0] = "$"+this.referencia.referencias[0];
            this.expresion = expresion;
        }
    }
}