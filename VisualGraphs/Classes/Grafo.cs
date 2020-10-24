using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VisualGraphs.Classes
{
    class Grafo
    {
        public List<Vertice> Vertices { get; set; }
        private AdjList Adj { get; set; }
        public List<Aresta> Arestas { get; set; }
        public bool isDigraph;
        protected bool isAciclic;
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
            Adj = new AdjList(this);
            isAciclic = true;
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
            Adj.Clear_adj(v, this);
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

                if (a.vertice1._id == a.vertice2._id) isAciclic = false; // no longer aciclic
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
        protected void DFS_search(int v, bool[] visited, Action action)//TODO
        {
            visited[v] = true;
            action();

            foreach (var n in Adj.get_Adj()[v])
            {
                if (!visited[n])
                    DFS_search(n, visited, action);
            }
        }
        /// <summary>
        /// Searches all connected vertices from the source by DFS.
        /// </summary>
        /// <param name="source">First Vertice to be search</param>
        public void BuscaEmProfundidade(int source)
        {
            bool[] visited = new bool[Vertices.Count];

            DFS_search(source, visited,()=> { Debug.WriteLine(source + " "); });
        }
        /// <summary>
        /// Searches all connected vertices from the source by BFS.
        /// </summary>
        /// <param name="source">First Vertice to be search</param>
        public string BuscaEmLargura(int source)
        {
            int count = 0;
            string out_put = "";
            int[] num = new int[Vertices.Count];

            for (int n = 0; n < Vertices.Count; n++)
                num[n] = -1;

            Queue<int> fila = new Queue<int>();

            num[source] = count++;

            fila.Enqueue(source);

            while (fila.Count > 0)
            {
                var vertex = fila.Dequeue();
                out_put += BuscaVertice(vertex).Label;

                foreach (var n in Adj.get_Adj()[vertex])
                {
                    if(num[n] == -1)
                    {
                        num[n] = count++;
                        fila.Enqueue(n);
                    }
                }
            }
            fila.Clear();
            return out_put;
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
        /// <summary>
        /// Returns the number of components from Graph
        /// </summary>
        /// <returns></returns>
        public int NumComponents()
        {
            bool[] marked = new bool[Vertices.Count];
            Adj.Update(this);
            int count = 0;

            for (int i = 0; i < Vertices.Count; i++)
                marked[i] = false;
            
            for(int j = 0; j < Vertices.Count; j++)
            {
                if (!marked[j])
                {
                    DFS_search(j, marked,()=> { });
                    count++;
                }
            }
            return count;
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
            return $"Arestas: \n{ArestasToString()}" +
                   $"Vertices: \n{VerticesToString()}\n" +
                   $"Numero de Arestas: {NumArestas()}\n" +
                   $"Numero de Vertices: {NumVertices()}\n" +
                   $"Direcionado: {isDigraph}\t"+
                   $"Aciclico: {isAciclic}";
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
