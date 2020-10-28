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
            Box.Text = "Calcular";
        }

        public void SetGrafo(Grafo g)
        {
            Grafo = g;
            Update();
        }
        //TODO analise de desempenho 
        public override void Update()
        {
            if (Grafo != null)
            {
                int NComponents = Grafo.NumComponents();
                Box.Text = " Nome: " + Grafo.name + "\n" +
                           " Componentes: " + NComponents + "\n" +
                           " Numero de Arestas: " + Grafo.NumArestas() + "\n" +
                           " Numero de Vertices: " + Grafo.NumVertices();
            }
            else
            {
                Box.Text = "Calculos nao Efetuados";
            }

        }
    }
}
