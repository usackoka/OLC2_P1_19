<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaJison.aspx.cs" Inherits="Proyecto1_2s19_201503712.Formularios.PruebaJison" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../Jison/Parser.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <style>
          .thumb {
            height: 75px;
            border: 1px solid #000;
            margin: 10px 5px 0 0;
          }
        </style>

        <input type="file" id="files" name="files[]" multiple />
        <output id="list"></output>
        <pre id="contenido-archivo"></pre>

        <script>
          function handleFileSelect(evt) {
            var files = evt.target.files; // FileList object

            // Loop through the FileList and render image files as thumbnails.
            for (var i = 0, f; f = files[i]; i++) {
                /*
              // Only process image files.
              if (!f.type.match('image.*')) {
                continue;
                }
                */

              var reader = new FileReader();
                /*
              // Closure to capture the file information.
              reader.onload = (function(theFile) {
                return function(e) {
                  // Render thumbnail.
                  var span = document.createElement('span');
                  span.innerHTML = ['<img class="thumb" src="', e.target.result,
                                    '" title="', escape(theFile.name), '"/>'].join('');
                  document.getElementById('list').insertBefore(span, null);
                };
              })(f);*/

              // Read in the image file as a data URL.
                //reader.readAsDataURL(f);
                var contenido = reader.readAsText(f);
                var elemento = document.getElementById('contenido-archivo');
                elemento.innerHTML = contenido;
            }
          }

          document.getElementById('files').addEventListener('change', handleFileSelect, false);
        </script>


    </form>
</body>
</html>
