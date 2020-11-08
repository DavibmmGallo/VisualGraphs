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
    private ShapeCircunferencia MyCircle;
    private Grafo MyGraph;
    
        public DrawManager(Canvas c)
        { 
            MyCanva = c;
        MyCircle = new ShapeCircunferencia();
        //Canvas.SetLeft(MyCircle.GetSpacialArea(), 400);
        //Canvas.SetTop(MyCircle.GetSpacialArea(), 400);
        Canvas.SetLeft(MyCanva, 400);
        Canvas.SetTop(MyCanva, 400);
        MyCanva.Children.Add(MyCircle.GetSpacialArea());
        }

    public void SetGraph(Grafo g)
    {
        MyGraph = g;
    }

    public void DrawEllipse(Vertice v, float left)
    {
        ShapeVertice sh = new ShapeVertice(v.Label);
        MyCanva.Children.Add(sh.GetVerticeShape(left));
    }


    public void ClearCanvas()
    {
        MyCanva.Children.Clear();
        Canvas.SetLeft(MyCanva, 400);
        Canvas.SetTop(MyCircle.GetSpacialArea(), 400);
        MyCircle = new ShapeCircunferencia();
        MyCanva.Children.Add(MyCircle.GetSpacialArea());
    }
}

