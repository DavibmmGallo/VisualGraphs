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

        /// <summary>
        /// Is Bounded (digraph)
        /// </summary>
        /// <param name="vx"></param>
        /// <param name="vy"></param>
        /// <returns></returns>
        bool is_bounded(Vertice vx, Vertice vy)
        {
            foreach ( var aresta in grafo.Arestas)
            {
                if (grafo.isDigraph)
                {
                    if (aresta.vertice1._id == vx._id && aresta.vertice2._id == vy._id)
                        return true;
                }
                else { 
                    if ((aresta.vertice1._id == vx._id && aresta.vertice2._id == vy._id) 
                    || (aresta.vertice1._id == vy._id && aresta.vertice2._id == vx._id))
                        return true;
                }
               
            }
            return false;
        }
        /// <summary>
        /// Returns Matrix
        /// </summary>
        /// <returns></returns>
        public float[,] get_matrix()
        {
            float[,] res = new float[grafo.NumVertices(), grafo.NumVertices()];
            for (int i = 0; i < grafo.NumVertices(); i++)
            {
                for(int j=0; j < grafo.NumVertices(); j++)
                {
                    if (is_bounded(grafo.BuscaVertice(i), grafo.BuscaVertice(j))) 
                    {
                        res[i, j] = grafo.BuscaAresta($"{i} {j}").Peso;
                    }
                    else
                        res[i, j] = 0;
                }              
              
            }
            return res;
        }
        /// <summary>
        /// toString()
        /// </summary>
        /// <returns></returns>
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
                        output += " ";
                        if (is_bounded(grafo.BuscaVertice(i - 1), grafo.BuscaVertice(p))) 
                            output += "1";
                        else
                            output += "0";
                        p++;
                    }
                    g++;
                    p = 0;
                    output += "\n";
                }
            }
            return output;
        }

    }
}
