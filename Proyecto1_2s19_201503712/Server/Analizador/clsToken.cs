using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador
{
    public class clsToken
    {
        public String lexema { get; set; }
        public String descripcion { get; set; }
        public int fila { get; set; }
        public int columna { get; set; }
        public String tipo { get; set; }
        public String ambito { get; set; }

        public clsToken(String Lexema, String Descripcion, int fila, int Columna, String Tipo, String ambito)
        {
            this.lexema = Lexema;
            this.descripcion = Descripcion;
            this.fila = fila;
            this.columna = Columna;
            this.tipo = Tipo;
            this.ambito = ambito;
        }
    }
}