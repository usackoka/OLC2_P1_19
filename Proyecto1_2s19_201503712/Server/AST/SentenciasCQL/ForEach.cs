using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class ForEach : Sentencia
    {
        List<KeyValuePair<String, Object>> parametros;
        String idCursor;
        List<NodoCQL> instrucciones;

        public ForEach(List<KeyValuePair<String,Object>> parametros, String idCursor, List<NodoCQL> instrucciones,
            int fila, int columna) {
            this.parametros = parametros;
            this.idCursor = idCursor;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            //creo el nuevo entorno para las variables iteradoras
            arbol.entorno = new Entorno(arbol.entorno);

            //obtengo el cursor y el resultado del select
            Cursor cursor = (Cursor)arbol.entorno.getValorVariable(this.idCursor, arbol, fila, columna);
            List<ColumnCQL> data = cursor.data != null ? cursor.data : new List<ColumnCQL>();

            //si no hay resultados en el select
            if (data.Count == 0) {
                return null;
            }

            //pregunto si la cantidad de selects corresponde a la cantidad de parámetros del foreach
            if (data.Count != parametros.Count) {
                arbol.addError("ForEach","No existe la misma cantidad de columnas seleccionadas que variables declaradas en el foreach",fila,columna);
            }

            //creo las variables iteradoras del foreach
            foreach (KeyValuePair<String, Object> kvp in parametros)
            {
                arbol.entorno.addVariable(kvp.Key, new Variable(Primitivo.getDefecto(kvp.Value, arbol), kvp.Value),arbol,fila,columna);
            }

            //ejecuto el foreach
            int indexFor = data[0].valores.Count;
            for (int i = 0; i < indexFor; i++)
            {
                //nuevo entorno para el foreach
                arbol.entorno = new Entorno(arbol.entorno);

                //doy valor a las variables del foreach
                for(int j=0; j<parametros.Count;j++)
                {
                    KeyValuePair<String, Object> kvp = parametros[j];
                    Object valor = data[j].valores[i];
                    arbol.entorno.reasignarVariable(kvp.Key,valor,kvp.Value,arbol,fila,columna);
                }

                //ejecuto las sentencias
                foreach (NodoCQL nodo in this.instrucciones)
                {
                    if (nodo is Sentencia)
                    {
                        Object val = ((Sentencia)nodo).Ejecutar(arbol);
                        if (val != null)
                        {
                            arbol.entorno = arbol.entorno.padre;
                            return val;
                        }
                    }
                    else
                    {
                        ((Expresion)nodo).getValor(arbol);
                    }
                }

                arbol.entorno = arbol.entorno.padre;
            }

            arbol.entorno = arbol.entorno.padre;
            return null;
        }
    }
}