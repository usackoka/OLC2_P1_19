using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Select_Type
    {
        List<Expresion> expresiones; 

        public Select_Type() {
            this.expresiones = null;
        }

        public Select_Type(List<Expresion> expresiones) {
            this.expresiones = expresiones;
        }

        public Object getResult(String idTabla, Where where, AST_CQL arbol) {
            //tabla resultado
            List<ColumnCQL> data = new List<ColumnCQL>();

            //Obtengo la tabla y veo si existe
            TableCQL tabla = arbol.dbms.getTable(idTabla);
            if (tabla==null) {
                return Catch.EXCEPTION.TableDontExists;
            }

            //creo un entorno para estas nuevas variables que llevarán el control del select
            arbol.entorno = new Entorno(arbol.entorno);

            //creo las variables iteradoras del select
            foreach (ColumnCQL column in tabla.data) {
                arbol.entorno.addVariable("$"+column.id,new Variable(Primitivo.getDefecto(column.tipoDato,arbol),
                    column.tipoDato),arbol,0,0);
            }

            //le creo las columnas
            if (expresiones != null)
            {
                for (int i = 0; i < expresiones.Count; i++)
                {
                    data.Add(new ColumnCQL("Columna " + (i + 1), expresiones[i].getTipo(arbol), false, 0, 0));
                }
            }
            else
            {
                for (int i = 0; i < tabla.data.Count; i++)
                {
                    data.Add(new ColumnCQL(tabla.data[i].id, tabla.data[i].tipoDato, false, 0, 0));
                }
            }

            //=========================== LLENAR LA TABLA DE RESULTADOS ===============================
            int indiceFor = tabla.data.Count!=0?tabla.data[0].valores.Count:0; //Número de tuplas
            //hago el for de columnas
            for (int i = 0; i < indiceFor; i++)
            {
                //doy valores a las variables de las columnas
                foreach (ColumnCQL column in tabla.data)
                {
                    arbol.entorno.reasignarVariable("$"+column.id,column.valores[i],column.tipoDato,arbol,0,0);
                }

                //creo una tupla resultado si se cumple el where
                if (where==null || Convert.ToBoolean(where.getValor(arbol))) {
                    //recorro las columnas que llevarán la tupla
                    for(int j =0; j<data.Count;j++) {
                        if (this.expresiones != null)
                        {
                            Object val = this.expresiones[j].getValor(arbol);
                            data[j].valores.Add(val);
                        }
                        else {
                            Object val = (new Primitivo("$" + tabla.data[j].id + " (Identifier)", 0, 0).getValor(arbol));
                            data[j].valores.Add(val);
                        }
                    }
                }
            }

            //regreso el entorno
            arbol.entorno = arbol.entorno.padre;

            //retorno la lista con el resultado del select
            return data;
        }
    }
}