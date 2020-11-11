using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace VisualGraphs.Classes.Drawing
{
    class ShapeAresta
    {
        private Grid MyGrid;
        private bool IsPositioned;
        private double X { get; set; }
        private double Y { get; set; }

        public ShapeAresta(ShapeVertice v1, ShapeVertice v2)
        {
            //definindo o grid
            IsPositioned = false;
            MyGrid = new Grid();
            X = v1.X;
            Y = v1.Y;
            MyGrid.Children.Add(new Line
            {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 4,
                X1 = v1.X,
                X2 = v2.X,
                Y1 = v1.Y,
                Y2 = v2.Y,
            });
        }

        //Seta a posição da aresta no canvas
        public void SetArestaPosition()
        {
            IsPositioned = true;
        }

        //se o Grid ja possui uma posicao definida, retorne-o, senao, retorne nulo
        public Grid GetGridlock()
        {
            if (IsPositioned)
            {
                return MyGrid;
            }
            return null;
        }
    }
}
