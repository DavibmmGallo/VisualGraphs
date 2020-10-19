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
using Windows.UI.Popups;
using System.Diagnostics;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VisualGraphs
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Gerador_Grafos : Page
    {
        private string selected_item_name="";
        private Grafo Graph;
        private bool Graph_exist = false; //temporary!!!
        private TextConsole myConsole;
        private MessageDialog msgdi;
        private Stats graphStats;

        public Gerador_Grafos()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();

            //view.TryEnterFullScreenMode();

            myConsole = new TextConsole(Console_output);
            graphStats = new Stats(Grafo_stats);
            graphStats.Clear();
        }

        private void Show_sections(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (btn.Name == "Add_btn")
                Add_scene.Visibility = Visibility.Visible;
            else
                Remove_scene.Visibility = Visibility.Visible;
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
            weigth_Aresta_box.Visibility = Visibility.Visible;
            weigth_label.Visibility = Visibility.Visible;
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

        private async void Confirm_add_item(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected_item_name == "Grafo")
                {
                    if(Graph_exist)
                    {
                        msgdi = new MessageDialog("Não pode existir mais de um Grafo ainda!");
                        await msgdi.ShowAsync();
                        return;
                    }
                    else
                    {
                        Graph = new Grafo(isDigraph.IsChecked.Value);
                        Graph.name = label_box.Text;
                        Graph_exist = true;
                        graphStats.SetGrafo(Graph);
                    }
                }
                if (Graph_exist)
                {
                    switch (selected_item_name)
                    {
                        case "Vértice":
                            {
                                Vertice vertice_aux = new Vertice(label_box.Text, Graph.NumVertices() + 1);

                                Graph.AddVertice(vertice_aux);
                                Debug.WriteLine(Graph.NumVertices());
                                Debug.WriteLine(Graph.NumArestas());
                                v1_box.Items.Add(vertice_aux.Label);
                                v2_box.Items.Add(vertice_aux.Label);
                                break;
                            }

                        case "Aresta":
                            {
                                Aresta aresta_aux = new Aresta(float.Parse(weigth_Aresta_box.Text), Graph.BuscaVertice(v1_box.SelectedItem.ToString()), Graph.BuscaVertice(v2_box.SelectedItem.ToString()), Graph.isDigraph);
                                Graph.AddAresta(aresta_aux);
                                break;
                            }
                    }
                    if (selected_item_name != "") myConsole.AddStringToConsole($"\n{selected_item_name} {label_box.Text} foi adicionado.");
                }
                else
                {
                    msgdi = new MessageDialog("Não existe Grafo ainda!");
                    await msgdi.ShowAsync();
                }
            }catch(Exception ex) {
                msgdi = new MessageDialog($"Erro {ex.Message}");
                await msgdi.ShowAsync();
            }

            
            clear_ui_add();
            Add_scene.Visibility = Visibility.Collapsed;
            ComboAdd_box.SelectedItem = "";
            myConsole.Update();
            graphStats.Update();
        }
        #endregion

        #region REMOVE
        private async void Confirm_rem_item(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected_item_name == "Grafo") // TODO
                {
                    Graph = null; 
                    Graph_exist = false;
                }
                else if (selected_item_name == "Vértice") //TODO
                {
                    Vertice vertice_aux = Graph.BuscaVertice(lbl_rem_box.Text);

                    //busca todas as arestas relacionadas e apaga

                    Graph.RemoveVertice(vertice_aux);

                    v1_box.Items.Remove(vertice_aux.Label);
                    v2_box.Items.Remove(vertice_aux.Label);
                }
                else if (selected_item_name == "Aresta") // TODO
                {
                    //Aresta aresta_aux = Graph.BuscaAresta();
                    //Graph.RemoveAresta(aresta_aux);
                }
            }
            catch (Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}");
                await msgdi.ShowAsync();
            }

            
            myConsole.AddStringToConsole(selected_item_name + " " + label_box.Text + " foi adicionado.");
            clear_ui_add();
            Add_scene.Visibility = Visibility.Collapsed;
            ComboAdd_box.SelectedItem = "";
            myConsole.Update();
            graphStats.Update();

            if (selected_item_name != "") myConsole.AddStringToConsole($"\n{selected_item_name} foi removido.");
            Remove_scene.Visibility = Visibility.Collapsed;
            ComboRem_box.SelectedItem = "";
            myConsole.Update();
           // myConsole.Clear();
        }
        private void ComboRem_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            selected_item_name = cmbx.SelectedItem.ToString();
        }
        #endregion

       
    }
}
