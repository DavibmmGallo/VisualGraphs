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
        private MatrizAdj MatrizAdj { get; set; }
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
            Adj.Update(this);
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
            Adj.Update(this);
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
                Adj.Update(this);
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
                Adj.remove_adj(a);
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
        /// Searches the vertice by conj.
        /// </summary>
        /// <param name="conj"></param>
        /// <returns></returns>
        public Aresta BuscaAresta(string conj)
        {
            Aresta temp;

            string[] str_aux = conj.Split(" ");

            var v1id = int.Parse(str_aux[0]);
            var v2id = int.Parse(str_aux[1]);

            foreach (var a in Arestas)
            {
                if ((a.vertice1._id == v1id && a.vertice2._id == v2id)||
                    (a.vertice1._id == v2id && a.vertice2._id == v1id))
                {
                    temp = a;
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
        /// <summary>
        /// returns a string that shows the minimum path between 2 vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="prnt"></param>
        /// <returns></returns>
        public string str_path(int source, int[] prnt)
        {
            string res = "";

            if (prnt[source] == -1)
                return BuscaVertice(source).Label;

            res += str_path(prnt[source],prnt);

            res += " -> " +BuscaVertice(source).Label;

            return res;
        }
        /// <summary>
        /// Returns the id of which vertex is closer
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="vst"></param>
        /// <returns></returns>
        private int min_dist(int[] dist, bool[] vst)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < NumVertices(); v++)
                if (vst[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }
        /// <summary>
        /// Dijkstra algorithim
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public string Dijkstra(int source, int dest)
        {
            MatrizAdj matrix = new MatrizAdj(this);
            string out_put = "";
            int[] parent = new int[NumVertices()];
            int[] dist = new int[NumVertices()];
            bool[] visitado = new bool[NumVertices()];

            for (int i = 0; i < NumVertices(); i++)
            {
                parent[i] = -1;
                dist[i] = int.MaxValue;
                visitado[i] = false;
            }
            dist[source] = 0;

            for (int count = 0; count < NumVertices() - 1; count++)
            {
                int u = min_dist(dist, visitado);

                visitado[u] = true;

                for (int v = 0; v < NumVertices(); v++)
                    if (!visitado[v] && matrix.get_matrix()[u, v] != 0
                        && dist[u] != int.MaxValue
                        && dist[u] + matrix.get_matrix()[u, v] < dist[v]) 
                    {
                        dist[v] = dist[u] + (int)matrix.get_matrix()[u,v];
                        parent[v] = u;
                    }
                        
            }
            string str = str_path(dest, parent);
            out_put += $"Caminho mínimo do {BuscaVertice(source)} até {BuscaVertice(dest)} : {dist[dest]}";
            out_put += "\nCaminho: " + str;

            return out_put;
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
        /// <summary>
        /// AdjList to string
        /// </summary>
        /// <returns></returns>
        public string AdjListToString()
        {
            return Adj.ToString();
        }
        /// <summary>
        /// AdjMatrix to string
        /// </summary>
        /// <returns></returns>
        public string MatrixListToString()
        {
            MatrizAdj = new MatrizAdj(this);
            return MatrizAdj.ToString();
        }
        /// <summary>
        /// Graph to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Arestas: \n{ArestasToString()}" +
                   $"Vertices: \n{VerticesToString()}\n" +
                   $"Numero de Arestas: {NumArestas()}\n" +
                   $"Numero de Vertices: {NumVertices()}\n" +
                   $"Direcionado: {isDigraph}\n" +
                   $"Densidade: {Densidade()}\n"+
                   $"Grau Médio: {GrauMedio()}";
                   
        }
        /// <summary>
        /// Density of a graph
        /// </summary>
        /// <returns></returns>
        public double Densidade()
        {
            int n = NumVertices();
            int m = NumArestas();

            return 2*(m)/(n*(n-1));
        }
        /// <summary>
        /// GrauMédio 
        /// </summary>
        /// <returns></returns>
        public double GrauMedio()
        {
            int n = NumVertices();
            int sum = 0;
            for(int i = 0; i < n; i++)
            {
                sum += Adj.get_Adj()[i].Count;
            }
            return sum / n;
        }
        #endregion

    }
}
