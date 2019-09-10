using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cliente.Formularios
{
    public partial class modoAvanzado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            localhost.RutasSoapClient servidor = new localhost.RutasSoapClient();

            String res0 = servidor.AnalizarLUP("[+LOGIN]  " +
                                                    "[+USER]admin[-USER]" +
                                                    "[+PASS]admin[-PASS]" +
                                                    "[-LOGIN]");
            String res1 = servidor.getErroresChison();
            String res = servidor.AnalizarLUP("[+QUERY] " +
                                                    "[+USER]" +
                                                    "   admin" +
                                                    "[-USER]" +
                                                    "[+DATA]" + hdCadena.Value +
                                                    "[-DATA]" +
                                                    "[-QUERY]");
            txtSalida.Value = "\n" + res1 + "\n" + res;
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            Session["idUser"] = null;
        }
    }
}