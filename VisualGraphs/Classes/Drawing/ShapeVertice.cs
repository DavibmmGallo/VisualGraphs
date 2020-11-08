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

class ShapeVertice
{
    private Grid MyGrid;

    public ShapeVertice(string texto)
    {
        //definindo o grid
        MyGrid = new Grid();
        MyGrid.Children.Add(new Ellipse
        {
            Fill = new SolidColorBrush(Colors.DarkBlue),
            Height = 30,
            Width = 30
        });
        MyGrid.Children.Add(new TextBlock
        {
            Text = texto
        });
    }

    public Grid GetVerticeShape(float left)
    {
        float nleft = left + 450;
        Canvas.SetTop(MyGrid, nleft); //y
        Canvas.SetLeft(MyGrid, ShapeCircunferencia.CalculaY(left) + 200); //x
        return MyGrid;
    }


}

