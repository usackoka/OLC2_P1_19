using Server.AST;
using Server.AST.CQL;
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
            //creo la base de datos
            arbol.dbms.createDataBase(this.name, arbol, fila, columna);

            //creo toda la data de la base de datos
            foreach (Data_Base_CHISON contenido in this.data) {
                String cql_type = contenido.getCqlType(arbol);

                if (compararNombre(cql_type,"table")) {
                    String name = contenido.getName(arbol);
                    List<ColumnCQL> columns = contenido.getColumnsDefinitions(arbol);
                    List<KeyValuePair<String, Object>> values = contenido.getData(arbol);
                }
            }

            return null;
        }

        Boolean compararNombre(String n1, String n2) {
            return n1.Equals(n2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}