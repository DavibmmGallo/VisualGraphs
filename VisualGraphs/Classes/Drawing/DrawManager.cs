using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualGraphs.Classes;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

class DrawManager
{
    private Canvas MyCanva;
    private Grafo MyGraph;
    
        public DrawManager(Canvas c)
        { 
            MyCanva = c;
        }

    public void SetGraph(Grafo g)
    {
        MyGraph = g;
    }

    public void DrawEllipse(Vertice v, float left, float top)
    {
        ShapeVertice sh = new ShapeVertice(v.Label);
        MyCanva.Children.Add(sh.GetVerticeShape(left,top));
    }
}

