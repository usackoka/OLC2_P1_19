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
        }

        public Expresion acceso;
        public AsignacionColumna(String idColumna, Expresion acceso, Expresion expresion) {
            this.idColumna = idColumna;
            this.acceso = acceso;
            this.expresion = expresion;
        }
    }
}