using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VisualGraphs.Classes
{
    class Grafo
    {

        public List<Vertice> Vertices { get; set; }
        public List<Aresta> Arestas { get; set; }
        public bool isDigraph;
        public string name { get; set; }

        /// <summary>
        /// [Grafo Class]
        /// </summary>
        /// <param name="__directed__">Tells whether the graph is directed.</param>
        public Grafo(bool __directed__)
        {
            isDigraph = __directed__;
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();
        }
        /// <summary>
        /// Adds Vertice into Grafo.
        /// </summary>
        /// <param name="v"></param>
        public void AddVertice(Vertice v)
        {
            Vertices.Add(v);
        }
        /// <summary>
        /// Removes Vertice from Grafo.
        /// </summary>
        /// <param name="v"></param>
        public void RemoveVertice(Vertice v)
        {
            Vertices.Remove(v);
        }
        /// <summary>
        /// Overload, creates new Vertice from id and lbl.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lbl"></param>
        public void AddVertice(int id, string lbl)
        {
            Vertices.Add(new Vertice(lbl, id));
        }
        /// <summary>
        /// Adds Aresta into Grafo
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool AddAresta(Aresta a)
        {
            if(a.isDirected == isDigraph)
            {
                if(!Vertices.Contains(a.vertice1)) 
                    Vertices.Add(a.vertice1);
                
                if(!Vertices.Contains(a.vertice2)) 
                    Vertices.Add(a.vertice2);
               
                Arestas.Add(a);
            }
            return false;
        }
        /// <summary>
        /// Adds Aresta into Grafo
        /// </summary>
        /// <param name="a">TODO</param>
        /// <returns></returns>
        public bool RemoveAresta(Aresta a)
        {
            if (a.isDirected == isDigraph)
            {
                Arestas.Remove(a);
            }
            return false;
        }
        /// <summary>
        /// Returns the number of Vertices.
        /// </summary>
        /// <returns>Vertices.Count();</returns>
        public int NumVertices()
        {
            return Vertices.Count;
        }
        /// <summary>
        /// Returns the number of Arestas.
        /// </summary>
        /// <returns>Arestas.Count();</returns>
        public int NumArestas()
        {
            return Arestas.Count;
        }

        #region Buscas
        /// <summary>
        /// Searches the vertice by id.
        /// </summary>
        /// <param name="id">The id from the vertice that will be seek.</param>
        /// <returns>Vertice.atIndex(id);</returns>
        public Vertice BuscaVertice(int id)
        {
            Vertice temp;
            foreach(Vertice v in Vertices)
            {
                if(v._id == id)
                {
                    temp = v;
                    return temp;
                }
            }
            return null;
        }
        /// <summary>
        /// Recursive search by depth.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="visited"></param>
        protected void DFS_search(int v, bool[] visited)
        {
            visited[v] = true;
            Debug.WriteLine(v + " ");

            List<Vertice> vList = Vertices;
            foreach (var n in vList)
            {
                if (!visited[n._id])
                    DFS_search(n._id, visited);
            }
        }
        /// <summary>
        /// Searches all connected vertices from the source by DFS.
        /// </summary>
        /// <param name="source">First Vertice to be search</param>
        public void BuscaEmProfundidade(int source)
        {
            bool[] visited = new bool[Vertices.Count];

            DFS_search(source, visited);
        }

        public void BuscaEmLargura()
        {
            //TODO
        }
        /// <summary>
        /// Searches the vertice by label.
        /// </summary>
        /// <param name="label">The label from the vertice that will be seek.</param>
        /// <returns>Vertice.atLabel(label);</returns>
        public Vertice BuscaVertice(string label)
        {
            Vertice temp;
            foreach(Vertice v in Vertices)
            {
                if(v.Label == label)
                {
                    temp = v;
                    return temp;
                }
            }
            return null;
        }
        #endregion

        #region Strings
        public string ShowVertices()
        {
            return VerticesToString();
        }

        public string ShowArestas()
        {
            return ArestasToString();
        }

        private string ArestasToString()
        {
            string arestas = "";

            foreach (Aresta a in Arestas)
            {
                arestas += (a.ToString() + "\n");
            }
            return arestas;
        }
        private string VerticesToString()
        {
            string vertices = "";
            foreach (Vertice v in Vertices)
            {
                vertices += (v.ToString() + "\n");
            }
            return vertices;
        }

        public override string ToString()
        {
            return "Arestas: " + "\n" + ArestasToString() + "\n" +
                   "Vertices: " + "\n" + VerticesToString() + "\n" +
                   "Numero de Arestas: " + NumArestas().ToString() + "\n" +
                   "Numero de Vertices: " + NumVertices().ToString() + "\n" +
                   "Direcionado: " + isDigraph.ToString();
        }

        public List<Vertice> Adjacencias(Vertice v)
        {
            List<Vertice> adj =  new List<Vertice>();
            if (v == null || !Vertices.Contains(v)) return null;
            foreach(Aresta a in Arestas)
            {
                if(a.Contains(v))
                {
                    //adj.Add()
                }
            }
            return null;
        }

        #endregion



    }
}
