using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

class ShapeCircunferencia
{
    private Ellipse Space;
    private float left;
    private float top;
    private float raio;
    private List<ShapeVertice> vertices;


    public ShapeCircunferencia()
    {
        //Circunferencia espacial
        Space = new Ellipse
        {
            Width = 400,
            Height = 400,
            Fill = new SolidColorBrush(Colors.LightCyan)
        };
    }
    
    public Ellipse GetSpacialArea()
    {
        return Space;
    }

    public static double CalculaY(double x)
    {
        //x += 450;
        double resultado = Math.Sqrt((x*x) - 900);
        Debug.WriteLine(resultado);
        return resultado;
    }



}
