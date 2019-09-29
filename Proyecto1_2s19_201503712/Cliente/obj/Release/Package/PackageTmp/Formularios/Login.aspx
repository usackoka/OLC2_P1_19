<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Cliente.Formularios.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%-- /////////////////////////////// BOOSTRAP /////////////////////////////////// --%>
    <link rel="stylesheet" href="css/index_style.css" />
    <link rel="stylesheet" href="css/bootstrap-reboot.min.css" />
    <link rel="stylesheet" href="css/bootstrap-reboot.css" />
    <link rel="stylesheet" href="css/bootstrap-grid.min.css" />
    <link rel="stylesheet" href="css/bootstrap-grid.css" />
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <script type="text/javascript" src="js/bootstrap.bundle.js"></script>
    <script type="text/javascript" src="js/bundle.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
</head>
<body style="background-color: #7A96EE">
    <%-- //////////////////////////////////// BEGIN BARRA //////////////////////////////////////////////// --%>
    <nav class="navbar navbar-expand-lg navbar-light bg-light navbar-dark bg-dark">
        <a class="navbar-brand" href="#">Proyecto 1 - 201503712</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse"
            data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
            aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <a class="nav-link disabled">Login</a>
                </li>
                <%--<li class="nav-item">
                    <a class="nav-link disabled" href="#">Disabled</a>
                </li>--%>
            </ul>

            <div class="form-inline my-2 my-lg-0">
                <button class="btn btn-outline-success my-2 my-sm-0" onclick="AnalizarLUP()">Analizar LUP</button>
            </div>
        </div>
    </nav>
    <%-- ////////////////////////////////////////// END BARRA ////////////////////////////////////////////////// --%>

    <div>
        <br />
        <br />
        <br />
        <form class="form-row" runat="server">
            <div class="col-md-offset-10"></div>
            <div class="form-group col-md-4">
                <label for="inputEmail4">Email</label>
                <input runat="server" type="text" class="form-control" id="user" placeholder="Usuario" />
            </div>
            <div class="form-group col-md-4">
                <label for="inputPassword4">Password</label>
                <input runat="server" type="password" class="form-control" id="pass" placeholder="Contraseña" />
            </div>
            <div class="col-md-offset-10"></div>
            <asp:Button runat="server" class="btn btn-primary" Text="Ingresar" OnClick="Unnamed_Click"></asp:Button>

            <asp:HiddenField ID="hdLogin" runat="server"/>
            <asp:HiddenField ID="hdUser" runat="server"/>
        </form>
        <br />

        <div class="tab">
            <button class="tablinks" onclick="openTab(event, 'Paquetes_Enviados')">Paquetes Enviados</button>
            <button class="tablinks" onclick="openTab(event, 'Paquetes_Recibidos')">Paquetes Recibidos</button>
            <button class="tablinks" onclick="openTab(event, 'Errores')">Errores</button>
        </div>
        <div id="Paquetes_Enviados" class="tabcontent">
            <h3>Paquetes Enviados</h3>
            <textarea id="txtEnviado" rows="20" cols="150" runat="server" class="text-white bg-dark"></textarea>
        </div>
        <div id="Paquetes_Recibidos" class="tabcontent">
            <h3>Paquetes Recibidos</h3>
            <textarea id="txtSalida" rows="20" cols="150" runat="server" class="text-white bg-dark"></textarea>
        </div>
        <div id="Errores" class="tabcontent">
            <h3>Errores</h3>
            <div id="divErrores">
            </div>
        </div>
    </div>

    <script src="../Jison/Analizador.js"></script>
    <script>
        function openTab(evt, cityName) {
            // Declare all variables
            var i, tabcontent, tablinks;

            // Get all elements with class="tabcontent" and hide them
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }

            // Get all elements with class="tablinks" and remove the class "active"
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }

            // Show the current tab, and add an "active" class to the button that opened the tab
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }

        function AnalizarLUP() {
            var text = document.getElementById("txtSalida").value;
            var ast = Analizador.parse(text);

            //creo la tabla para los errores
            var divTablaErrores = document.createElement('div');
            var tablaErrores = "<table border=\"2\" style=\"margin: 0 auto;\" class=\"table table-striped table-bordered table-responsive table-dark\">" +
                "<td>No.</td>" +
                "<td>Lexema</td>" +
                "<td>Descripcion</td>" +
                "<td>Tipo</td>" +
                "<td>Fila</td>" +
                "<td>Columna</td>";
            for (i in ast.errores) {
                var error = ast.errores[i];
                tablaErrores += "<tr>";
                tablaErrores += "<td>" + i + "</td>";
                tablaErrores += "<td>" + error.lexema.toString().replace("[+LEXEMA]", "").replace("[-LEXEMA]", "") + "</td>";
                tablaErrores += "<td>" + error.descripcion.toString().replace("[+DESC]", "").replace("[-DESC]", "") + "</td>";
                tablaErrores += "<td>" + error.tipo.toString().replace("[+TYPE]", "").replace("[-TYPE]", "") + "</td>";
                tablaErrores += "<td>" + error.fila.toString().replace("[+LINE]", "").replace("[-LINE]", "") + "</td>";
                tablaErrores += "<td>" + error.columna.toString().replace("[+COLUMN]", "").replace("[-COLUMN]", "") + "</td>";
                tablaErrores += "</tr>";
            }
            tablaErrores += "</table>";
            divTablaErrores.innerHTML = tablaErrores;
            document.getElementById('divErrores').appendChild(divTablaErrores);
            
            if (ast.login == true) {
                document.getElementById('hdLogin').value = "true";
                alert("Login True");
            } else {
                document.getElementById('hdLogin').value = "false";
                alert("Login False");
            }
        }

    </script>
</body>
</html>
