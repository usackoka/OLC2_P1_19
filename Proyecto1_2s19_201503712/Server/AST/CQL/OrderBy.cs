using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class OrderBy
    {
        List<Order> orders;
        Hashtable ht;

        public OrderBy(List<Order> orders) {
            this.orders = orders;
            Object[] r = new Object[ht.Values.Count];
            ht.Values.CopyTo(r,0);
            
        }

        public List<ColumnCQL> getResult(List<ColumnCQL> data, AST_CQL arbol) {
            foreach (Order order in this.orders) {
                foreach (ColumnCQL column in data) {
                    if (order.id.Equals(column.id)) {
                        //
                        SortedDictionary<string, int> dict = new SortedDictionary<string, int>();
                        dict.Add("Exchange C", 200);
                        dict.Add("Exchange A", 200);
                        dict.Add("Exchange V", 100);
                    }
                }
            }
            return data;
        }
    }
}