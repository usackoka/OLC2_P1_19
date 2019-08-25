using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class OrderBy
    {
        List<Order> orders;

        public OrderBy(List<Order> orders) {
            this.orders = orders;
        }
    }
}