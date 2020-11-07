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
    private Ellipse shape;
    private TextBlock Label;
    private Grid MyGrid;

    public ShapeVertice(string texto)
    {
        //setando o texto
        Label = new TextBlock();
        Label.Text = texto;

        //definindo a forma;
        shape = new Ellipse();
        shape.Fill =  new SolidColorBrush(Colors.DarkBlue);
        shape.Height = 30;
        shape.Width = 30;

        //definindo o grid
        MyGrid = new Grid();
        MyGrid.Children.Add(shape);
        MyGrid.Children.Add(Label);
    }

    public Grid GetVerticeShape(float left, float top)
    {
        Canvas.SetLeft(MyGrid, left);
        Canvas.SetTop(MyGrid, top);
        return MyGrid;
    }


}

