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
    private Vertice MyVertice;
    private double Size;
    private bool IsPositioned;
    public double posX { get; set; }
    public double posY { get; set; }

    public ShapeVertice(Vertice v)
    {
        //definindo o grid
        IsPositioned = false;
        MyGrid = new Grid();
        MyVertice = v;
        MyGrid.Children.Add(new Ellipse
        {
            Fill = new SolidColorBrush(Colors.DarkBlue),
            Height = 30,
            Width = 30
        });
        Size = 30;
        MyGrid.Children.Add(new TextBlock
        {
            Text = v.Label,
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
        posX = x;
        posY = y;
        Canvas.SetLeft(MyGrid, x); //x
        Canvas.SetTop(MyGrid, y); //y
        IsPositioned = true;
    }

    //metodo para saber se o vertice no parametro e igual ao valor interno

    public bool CheckShapedVertice(Vertice v)
    {
        return MyVertice.Equals(v);
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

