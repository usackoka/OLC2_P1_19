using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.AST.CQL
{
    public class OrderBy
    {
        List<Order> orders;
        Hashtable ht;

        public OrderBy(List<Order> orders)
        {
            this.orders = orders;
        }

        public List<ColumnCQL> getResult(List<ColumnCQL> data, AST_CQL arbol)
        {
            if (this.orders.Count<1) {
                return data;
            }

            Order order = orders[0];

            //obtengo la columna sobre la cual se ordenará
            List<Object> valoresOrder = new List<object>();
            foreach (ColumnCQL column in data) {
                if (column.id.Equals(order.id, StringComparison.InvariantCultureIgnoreCase)) {
                    valoresOrder.AddRange(column.valores);
                    break;
                }
            }

            //ordeno
            foreach (ColumnCQL column in data) {
                SortedDictionary<Object, Object> dict = new SortedDictionary<Object, Object>();
                for (int i = 0; i < valoresOrder.Count; i++)
                {
                    try
                    {
                        dict.Add(valoresOrder[i], column.valores[i]);
                    }
                    catch (Exception)
                    {
                        arbol.addError("IndexOutOfBounds - OrderBy", "Recuperado", 0, 0);
                    }
                }

                //regreso los valores
                int n = 0;
                foreach (KeyValuePair<Object,Object> kvp in dict)
                {
                    try
                    {
                        column.valores[n++] = kvp.Value;
                    }
                    catch (Exception)
                    {
                        arbol.addError("IndexOutOfBounds - OrderBy","Recuperado",0,0);
                    }
                }
            }

            return data;
        }
    }
}