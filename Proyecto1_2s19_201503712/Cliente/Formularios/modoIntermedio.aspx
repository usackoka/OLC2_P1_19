<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modoIntermedio.aspx.cs" Inherits="Cliente.Formularios.modoIntermedio" %>

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

    <%-- /////////////////////////// BLOCKLY /////////////////////////////////////// --%>
    <script src="../BlockLy/blockly_compressed.js"></script>
    <script src="../BlockLy/blocks_compressed.js"></script>
    <script src="../BlockLy/javascript_compressed.js"></script>
    <script src="../BlockLy/misBloques.js"></script>
    <script src="../BlockLy/es.js"></script>
</head>
<body>
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
                <li class="nav-item">
                    <a class="nav-link" href="modoPrincipiante.aspx">Principiante</a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="modoIntermedio.aspx">Intermedio</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="modoAvanzado.aspx">Avanzado</a>
                </li>
                <%--<li class="nav-item">
                    <a class="nav-link disabled" href="#">Disabled</a>
                </li>--%>
            </ul>
            <div class="form-inline my-2 my-lg-0">
                <button class="btn btn-outline-success my-2 my-sm-0" onclick="AnalizarLUP()">Analizar LUP</button>
                <form id="form2" runat="server" class="form-inline my-2 my-lg-0">
                    <asp:HiddenField ID="hdCadena" runat="server" />
                    <asp:HiddenField ID="hdCierre" runat="server" />
                    <asp:Button class="btn btn-outline-success my-2 my-sm-0" runat="server" OnClientClick="AlmacenarTexto()" OnClick="Unnamed_Click" Text="Analizar CQL"></asp:Button>
                    <asp:Button class="btn btn-outline-success my-2 my-sm-0" runat="server" OnClick="Unnamed_Click1" Text="Cerrar Sesión"></asp:Button>
                </form>
            </div>
        </div>
    </nav>
    <%-- ////////////////////////////////////////// END BARRA ////////////////////////////////////////////////// --%>

    <form id="form1">
        <div id="DivBlockly" style="height: 400px; width: 1350px"></div>
        <input type="button" onclick="MostrarCodigo()" value="MostrarCodigo" />
        <xml id="toolbox" style="display: none">
             <category name="Sentencias">
                <block type="select"></block>
                <block type="where"></block>
                <block type="where_2"></block>
                <block type="orderby"></block>
                <block type="limit"></block>
                <block type="insert"></block>
                <block type="update"></block>
                <block type="delete"></block>
             </category>
            <category name="Control dbms">
                <block type="use"></block>
            </category>
             <category name="Atributos">
                <block type="text"></block>
                <block type="seleccion"></block>
             </category>
             <category name="Valores">
                <block type="text"></block>
                <block type="lists_create_with"></block>
                <block type="math_number"></block>
            </category>
            <category name="Operadores">
                <block type="logic_compare"></block>
                <block type="math_arithmetic"></block>
            </category>
            <category name="Sentencias de Control" colour="330">
                <block type="controls_if"></block>
            </category>
            <category name="Variables" colour="330">
                <block type="var"></block>
                <block type="reasignar"></block>
                <block type="valinstancia"></block>
                <block type="create_var"></block>

            </category>
        </xml>
        <script>
            var EspacioTrabajo = Blockly.inject('DivBlockly', { toolbox: document.getElementById('toolbox') });
            function MostrarCodigo() {
                var codigo = Blockly.JavaScript.workspaceToCode(EspacioTrabajo);
                document.getElementById('txtBlockly').innerHTML = codigo;
            }
        </script>
    </form>

    <div id="divEditorTexto">
        <div class="tab">
            <button class="tablinks" onclick="openTab(event, 'Consola')">Consola</button>
            <button class="tablinks" onclick="openTab(event, 'Paquetes_Enviados')">Paquetes Enviados</button>
            <button class="tablinks" onclick="openTab(event, 'Paquetes_Recibidos')">Paquetes Recibidos</button>
            <button class="tablinks" onclick="openTab(event, 'Generado_Blockly')">Generado Blockly</button>
            <button class="tablinks" onclick="openTab(event, 'Errores')">Errores</button>
            <button class="tablinks" onclick="openTab(event, 'Consultas')">Consultas</button>
        </div>
        <div id="Consola" class="tabcontent">
            <h3>Consola</h3>
            <textarea id="txtConsola" rows="20" cols="150" runat="server" class="text-white bg-dark"></textarea>
        </div>
        <div id="Generado_Blockly" class="tabcontent">
            <h3>Generado Blockly</h3>
            <textarea id="txtBlockly" rows="20" cols="150" runat="server" class="text-white bg-dark"></textarea>
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
        <div id="Consultas" class="tabcontent">
            <h3>Consultas</h3>
            <div id="divConsultas">
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

        function AlmacenarTexto() {
            document.getElementById('hdCadena').value = document.getElementById('txtBlockly').value;
        }

        function AnalizarLUP() {
            var text = document.getElementById("txtSalida").value;
            var ast = Analizador.parse(text);

            //pregunto si tiene permiso de cerrar sesión
            if (ast.logout == true) {
                document.getElementById('hdCierre').value = "true";
                alert("Logout True");
            } else {
                document.getElementById('hdCierre').value = "false";
                alert("Logout False");
            }

            //imprimo los mensajes en la consola
            for (i in ast.mensajes) {
                document.getElementById("txtConsola").value +=
                    (ast.mensajes[i]).toString().replace("[+MESSAGE]", "").replace("[-MESSAGE]", "") + "\n";
            }

            //creo la tabla para las consultas
            for (i in ast.data) {
                var divTablaConsulta = document.createElement('div');
                var data = ast.data[i];
                divTablaConsulta.innerHTML += data.toString().replace("[+DATA]", "").replace("[-DATA]", "");
                document.getElementById('divConsultas').appendChild(divTablaConsulta);
            }

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
        }
    </script>
</body>
</html>
