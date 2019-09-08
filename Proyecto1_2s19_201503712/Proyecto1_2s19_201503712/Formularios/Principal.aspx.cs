using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_2s19_201503712.Formularios
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            localhost.RutasSoapClient servidor = new localhost.RutasSoapClient();

            String res0 = servidor.AnalizarPruebaCQL("[+LOGIN]  " +
                                                    "[+USER]admin[-USER]" +
                                                    "[+PASS]admin[-PASS]" +
                                                    "[-LOGIN]");
            String res1 = servidor.getErroresChison();
            String res = servidor.AnalizarPruebaCQL("[+QUERY] "+
                                                    "[+USER]" +
                                                    "   Luis" +
                                                    "[-USER]" +
                                                    "[+DATA]" + hdCadena.Value+
                                                    "[-DATA]"+
                                                    "[-QUERY]");
            //Response.Write("<script>alert('" + res + "')</script>");
            //Response.Write("<textarea>"+res+"</textarea>");
            txtSalida.Value = res0+"\n"+res1+"\n"+res;
        }
    }
}