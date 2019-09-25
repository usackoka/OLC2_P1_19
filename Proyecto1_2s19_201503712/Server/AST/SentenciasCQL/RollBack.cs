using Server.AST.DBMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class RollBack : Sentencia
    {
        bool batch;
        public RollBack(int fila, int columna, bool batch) {
            this.fila = fila;
            this.columna = columna;
            this.batch = batch;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            String baseUso = arbol.dbms.system.id;
            String userActivo = arbol.dbms.usuarioActivo.id;
            arbol.dbms = new Management();
            if (batch)
            {
                arbol.dbms.analizarChison("batch");
            }
            else
            {
                arbol.dbms.analizarChison("");
            }
            arbol.dbms.usuarioActivo = arbol.dbms.getUser(userActivo);
            return arbol.dbms.useDataBase(baseUso,arbol,fila,columna) ;
        }
    }
}