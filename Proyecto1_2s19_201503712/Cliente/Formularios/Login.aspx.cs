using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cliente.Formularios
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove((string)entry.Key);
                }

                Session["idUser"] = null;
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            localhost.RutasSoapClient servidor = new localhost.RutasSoapClient();

            if (hdLogin.Value.Equals("true"))
            {
                Session["idUser"] = hdUser.Value;
                Response.Redirect("modoAvanzado.aspx");
            }

            String cadenaEnvio = "[+LOGIN]  " +
                                "[+USER]" + user.Value + "[-USER]" +
                                "[+PASS]" + pass.Value + "[-PASS]" +
                                "[-LOGIN]";
            hdUser.Value = user.Value;

            String res0 = servidor.AnalizarLUP(cadenaEnvio);
            String res1 = servidor.getErroresChison();
            txtEnviado.Value = cadenaEnvio;
            txtSalida.Value = "\n" + res0 + "\n" + res1;
        }
    }
}