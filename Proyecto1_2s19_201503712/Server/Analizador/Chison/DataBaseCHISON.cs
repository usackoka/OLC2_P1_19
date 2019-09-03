using Server.AST;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class DataBaseCHISON : Sentencia
    {
        String name;
        List<Data_Base_CHISON> data;

        public DataBaseCHISON(String name, List<Data_Base_CHISON> data, int fila, int columna) {
            this.name = name;
            this.data = data;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }
    }
}