<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Proyecto1_2s19_201503712.Formularios.Principal" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Basic -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!-- Mobile Metas -->
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <!-- Site Metas -->
    <title>OLC2 - Proyecto 1</title>
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Site Icons -->
    <link rel="shortcut icon" href="Plantilla/images/logo2.ico" type="image/x-icon" />
    <link rel="apple-touch-icon" href="Plantilla/images/logo2.png">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="Plantilla/css/bootstrap.min.css">
    <!-- Site CSS -->
    <link rel="stylesheet" href="css/style.css">
    <!-- Colors CSS -->
    <link rel="stylesheet" href="Plantilla/css/colors.css">
    <!-- ALL VERSION CSS -->
    <link rel="stylesheet" href="Plantilla/css/versions.css">
    <!-- Responsive CSS -->
    <link rel="stylesheet" href="Plantilla/css/responsive.css">
    <!-- Custom CSS -->
    <link rel="stylesheet" href="Plantilla/css/custom.css">

    <%-- ///////////////////// EDITOR //////////////////////////////////////// --%>
    <link rel="stylesheet" href="css/index_style.css" />
    <link rel="stylesheet" href="../codeMirror/doc/docs.css" />
    <link rel="stylesheet" href="../codeMirror/lib/codemirror.css" />
    <link rel="stylesheet" href="../codeMirror/addon/fold/foldgutter.css" />
    <link rel="stylesheet" href="../codeMirror/addon/dialog/dialog.css" />
    <link rel="stylesheet" href="../codeMirror/theme/monokai.css" />
    <script src="../codeMirror/lib/codemirror.js"></script>
    <script src="../codeMirror/addon/search/searchcursor.js"></script>
    <script src="../codeMirror/addon/search/search.js"></script>
    <script src="../codeMirror/addon/dialog/dialog.js"></script>
    <script src="../codeMirror/addon/edit/matchbrackets.js"></script>
    <script src="../codeMirror/addon/edit/closebrackets.js"></script>
    <script src="../codeMirror/addon/comment/comment.js"></script>
    <script src="../codeMirror/addon/wrap/hardwrap.js"></script>
    <script src="../codeMirror/addon/fold/foldcode.js"></script>
    <script src="../codeMirror/addon/fold/brace-fold.js"></script>
    <script src="../codeMirror/mode/javascript/javascript.js"></script>
    <script src="../codeMirror/mode/sql/sql.js"></script>
    <script src="../codeMirror/mode/css/css.js"></script>
    <script src="../codeMirror/keymap/sublime.js"></script>
</head>
<body class="barber_version">

    <!-- LOADER -->
    <div id="preloader">
        <div class="cube-wrapper">
            <div class="cube-folding">
                <span class="leaf1"></span>
                <span class="leaf2"></span>
                <span class="leaf3"></span>
                <span class="leaf4"></span>
            </div>
            <span class="loading" data-name="Loading">OLC2-LOADING</span>
        </div>
    </div>
    <!-- end loader -->
    <!-- END LOADER -->

    <div id="wrapper">

        <!-- Sidebar-wrapper -->
        <div id="sidebar-wrapper">
            <div class="side-top">
                <div class="logo-sidebar">
                    <a href="Principal.aspx">
                        <img src="Plantilla/images/logos/logo2.png" alt="image"></a>
                </div>
                <ul class="sidebar-nav">
                    <li><a class="active" href="Principal.aspx">Inicio</a></li>
                    <li><a href="#">Modo Principiante</a></li>
                    <li><a href="#">Modo Intermedio</a></li>
                    <li><a href="#">Modo Avanzado</a></li>
                    <li><a href="#">Reporte de Errores</a></li>
                    <li><a href="#">Cerrar Sesión</a></li>
                </ul>
            </div>
        </div>
        <!-- End Sidebar-wrapper -->

        <!-- Page Content -->
        <div >
            <style>
                .CodeMirror {
                    border-top: 1px solid #eee;
                    border-bottom: 1px solid #eee;
                    line-height: 1.3;
                    height: 500px
                }

                .CodeMirror-linenumbers {
                    padding: 0 8px;
                }
            </style>

            <div id="div1">
                <div id="divControles">
                    Seleccionar Pestaña:
                    <select id="desplegable"></select>
                    &nbsp; &nbsp;
                    <input type="file" id="openfile" value="Nueva Pestaña" />
                    <button onclick="AnalizarLUP()">Analizar LUP</button>
                    <form id="form1" runat="server">
                        <asp:HiddenField ID="hdCadena" runat="server" />
                        <asp:Button runat="server" OnClientClick="AlmacenarTexto()" OnClick="Unnamed_Click" Text="Analizar CQL"></asp:Button>
                    </form>
                </div>

                <div id="divEntrada">
                    <h4>Respuesta Server</h4>
                    <textarea id="txtSalida" rows="14" cols="60" runat="server"></textarea>
                    <h4>Paquetes Enviados</h4>
                    <textarea id="txtEnviado" rows="14" cols="60" runat="server"></textarea>
                </div>

                <div id="divEditor">
                    <form id="form_txtEntrada">
                        <textarea id="txtEntrada"></textarea>
                    </form>
                    <div class="tab">
                        <button class="tablinks" onclick="openTab(event, 'Consola')">Consola</button>
                        <button class="tablinks" onclick="openTab(event, 'Errores')">Errores</button>
                    </div>
                    <div id="Consola" class="tabcontent">
                        <h3>Consola</h3>
                        <textarea id="txtConsola" rows="15" cols="80" runat="server"></textarea>
                    </div>
                    <div id="Errores" class="tabcontent">
                        <h3>Errores</h3>
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

                    //imprimo los mensajes en la consola
                    for (i = 0; i < ast.mensajes.length; i++) {
                        document.getElementById("txtConsola").value +=
                            (ast.mensajes[i]).toString().replace("[+MESSAGE]", "").replace("[-MESSAGE]", "") + "\n";
                    }

                    //creo la tabla para las consultas
                    var divTablaConsulta = document.createElement('div');
                    for (i = 0; i < ast.data.length; i++) {
                        var data = ast.data[i];
                        divTablaConsulta.innerHTML += data.toString().replace("[+DATA]", "").replace("[-DATA]", "");
                    }
                    document.getElementById('divEditor').appendChild(divTablaConsulta);

                    //creo la tabla para los errores
                    var divTablaErrores = document.createElement('div');
                    var tablaErrores = "<table border=\"2\" style=\"margin: 0 auto;\">" +
                        "<td>No.</td>" +
                        "<td>Lexema</td>" +
                        "<td>Descripcion</td>" +
                        "<td>Tipo</td>" +
                        "<td>Fila</td>" +
                        "<td>Columna</td>";
                    for (i = 0; i < ast.errores.length; i++) {
                        var error = ast.errores[i];
                        tablaErrores += "<tr>";
                        tablaErrores += "<td>" + (i + 1) + "</td>";
                        tablaErrores += "<td>" + error.lexema.toString().replace("[+LEXEMA]", "").replace("[-LEXEMA]", "") + "</td>";
                        tablaErrores += "<td>" + error.descripcion.toString().replace("[+DESC]", "").replace("[-DESC]", "") + "</td>";
                        tablaErrores += "<td>" + error.tipo.toString().replace("[+TYPE]", "").replace("[-TYPE]", "") + "</td>";
                        tablaErrores += "<td>" + error.fila.toString().replace("[+LINE]", "").replace("[-LINE]", "") + "</td>";
                        tablaErrores += "<td>" + error.columna.toString().replace("[+COLUMN]", "").replace("[-COLUMN]", "") + "</td>";
                        tablaErrores += "</tr>";
                    }
                    tablaErrores += "</table>";
                    divTablaErrores.innerHTML = tablaErrores;
                    document.getElementById("Errores").appendChild(divTablaErrores);
                }
            </script>

            <script>
                var cont = 0;
                var fileInput = document.getElementById('openfile');
                var fileDisplayArea = document.getElementById('fileDisplayArea');

                fileInput.addEventListener('change', function () {
                    var fr = new FileReader();
                    fr.onload = function () {
                        newBuf(this.result, "Pestaña" + (cont++));
                    }
                    fr.readAsText(this.files[0]);
                });
            </script>

            <script>
                var desplegable = document.getElementById("desplegable");
                CodeMirror.on(desplegable, "change", function () {
                    selectBuffer(txtEntrada, desplegable.options[desplegable.selectedIndex].value);
                });

                var buffers = {};

                var txtEntrada = CodeMirror.fromTextArea(document.getElementById('txtEntrada'), {
                    lineNumbers: true,
                    mode: "text/x-mssql",
                    keyMap: "sublime",
                    autoCloseBrackets: true,
                    matchBrackets: true,
                    showCursorWhenSelecting: true,
                    theme: "monokai"
                });

                openBuffer("untitled", "", "text/x-mssql");
                selectBuffer(txtEntrada, "untitled");

                function AlmacenarTexto() {
                    document.getElementById('hdCadena').value = txtEntrada.getValue();
                }

                function openBuffer(name, text, mode) {
                    buffers[name] = CodeMirror.Doc(text, mode);
                    var opt = document.createElement("option");
                    opt.appendChild(document.createTextNode(name));
                    desplegable.appendChild(opt);
                }

                function newBuf(texto, name) {
                    if (name == null) return;
                    if (buffers.hasOwnProperty(name)) {
                        alert("Ya hay una pestaña con este nombre");
                        return;
                    }
                    openBuffer(name, texto, "text/x-mssql");
                    selectBuffer(txtEntrada, name);
                    var sel = desplegable;
                    sel.value = name;
                }

                function selectBuffer(editor, name) {
                    var buf = buffers[name];
                    if (buf.getEditor()) buf = buf.linkedDoc({ sharedHist: true });
                    var old = editor.swapDoc(buf);
                    var linked = old.iterLinkedDocs(function (doc) { linked = doc; });
                    if (linked) {
                        // Make sure the document in buffers is the one the other view is looking at
                        for (var name in buffers) if (buffers[name] == old) buffers[name] = linked;
                        old.unlinkDoc(linked);
                    }
                    editor.focus();
                }

                function nodeContent(id) {
                    var node = document.getElementById(id), val = node.textContent || node.innerText;
                    val = val.slice(val.match(/^\s*/)[0].length, val.length - val.match(/\s*$/)[0].length) + "\n";
                    return val;
                }
            </script>
        </div>
    </div>
    <!-- end wrapper -->

    <a href="#" id="scroll-to-top" class="dmtop global-radius"><i class="fa fa-angle-up"></i></a>

    <!-- ALL JS FILES -->
    <script src="Plantilla/js/all.js"></script>
    <script src="Plantilla/js/responsive-tabs.js"></script>
    <!-- ALL PLUGINS -->
    <script src="Plantilla/js/custom.js"></script>

    <!-- Menu Toggle Script -->
    <script>
        (function ($) {
            "use strict";
            $("#menu-toggle").click(function (e) {
                e.preventDefault();
                $("#wrapper").toggleClass("toggled");
            });
            smoothScroll.init({
                selector: '[data-scroll]', // Selector for links (must be a class, ID, data attribute, or element tag)
                selectorHeader: null, // Selector for fixed headers (must be a valid CSS selector) [optional]
                speed: 500, // Integer. How fast to complete the scroll in milliseconds
                easing: 'easeInOutCubic', // Easing pattern to use
                offset: 0, // Integer. How far to offset the scrolling anchor location in pixels
                callback: function (anchor, toggle) { } // Function to run after scrolling
            });
        })(jQuery);
    </script>

</body>
</html>
