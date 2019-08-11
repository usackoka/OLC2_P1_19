using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_2s19_201503712.Formularios
{
    public partial class PruebaJison : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void prueba_Click(object sender, EventArgs e)
        {
            localhost.Rutas lc = new localhost.Rutas();
            resultado.InnerText = lc.HelloWorld();
        }
    }
}