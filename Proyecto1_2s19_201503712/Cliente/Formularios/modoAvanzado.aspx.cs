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
            String res1 = servidor.getErroresChison();
            String user = "admin";
            if (Session["idUser"]!=null) {
                user = Session["idUser"].ToString();
            }
            String enviado = "[+QUERY] " +
                                                    "[+USER]" +
                                                    "   " + user +
                                                    "[-USER]" +
                                                    "[+DATA]" + hdCadena.Value +
                                                    "[-DATA]" +
                                                    "[-QUERY]";
            String res = servidor.AnalizarLUP(enviado);
            txtSalida.Value = "\n" + res1 + "\n" + res;
            txtEnviado.Value = enviado;
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            if (hdCierre.Value.Equals("true"))
            {
                Session["idUser"] = null;
                Response.Redirect("Login.aspx");
            }
            else {
                localhost.RutasSoapClient servidor = new localhost.RutasSoapClient(); String user = "admin";
                String res1 = servidor.getErroresChison();
                if (Session["idUser"] != null)
                {
                    user = Session["idUser"].ToString();
                }
                String enviado = "[+LOGOUT]\n" +
                    "   [+USER]\n" +
                    "       "+user+"\n"+
                    "   [-USER]\n"+
                    "[-LOGOUT]";
                String res = servidor.AnalizarLUP(enviado);
                txtSalida.Value = "\n" + res1 + "\n" + res;
                txtEnviado.Value = enviado;
            }
        }
    }
}