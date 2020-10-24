using System;
using System.Diagnostics;
using System.IO;


namespace VisualGraphs.Classes
{
    class LogManager
    {
        private string pathDirectory;
        private StreamWriter writer;

        public LogManager()
        {
            pathDirectory = @"C:\Users\souza\AppData";
            Debug.WriteLine(pathDirectory);
            writer = File.CreateText(pathDirectory);
        }


        public void TesteFileSave(Grafo g)
        {
            writer.WriteLine(g.ToString());
            writer.Close();
        }

    }
}
