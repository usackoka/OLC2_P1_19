using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Server.Otros
{
    public class Graficar
    {
        public static String graph = "";

        public static void ConstruirArbol(ParseTreeNode raiz, String nombre, String ruta)
        {
            if (ruta.Equals(""))
                ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"AST_GRAPH");

            try
            {
                System.IO.File.Delete(ruta + "\\" + nombre + ".dot");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            System.IO.StreamWriter f = new System.IO.StreamWriter(ruta + "\\" + nombre + ".dot");
            f.Write("digraph lista{ rankdir=TB;node[shape = box, style = filled, color = white]; ");
            graph = "";
            Recorrido(raiz);
            f.Write(graph);
            f.Write("}");
            f.Close();

            //Genero la imagen
            ExecuteCommand("dot -Tpng " + ruta + "\\" + nombre + ".dot -o " + ruta + "\\" + nombre + ".png");
            //Abro la imagen
            Process.Start("mspaint.exe", ruta + "\\" + nombre + ".png");
        }

        public static void Recorrido(ParseTreeNode raiz)
        {
            graph = graph + "nodo" + raiz.GetHashCode() + "[label=\"" + raiz.ToString().Replace("\"", "\\\"") + " \", fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"]; \n";
            if (raiz.ChildNodes.Count > 0)
            {
                ParseTreeNode[] hijos = raiz.ChildNodes.ToArray();
                for (int i = 0; i < raiz.ChildNodes.Count; i++)
                {
                    Recorrido(hijos[i]);
                    graph = graph + "\"nodo" + raiz.GetHashCode() + "\"-> \"nodo" + hijos[i].GetHashCode() + "\" \n";
                }
            }
        }

        static void ExecuteCommand(string _Command)
        {
            //Indicamos que deseamos inicializar el proceso cmd.exe junto a un comando de arranque. 
            //(/C, le indicamos al proceso cmd que deseamos que cuando termine la tarea asignada se cierre el proceso).
            //Para mas informacion consulte la ayuda de la consola con cmd.exe /? 
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
            // Indicamos que la salida del proceso se redireccione en un Stream
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            //Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
            procStartInfo.CreateNoWindow = false;
            //Inicializa el proceso
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            //Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
            string result = proc.StandardOutput.ReadToEnd();
            //Muestra en pantalla la salida del Comando
            Console.WriteLine(result);
        }

    }
}