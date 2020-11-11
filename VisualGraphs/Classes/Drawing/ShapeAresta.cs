using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

class ShapeAresta
{
    private ShapeVertice Point1;
    private ShapeVertice Point2;
    private Line Body;

    public ShapeAresta(ShapeVertice p1, ShapeVertice p2)
    {
        Point1 = p1;
        Point2 = p2;
    }


    public Line GetArestaBody()
    {
        return Body = new Line()
        {
            X1 = Point1.posX + 10,
            Y1 = Point1.posY + 10,
            X2 = Point2.posX + 10,
            Y2 = Point2.posY + 10,
            Stroke = new SolidColorBrush(Colors.DarkRed),
            StrokeThickness = 2
        };
    }



}
