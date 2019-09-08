﻿using Server.AST;
using Server.AST.CQL;
using Server.AST.DBMS;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class DataBaseCHISON
    {
        String name;
        List<Data_Base_CHISON> data;
        int fila, columna;

        public DataBaseCHISON(String name, List<Data_Base_CHISON> data, int fila, int columna) {
            this.name = name;
            this.data = data;
            this.fila = fila;
            this.columna = columna;
        }

        public object Ejecutar(Management dbms)
        {
            //creo la base de datos
            dbms.createDataBase(this.name, fila, columna);

            //creo toda la data de la base de datos
            foreach (Data_Base_CHISON contenido in this.data) {
                String cql_type = contenido.getCqlType(dbms);

                if (compararNombre(cql_type,"table")) {
                    String name = contenido.getName(dbms);
                    List<ColumnCQL> columns = contenido.getColumnsDefinitions(dbms);
                    List<KeyValuePair<String, Object>> values = contenido.getData(dbms);
                }
            }

            return null;
        }

        Boolean compararNombre(String n1, String n2) {
            return n1.Equals(n2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}