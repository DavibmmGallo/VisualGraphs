using System;
using System.Collections.Generic;
using System.Text;

namespace GrafosImplementados.Classes
{
    class Grafo
    {
        public List<Vertice> Vertices { get; set; }
        public List<Aresta> Arestas { get; set; }
        public bool isDigraph;
        public Grafo(bool b)
        {
            isDigraph = b;
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();
        }

        public void AddVertice(Vertice v)
        {
            Vertices.Add(v);
        }
        public bool AddAresta(Aresta a)
        {
            if(a.isDirected == isDigraph)
            {
                Vertices.Add(a.vertice1);
                Vertices.Add(a.vertice2);
                Arestas.Add(a);
            }
            return false;
        }

        public int NumVertices()
        {
            return Vertices.Count;
        }

        public int NumArestas()
        {
            return Arestas.Count;
        }

        #region Buscas
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

        public void BuscaEmProfundidade()
        {
            //TODO
        }

        public void BuscaEmLargura()
        {
            //TODO
        }

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
