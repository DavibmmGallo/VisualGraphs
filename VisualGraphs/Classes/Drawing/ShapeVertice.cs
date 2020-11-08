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
    private double Size;
    private bool IsPositioned;

    public ShapeVertice(string texto)
    {
        //definindo o grid
        IsPositioned = false;
        MyGrid = new Grid();
        MyGrid.Children.Add(new Ellipse
        {
            Fill = new SolidColorBrush(Colors.DarkBlue),
            Height = 30,
            Width = 30
        });
        Size = 30;
        MyGrid.Children.Add(new TextBlock
        {
            Text = texto,
            FontSize = 10,
            Name = "A"
        }) ;
    }

    public double GetSize()
    {
        return Size;
    }

    //retorna o Grid com o shape e o nome em uma grid, na posicao especificada
    public void SetVerticePosition(double x, double y)
    {
        Canvas.SetLeft(MyGrid, x); //x
        Canvas.SetTop(MyGrid, y); //y
        IsPositioned = true;
    }

    //se o Grid ja possui uma posicao definida, retorne-o, senao, retorne nulo
    public Grid GetGridlock()
    {
        if(IsPositioned)
        {
            return MyGrid;
        }
        return null;
    }


}

