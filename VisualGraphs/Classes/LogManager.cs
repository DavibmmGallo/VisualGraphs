using System;
using System.Diagnostics;
using System.IO;


namespace VisualGraphs.Classes
{
    class LogManager
    {
        private string pathDirectory;
        Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        Windows.Storage.StorageFile sampleFile =  storageFolder.GetFileAsync("sample.txt");
        public LogManager()
        {
            pathDirectory = @"C:\Users\souza\Documents\GitHub\VisualGraphs2";
            Debug.WriteLine(pathDirectory);
        }


        public void TesteFileSave(Grafo g)
        {
            System.IO.File.WriteAllText(pathDirectory, g.ToString());
        }

    }
}
