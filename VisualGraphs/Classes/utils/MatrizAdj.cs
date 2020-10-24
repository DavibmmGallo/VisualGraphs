using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Popups;

namespace VisualGraphs.Classes
{
    class MatrizAdj : AdjList
    {
        public MatrizAdj(Grafo grafo):base(grafo){ this.grafo = grafo; }

        private Grafo grafo;

        private FileSavePicker savePicker = new FileSavePicker();
        private MessageDialog msgdi;

        bool is_bounded(Vertice vx, Vertice vy)
        {
            foreach ( var aresta in grafo.Arestas)
            {
                if ((aresta.vertice1._id == vx._id && aresta.vertice2._id == vy._id) || (aresta.vertice1._id == vy._id && aresta.vertice2._id == vx._id))
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            string output = "";
            bool f = true;
            int p = 0, g = 0;
            
            for (int i = 0; i < grafo.NumVertices() + 1; i++)
            {
                if (f)
                {
                    output += "  ";
                    foreach (var vertice in grafo.Vertices)
                    {
                        output+= $" {vertice.Label}";
                    }
                    output += "\n";
                    f = false;
                }
                else
                {
                    output += grafo.BuscaVertice(i - 1).Label + " ";
                    while (p < grafo.NumVertices())
                    {
                        output += " ";/*
                        if (p >= g)
                        {*/
                            if (is_bounded(grafo.BuscaVertice(i - 1), grafo.BuscaVertice(p))) 
                                output += "1";
                            else
                                output += "0";
                        /*}
                        else
                            output += " ";
                        */p++;
                    }
                    g++;
                    p = 0;
                    output += "\n";
                }
            }
            return output;
        }

        public async Task SaveAsync()
        {
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "New Document";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, this.ToString());

                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                if (status == FileUpdateStatus.Complete)
                {
                    msgdi = new MessageDialog($"File " + file.Name + " was saved.");
                }
                else
                {
                    msgdi = new MessageDialog($"File " + file.Name + " couldn't be saved.");
                }
            }
            else
            {
                msgdi = new MessageDialog($"Operation cancelled.");
               
            }
            await msgdi.ShowAsync();
        }
    }
}
