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
        string selected_item_name="";
        private Grafo Graph;
        private TextConsole myConsole;
        public Gerador_Grafos()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();
            myConsole = new TextConsole(Console_output);
            //view.TryEnterFullScreenMode();
        }

        async void main_thread()
        {
           
        }

        #region ADD
        /// <summary>
        /// Clears all controls from add.
        /// </summary>
        void clear_ui_add()
        {
            isDigraph.Visibility = Visibility.Collapsed;
            isDigraph.IsChecked = false;
            label_box.Visibility = Visibility.Collapsed;
            label_box.Text = "";
            lbl_label.Visibility = Visibility.Collapsed;
            v1_box.Visibility = Visibility.Collapsed;
            v1_box.SelectedItem = "";
            v2_box.Visibility = Visibility.Collapsed;
            v2_box.SelectedItem = "";
            weigth_Aresta_box.Visibility = Visibility.Collapsed;
            weigth_Aresta_box.Text = "";
            weigth_label.Visibility = Visibility.Collapsed;
            Aresta_seta.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// Controls ui at graph add
        /// </summary>
        void GraphSet_add_Control()
        {
            isDigraph.Visibility = Visibility.Visible;
            label_box.Visibility = Visibility.Visible;
            lbl_label.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Controls ui at vertice add
        /// </summary>
        void Vertice_add_Control()
        {
            label_box.Visibility = Visibility.Visible;
            lbl_label.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Controls ui at Aresta add
        /// </summary>
        void Aresta_add_Control()
        {
            label_box.Visibility = Visibility.Visible;
            lbl_label.Visibility = Visibility.Visible;
            v1_box.Visibility = Visibility.Visible;
            v2_box.Visibility = Visibility.Visible;
            Aresta_seta.Visibility = Visibility.Visible;
        }

        private void Change_selection(object sender, SelectionChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            selected_item_name = cmbx.SelectedItem.ToString();
            clear_ui_add();

            if (selected_item_name == "Grafo")
                GraphSet_add_Control();
            else if (selected_item_name == "Vértice")
                Vertice_add_Control();
            else if (selected_item_name == "Aresta")
                Aresta_add_Control();
        }

        private void Show_add_section(object sender, RoutedEventArgs e)
        {
            Add_scene.Visibility = Visibility.Visible;
        }

        private void Confirm_add_item(object sender, RoutedEventArgs e)
        {
            //Create and confirm
            if (selected_item_name == "Grafo")
            {
                Grafo grafo_aux = new Grafo(isDigraph.IsChecked.Value);
                grafo_aux.name = label_box.Text;
            }
            else if (selected_item_name == "Vértice")
            {
                Vertice vertice_aux = new Vertice(label_box.Text,1);
            }
            else if (selected_item_name == "Aresta")
            {
                Aresta aresta_aux = new Aresta(float.Parse(weigth_Aresta_box.Text), new Vertice(v1_box.Text,1), new Vertice(v2_box.Text,2));
            }

            //Console_output.Text += "\n" + selected_item_name +" "+ label_box.Text +" foi adicionado.";
            myConsole.AddStringToConsole("\n" + selected_item_name + " " + label_box.Text + " foi adicionado.");
            clear_ui_add();
            Add_scene.Visibility = Visibility.Collapsed;
            ComboAdd_box.SelectedItem = "";
            myConsole.UpdateConsole();
        }
        #endregion 
    }
}
