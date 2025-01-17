﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modoAvanzado.aspx.cs" Inherits="Cliente.Formularios.modoAvanzado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%-- /////////////////////////////// BOOSTRAP /////////////////////////////////// --%>
    <link rel="stylesheet" href="css/index_style.css" />
    <link rel="stylesheet" href="css/triviu.css" />
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

    <%-- ///////////////////// EDITOR //////////////////////////////////////// --%>
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
                <li class="nav-item">
                    <a class="nav-link" href="modoPrincipiante.aspx">Principiante</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="modoIntermedio.aspx">Intermedio</a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="modoAvanzado.aspx">Avanzado</a>
                </li>
                <%--<li class="nav-item">
                    <a class="nav-link disabled" href="#">Disabled</a>
                </li>--%>
            </ul>
            <div class="form-inline my-2 my-lg-0">
                <button class="btn btn-outline-success my-2 my-sm-0" onclick="AnalizarLUP()">Analizar LUP</button>
                <form id="form1" runat="server" class="form-inline my-2 my-lg-0">
                    <asp:HiddenField ID="hdCadena" runat="server" />
                    <asp:HiddenField ID="hdCierre" runat="server" />
                    <asp:Button class="btn btn-outline-success my-2 my-sm-0" runat="server" OnClientClick="AlmacenarTexto()" OnClick="Unnamed_Click" Text="Analizar CQL"></asp:Button>
                    <asp:Button class="btn btn-outline-success my-2 my-sm-0" runat="server" OnClick="Unnamed_Click1" Text="Cerrar Sesión"></asp:Button>
                </form>
            </div>
        </div>
    </nav>
    <%-- ////////////////////////////////////////// END BARRA ////////////////////////////////////////////////// --%>

    <div>
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

        <div id="divContGeneral">
            <div id="divControless">
                <select id="desplegable" class="btn btn-outline-success my-2 my-sm-0"></select>
                <button onclick="saveTextAsFile()">Guardar Como</button>
                &nbsp; &nbsp;
                    <input type="file" id="openfile" value="Nueva Pestaña" class="form-control mr-sm-2" lang="es" />
            </div>

            <div id="divEditorTexto">
                <div class="container">
                    <div class="row">
                        <style> 
                            h1 { 
                                color:Green; 
                            } 
                            div.scroll { 
                                margin:4px, 4px; 
                                padding:4px; 
                                background-color: green; 
                                width: 500px; 
                                height: 110px; 
                                overflow-x: hidden; 
                                overflow-x: auto; 
                                text-align:justify; 
                            } 
                        </style> 
                        <div class="scroll col order-1 col-md-3" style="height: 500px;" >
                        <%-- =================== BEGIN ARBOLITO ======================= --%>
                        <ul id="myUL">
                        </ul>

                        <script type="text/javascript">
                            var toggler = document.getElementsByClassName("caret");
                            var i;

                            for (i = 0; i < toggler.length; i++) {
                                toggler[i].addEventListener("click", function () {
                                    this.parentElement.querySelector(".nested").classList.toggle("active");
                                    this.classList.toggle("caret-down");
                                });
                            }
                        </script>
                        <%-- =================== END ARBOLITO ========================= --%>
                        </div>
                        <div class="col order-2 col-md-8">
                            <form id="form_txtEntrada">
                                <textarea id="txtEntrada"></textarea>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="tab">
                    <button class="tablinks" onclick="openTab(event, 'Consola')">Consola</button>
                    <button class="tablinks" onclick="openTab(event, 'Paquetes_Enviados')">Paquetes Enviados</button>
                    <button class="tablinks" onclick="openTab(event, 'Paquetes_Recibidos')">Paquetes Recibidos</button>
                    <button class="tablinks" onclick="openTab(event, 'Errores')">Errores</button>
                    <button class="tablinks" onclick="openTab(event, 'Consultas')">Consultas</button>
                </div>
                <div id="Consola" class="tabcontent">
                    <h3>Consola</h3>
                    <textarea id="txtConsola" rows="20" cols="150" runat="server" class="text-white bg-dark"></textarea>
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
        </div>

        <script>
            function saveTextAsFile() {
                var textFileAsBlob = new Blob([txtEntrada.getValue()], { type: 'text/plain' });
                var downloadLink = document.createElement("a");
                downloadLink.download = prompt("Nombre del archivo");
                downloadLink.innerHTML = "Guardar Como";
                if (window.webkitURL != null) {
                    //Chrome allows the link to be clicked
                    //without actually adding it to the DOM.
                    downloadLink.href = window.webkitURL.createObjectURL(textFileAsBlob);
                }
                else {
                    //Firefox requires the link to be added to the DOM
                    //before it can be clicked.
                    downloadLink.href = window.URL.createObjectURL(textFileAsBlob);
                    downloadLink.onclick = destroyClickedElement;
                    downloadLink.style.display = "none";
                    document.body.appendChild(downloadLink);
                }

                downloadLink.click();
            }
        </script>

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

                //pregunto si tiene permiso de cerrar sesión
                if (ast.logout == true) {
                    document.getElementById('hdCierre').value = "true";
                    alert("Logout True");
                } else {
                    document.getElementById('hdCierre').value = "false";
                    //alert("Logout False");
                }

                //imprimo los mensajes en la consola
                for (i in ast.mensajes) {
                    document.getElementById("txtConsola").value +=
                        (ast.mensajes[i]).toString().replace("[+MESSAGE]", "").replace("[-MESSAGE]", "").replace("\n", "");
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

                //creo el arbol que muestra lo de la dbms
                var myUL = document.getElementById('myUL');
                for (i in ast.dbms.dataBases) {
                    var dataBase = ast.dbms.dataBases[i];
                    var liDb = document.createElement('li');
                    var span = document.createElement('span');
                    var ULdb = document.createElement('ul');
                    ULdb.className = 'nested';
                    span.className = 'caret';
                    span.innerHTML = dataBase.name;

                    //tablas
                    var liTabs = document.createElement('li');
                    var spanTabs = document.createElement('span');
                    var ULTabs = document.createElement('ul');
                    ULTabs.className = 'nested';
                    spanTabs.className = 'caret';
                    spanTabs.innerHTML = "Tablas";

                    //UserTypes
                    var liUser = document.createElement('li');
                    var spanUser = document.createElement('span');
                    var ULUser = document.createElement('ul');
                    ULUser.className = 'nested';
                    spanUser.className = 'caret';
                    spanUser.innerHTML = "User Types";

                    //Procedures
                    var liProc = document.createElement('li');
                    var spanProc = document.createElement('span');
                    var ULProc = document.createElement('ul');
                    ULProc.className = 'nested';
                    spanProc.className = 'caret';
                    spanProc.innerHTML = "Procedures";

                    //cargo los procedures
                    for (k in dataBase.procedures) {
                        var proc = dataBase.procedures[k];
                        var liProcedure = document.createElement('li');
                        liProcedure.innerHTML = proc;
                        ULProc.appendChild(liProcedure);
                    }

                    //cargo los userTypes
                    for (j in dataBase.types) {
                        var type = dataBase.types[j];
                        var liType = document.createElement('li');
                        var spanType = document.createElement('span');
                        var ULType = document.createElement('ul');
                        spanType.className = 'caret';
                        spanType.innerHTML = type.name;

                        //cargo las columnas de las tablas
                        for (k in type.atrs) {
                            var nombre = type.atrs[k];
                            var liAtrs = document.createElement('li');
                            liAtrs.innerHTML = nombre;
                            ULType.appendChild(liAtrs);
                        }

                        liType.appendChild(spanType);
                        liType.appendChild(ULType);
                        ULUser.appendChild(liType);
                    }

                    //cargo las tablas
                    for (j in dataBase.tables) {
                        var tabla = dataBase.tables[j];
                        var liTab = document.createElement('li');
                        var spanTab = document.createElement('span');
                        var ULTabla = document.createElement('ul');
                        spanTab.className = 'caret';
                        spanTab.innerHTML = tabla.name;

                        //cargo las columnas de las tablas
                        for (k in tabla.columns) {
                            var columna = tabla.columns[k];
                            var liCol = document.createElement('li');
                            liCol.innerHTML = columna;
                            ULTabla.appendChild(liCol);
                        }

                        liTab.appendChild(spanTab);
                        liTab.appendChild(ULTabla);
                        ULTabs.appendChild(liTab);
                    }
                    
                    //Procedures
                    liProc.appendChild(spanProc);
                    liProc.appendChild(ULProc);
                    ULdb.appendChild(liProc);
                    
                    //UserTypes
                    liUser.appendChild(spanUser);
                    liUser.appendChild(ULUser);
                    ULdb.appendChild(liUser);

                    //tablas
                    liTabs.appendChild(spanTabs);
                    liTabs.appendChild(ULTabs);
                    ULdb.appendChild(liTabs);

                    //global
                    liDb.appendChild(span);
                    liDb.appendChild(ULdb);
                    myUL.appendChild(liDb);
                }

                var toggler = document.getElementsByClassName("caret");
                var i;

                for (i = 0; i < toggler.length; i++) {
                    toggler[i].addEventListener("click", function () {
                        this.parentElement.querySelector(".nested").classList.toggle("active");
                        this.classList.toggle("caret-down");
                    });
                }
                //document.getElementById('txtConsola').value = myUL.innerHTML;
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
</body>
</html>
