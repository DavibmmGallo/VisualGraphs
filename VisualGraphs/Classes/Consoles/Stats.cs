using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VisualGraphs.Classes
{
    class Stats :Console
    {
        private Grafo Grafo;

        public Stats(TextBox t): base(t){}

        public override void Clear()
        {
            Box.Text = "Nao ha Rede";
        }

        public void SetGrafo(Grafo g)
        {
            Grafo = g;
            int NComponents = g.NumVertices() + g.NumArestas();
            Box.Text = " Nome: " + Grafo.name + "\n" +
                " Componentes: " + NComponents + "\n" + 
                " Numero de Arestas: " + Grafo.NumArestas() + "\n" +
                " Numero de Vertices: " + Grafo.NumVertices();
        }

        public override void Update()
        {
            int NComponents;
            if (Grafo != null)
            {
                NComponents = Grafo.NumVertices() + Grafo.NumArestas();
                Box.Text = " Nome: " + Grafo.name + "\n" +
                           " Componentes: " + NComponents + "\n" +
                           " Numero de Arestas: " + Grafo.NumArestas() + "\n" +
                           " Numero de Vertices: " + Grafo.NumVertices();
            }
            else
            {
                Box.Text = "Nao ha Rede";
            }

        }
    }
}
