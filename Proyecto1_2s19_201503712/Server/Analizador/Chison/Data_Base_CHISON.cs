using Server.AST;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    /*
     Esta clase será la encargada de crear todos los elementos de la base de datos
    */
    public class Data_Base_CHISON:Sentencia
    {
        String id;
        Object obj;
        
        public Data_Base_CHISON(String id, Object obj, int fila, int columna) {
            this.id = id;
            this.obj = obj;
            this.fila = fila;
            this.columna = columna;
        }

        public Data_Base_CHISON() {

        }

        public void addRange(Data_Base_CHISON db) {
        }

        void Inicializar() {
            if (this.id.Equals("name", StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else if (this.id.Equals("cql-type", StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else if (this.id.Equals("columns", StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else if (this.id.Equals("data", StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else if (this.id.Equals("attrs", StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else if (this.id.Equals("parameters", StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else {
                //this.id.Equals("instr", StringComparison.InvariantCultureIgnoreCase)
            }
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }
    }
}