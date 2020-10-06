using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VisualGraphs.Classes;
using System.Collections.ObjectModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VisualGraphs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Gerador_Grafos : Page
    {
        public Gerador_Grafos()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();
            view.TryEnterFullScreenMode();
        }

        private void Change_selection(object sender, SelectionChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            switch (cmbx.Items.ToString())
            {
                case "Grafo":
                    
                    break;
                case "Vértice":

                    break;
                case "Aresta":

                    break;
                default:
                    break;
            }
            
        }
    }
}
