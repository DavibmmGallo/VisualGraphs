using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private Ellipse Space; // Circulo de representacao
    private double Raio;
    private List<ShapeVertice> vertices;
    
        public DrawManager(Canvas c)
        {
        MyCanva = c;

        vertices = new List<ShapeVertice>();
        //Circunferencia espacial como referencia
        Space = new Ellipse
        {
            Width = 400,
            Height = 400,
            Fill = new SolidColorBrush(Colors.LightCyan)
        };
        //centralizando o circulo de referencia no meio
        Canvas.SetLeft(Space, 475 - Space.Width / 2); // x
        Canvas.SetTop(Space, 225 - Space.Height / 2); // y

        Debug.WriteLine(475 - Space.Width / 2);
        Debug.WriteLine(225 - Space.Height / 2);
        //raio da circunferencia de referencia
        Raio = Space.Width / 2;
        MyCanva.Children.Add(Space);
    }


    #region Metodos Publicos
    //metodo que vai desenhar os shapes dos vertices na tela apos os calculos
    public void Draw()
    {
        /*
         *             posx = 475 + Math.Cos(iterator) * Raio;
                posy = 225 + Math.Sin(iterator) * Raio;
         * */
        double iterator = GetAngle();
        double space = 0;
        double x, y;
        foreach(ShapeVertice v in vertices)
        {
            x = 475 - v.GetSize()/1.8 + Math.Cos((space * Math.PI) / 180) * Raio;
            y = 225 - v.GetSize()/1.8 + Math.Sin((space * Math.PI) / 180) * Raio;
            v.SetVerticePosition(x, y);
            MyCanva.Children.Add(v.GetGridlock());
            space += iterator;
        }

    }


    //Metodo que adiciona um Shape a um determinado vertice
    public void AddVerticeToShape(Vertice v)
    {
        vertices.Add(new ShapeVertice(v.Label));
    }



    //metodo que Limpa o Canvas
    public void ClearCanvas()
    {
        MyCanva.Children.Clear(); //limpa o canvas
        vertices.Clear(); // limpa a lista de shapes
    }

    #endregion

    #region Metodos Privados

    //metodo para encontrar os angulo do espacamento MAX 180 MIN 1
    private double GetAngle()
    {
        return 360 / vertices.Count;
    }




    #endregion
}

