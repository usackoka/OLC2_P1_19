using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST
{
    public class Entorno
    {
        Entorno padre;

        public Entorno(Entorno padre) {
            this.padre = padre;
        }

    }
}