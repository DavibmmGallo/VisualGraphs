using System;
using System.Collections.Generic;
using System.Text;

namespace VisualGraphs.Classes
{
    class Aresta
    {
        public Vertice vertice1 { get; set; }
        public Vertice vertice2 { get; set; }
        public bool isDirected;
        public float Peso { get; set; }

        public Aresta(float p, Vertice v1, Vertice v2)
        {
            Peso = p;
            vertice1 = v1;
            vertice2 = v2;
            isDirected = false;
        }
        public Aresta(float p, Vertice v1, Vertice v2, bool b)
        {
            Peso = p;
            vertice1 = v1;
            vertice2 = v2;
            isDirected = b;
        }

        public bool Contains(Vertice v)
        {
            return (vertice1.Equals(v) || vertice2.Equals(v));
        }

        public override string ToString()
        {
            if(isDirected)
            {
                return "Peso: " + Peso.ToString() + "\n" +
                        vertice1.ToString() + " Conectado ao " + vertice2.ToString() + "\n" +
                        "Direcionado: " + isDirected.ToString() + "\n"; 
            }
            return "Peso: " + Peso.ToString() + "\n" +
                    vertice1.ToString() + " Conectado ao " + vertice2.ToString() + "\n";
        }
    }
}
