using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace VisualGraphs.Classes
{
    /// <summary>
    /// This class serves for manage vertex adjacences in some functions
    /// </summary>
    class AdjList
    {
        private List<List<int>> Adj;
        private int N { get; set; }
        private int M { get; set; }
        private FileSavePicker savePicker = new FileSavePicker();
        private MessageDialog msgdi;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grafo"> Graph</param>
        public AdjList(Grafo grafo) 
        {
            Adj = new List<List<int>>();
            N = grafo.NumVertices();
            M = grafo.NumArestas();

            while (Adj.Count < N)
                Adj.Add(new List<int>());

            foreach (var aresta in grafo.Arestas)
            {
                Adj[aresta.vertice1._id].Add(aresta.vertice2._id);
                if(!grafo.isDigraph)
                    Adj[aresta.vertice2._id].Add(aresta.vertice1._id);
            }          
            
        }
        /// <summary>
        /// Get A List of adjacences
        /// </summary>
        /// <returns>List<></returns>
        public List<List<int>> get_Adj()
        {
            return Adj;
        }
        /// <summary>
        /// Updates all adjacences from Graph
        /// </summary>
        /// <param name="grafo"></param>
        public void Update(Grafo grafo)
        {
            N = grafo.NumVertices();
            M = grafo.NumArestas();

            while (Adj.Count < N)
                Adj.Add(new List<int>());
            }


            foreach (var aresta in grafo.Arestas)
            {
                if (!Adj[aresta.vertice1._id].Contains(aresta.vertice2._id))
                {
                    Adj[aresta.vertice1._id].Add(aresta.vertice2._id);
                    if (!grafo.isDigraph)
                        Adj[aresta.vertice2._id].Add(aresta.vertice1._id);
                }
                
            }
        }
        /// <summary>
        /// Get all neighborhoods of an vertex
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public List<int> get_Adj(Vertice v)
        {
            return Adj[v._id];
        }
        /// <summary>
        /// Cleans all neighborhoods of an vertex
        /// CAUTION! DO NOT USE THIS FUNCTION UNLESS IT'S FOR REMOVE A VERTEX FROM GRAPH.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="g"></param>
        public void Clear_adj(Vertice v, Grafo g)
        {
            foreach(var aresta in g.Arestas.ToList())
            {
                if (aresta.Contains(v))
                {
                    g.RemoveAresta(aresta);
                    if (aresta.vertice1._id > v._id) aresta.vertice1._id--;
                    if (aresta.vertice2._id > v._id) aresta.vertice2._id--;
                }
            }
            
            foreach(var i in Adj[v._id].ToList())
            {
                Adj[i].Remove(v._id);
                foreach (var j in Adj[i].ToList())
                    if (j > v._id) Adj[i][Adj[i].IndexOf(j)]--;
            }
            Adj.RemoveAt(v._id);
        }
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string retrn = "";
            for(int i = 0; i < N; i++)
            {
                for(int j=0; j < M; j++)
                {
                   
                    if (!Equals(Adj[i][j],null))
                    {
                        retrn += Adj[i][j].ToString();
                    }
                    
                    retrn += " ";
                }
                retrn += "\n";
            }
            return retrn;
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
