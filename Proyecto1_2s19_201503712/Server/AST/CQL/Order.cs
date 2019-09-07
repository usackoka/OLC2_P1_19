using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Order
    {
        public String id;
        public ORDER tipoOrder;

        public Order(String id, ORDER tipoOrder) {
            this.id = id;
            this.tipoOrder = tipoOrder;
        }

        public enum ORDER {
            ASC, DESC
        }
    }
}