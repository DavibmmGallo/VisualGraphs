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
using System.Threading.Tasks;
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
        private LogManager log;

        public Gerador_Grafos()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();

            view.TryEnterFullScreenMode();

            myConsole = new TextConsole(Console_output);
            graphStats = new Stats(Grafo_stats);
            graphStats.Clear();
            log = new LogManager();
        }

        #region ADD
        /// <summary>
        /// Clears all controls from add.
        /// </summary>
        void clear_ui_add()
        {
            isDigraph.IsOn = false;
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
            Add_Graph.Visibility = Visibility.Visible;
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

        private void Create_Graph(object sender, RoutedEventArgs e)
        {
            GraphSet_add_Control();           
        }
        private async void Graph_Confirm_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Graph_exist)
                {
                    msgdi = new MessageDialog("Não pode existir mais de um Grafo ainda!");
                    await msgdi.ShowAsync();
                    return;
                }
                else
                {
                    Graph = new Grafo(isDigraph.IsOn);
                    Graph.name = label_box_graph.Text;
                    Graph_exist = true; 
                    GraphCreate.Visibility = Visibility.Collapsed;
                    myConsole.AddStringToConsole($"\nGrafo {label_box_graph.Text} foi adicionado.");
                    myConsole.Update();
                    Add_Graph.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                msgdi = new MessageDialog($"Erro: {ex.Message}");
                await msgdi.ShowAsync();
            }
            label_box_graph.Text = "";
        }
        /// <summary>
        /// Checks Add-combobox and confirms the operation
        /// </summary>
        /// <param name="sender">Button Confirm</param>
        /// <param name="e">Routed Event</param>
        private async void Confirm_add_item(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Graph_exist)
                {
                    switch (selected_item_name)
                    {
                        case "Vértice":
                            {
                                Vertice vertice_aux = new Vertice(label_box.Text, Graph.NumVertices());

                                Graph.AddVertice(vertice_aux);
                                v1_box.Items.Add(vertice_aux.Label);
                                v2_box.Items.Add(vertice_aux.Label);
                                v1_rem_box.Items.Add(vertice_aux.Label);
                                v2_rem_box.Items.Add(vertice_aux.Label);
                                break;
                            }

                        case "Aresta":
                            {
                                if(Graph.Vertices.Count > 0)
                                {
                                    Aresta aresta_aux = new Aresta(float.Parse(weigth_Aresta_box.Text), Graph.BuscaVertice(v1_box.SelectedItem.ToString()), Graph.BuscaVertice(v2_box.SelectedItem.ToString()), Graph.isDigraph);
                                    Graph.AddAresta(aresta_aux);
                                }
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
            graphStats.Clear();
        }
        #endregion

        #region REMOVE
        /// <summary>
        /// Selects the item that will be removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboRem_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            selected_item_name = cmbx.SelectedItem.ToString();
            if (selected_item_name == "Grafo")
            {
                v1_rem_box.Visibility = Visibility.Collapsed;
                v2_rem_box.Visibility = Visibility.Collapsed;
                lbl_rem_box.Visibility = Visibility.Collapsed;
            }                
            else if (selected_item_name == "Aresta") 
            {
                v1_rem_box.Visibility = Visibility.Visible;
                v2_rem_box.Visibility = Visibility.Visible;
                lbl_rem_box.Visibility = Visibility.Collapsed;
            }
            else if(selected_item_name == "Vértice")
            {
                v1_rem_box.Visibility = Visibility.Collapsed;
                v2_rem_box.Visibility = Visibility.Collapsed;
                lbl_rem_box.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Checks Rem-combobox and confirms the operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Confirm_rem_item(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected_item_name == "Grafo")
                {
                    Graph = null; 
                    Graph_exist = false;
                    graphStats.SetGrafo(Graph);
                    GraphCreate.Visibility = Visibility.Visible;
                }
                else if (selected_item_name == "Vértice") //TODO
                {
                    Vertice vertice_aux = Graph.BuscaVertice(lbl_rem_box.Text);

                    Graph.RemoveVertice(vertice_aux);

                    v1_box.Items.Remove(vertice_aux.Label);
                    v2_box.Items.Remove(vertice_aux.Label);
                }
                else if (selected_item_name == "Aresta")
                {
                    Aresta aresta_aux = Graph.BuscaAresta($"{Graph.BuscaVertice(v1_rem_box.SelectedItem.ToString())._id} {Graph.BuscaVertice(v2_rem_box.SelectedItem.ToString())._id}");
                    Graph.RemoveAresta(aresta_aux);
                }
            }
            catch (Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}");
                await msgdi.ShowAsync();
            }

            if (selected_item_name != "") myConsole.AddStringToConsole($"\n{selected_item_name} foi removido.");
            Remove_scene.Visibility = Visibility.Collapsed;
            ComboRem_box.SelectedItem = "";
            v1_rem_box.Visibility = Visibility.Collapsed;
            v2_rem_box.Visibility = Visibility.Collapsed;
            myConsole.Update();
            graphStats.Update();
        }

        #endregion

        #region Utils
        /// <summary>
        /// BFS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)// TODO
        {
            try
            {
                msgdi = new MessageDialog(Graph.BuscaEmLargura(0));
                /*
                MatrizAdj matriz = new MatrizAdj(Graph);
                await log.SaveAsync(matriz.ToString());*/
            }catch(Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}");
            }
            await msgdi.ShowAsync();
        }
        /// <summary>
        /// Calculate Graph functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calcular_on_click(object sender, RoutedEventArgs e)
        {
            if(Graph != null)
            {
                graphStats.SetGrafo(Graph);
                Debug.WriteLine("Calculos efetuados");
                myConsole.AddStringToConsole("Calculos efetuados");
            }
            else
            {
                Debug.WriteLine("Erro ao calcular");
                myConsole.AddStringToConsole("Erro ao calcular componentes");
            }
            myConsole.Update();
        }
        /// <summary>
        /// show saves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_save_log(object sender, RoutedEventArgs e)
        {
            Save_Sttgs.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Shows Add and Rem sections
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void Show_sections(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (btn.Name == "Add_btn")
                Add_scene.Visibility = Visibility.Visible;
            else
                Remove_scene.Visibility = Visibility.Visible;
        }
        private async void Save_log_event(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var str_btn = btn.Name as string;

            try
            {
                switch (str_btn)
                {
                    case "Save_Graph":
                        await log.SaveAsync(Graph.ToString());
                        break;
                    case "Save_adjList":
                        await log.SaveAsync(Graph.AdjListToString());
                        break;
                    case "Save_adjMatrix":
                        await log.SaveAsync(Graph.MatrixListToString());
                        break;
                    case "Save_Console":
                        await log.SaveAsync(myConsole.ToString());
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}\nGrafo = {Graph}");
                await msgdi.ShowAsync();
            }
            Save_Sttgs.Visibility = Visibility.Collapsed;
        }

        #endregion

    }
}
