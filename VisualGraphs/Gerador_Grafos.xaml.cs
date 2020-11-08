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
using System.Numerics;
using Windows.UI.Input;
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
        private bool Graph_exist = false; 
        private TextConsole myConsole;
        private MessageDialog msgdi;
        private Stats graphStats;
        private LogManager log;
        private Vector2 CurrentPosition { get; set; }
        private Vector2 CurrentTransform { get; set; }
        private bool isCaptureOn = false;
        private Dictionary<string, Vector2> currentGrid = new Dictionary<string, Vector2>();

        public Gerador_Grafos()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();
            Add_scene.RenderTransform = new TranslateTransform();
            Remove_scene.RenderTransform = new TranslateTransform();
            Utils_scene.RenderTransform = new TranslateTransform();
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
            v1_box.Visibility = Visibility.Collapsed;
            v1_box.SelectedItem = "";
            v2_box.Visibility = Visibility.Collapsed;
            v2_box.SelectedItem = "";
            weigth_Aresta_box.Visibility = Visibility.Collapsed;
            weigth_Aresta_box.Text = "";
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
        }
        /// <summary>
        /// Controls ui at Aresta add
        /// </summary>
        void Aresta_add_Control()
        {
            weigth_Aresta_box.Visibility = Visibility.Visible;
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
                    myConsole.Update();
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
                else if (selected_item_name == "Vértice")
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
            
            myConsole.Update();
            graphStats.Update();
        }

        #endregion

        #region Utils
        /// <summary>
        /// confirms util options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void confirm_util(object sender, RoutedEventArgs e)// TODO
        {
            try
            {
                switch (util_cmbx.SelectedItem.ToString())
                {
                    case "Caminho mínimo":
                        msgdi = new MessageDialog(Graph.Dijkstra(Graph.BuscaVertice(from_utils_dkstra.Text)._id,Graph.BuscaVertice(to_utils_dkstra.Text)._id));
                        await msgdi.ShowAsync(); 
                        break;
                    case "Ler arquivo":
                        break;
                    case "BFS":
                        msgdi = new MessageDialog(Graph.BuscaEmLargura(0));
                        await msgdi.ShowAsync();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}");
                await msgdi.ShowAsync();
            }
            
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
        /// <summary>
        /// Closes scenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void back_main(object sender, RoutedEventArgs e)
        {
            try
            {
                var snd = sender as Button;
                switch (snd.Name)
                {
                    case "back_add_btn":
                        clear_ui_add();
                        Add_scene.Visibility = Visibility.Collapsed;
                        ComboAdd_box.SelectedItem = "";
                        graphStats.Clear();
                        break;
                    case "back_Rem_btn":
                        Remove_scene.Visibility = Visibility.Collapsed;
                        ComboRem_box.SelectedItem = "";
                        v1_rem_box.Visibility = Visibility.Collapsed;
                        v2_rem_box.Visibility = Visibility.Collapsed;
                        break;
                    case "back_util_btn":
                        Utils_scene.Visibility = Visibility.Collapsed;
                        to_utils_dkstra.Visibility = Visibility.Collapsed;
                        from_utils_dkstra.Visibility = Visibility.Collapsed;
                        break;

                }
            }catch(Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}");
                await msgdi.ShowAsync();
            }
        }
        /// <summary>
        /// Moves grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void move_element(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var grid = sender as Grid;
                Vector2 diff = e.GetCurrentPoint(Parent as UIElement).Position.ToVector2() - CurrentPosition;
                if (isCaptureOn)
                {
                    (grid.RenderTransform as TranslateTransform).X = currentGrid[grid.Name].X + diff.X;
                    (grid.RenderTransform as TranslateTransform).Y = currentGrid[grid.Name].Y + diff.Y;
                }
            }catch(Exception ex)
            {
                msgdi = new MessageDialog($"Erro {ex.Message}");
                await msgdi.ShowAsync();
            }
        }
        /// <summary>
        /// Grid Mouse button down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_left_button_down(object sender, PointerRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if(!currentGrid.ContainsKey(grid.Name))
                currentGrid.Add(grid.Name, CurrentTransform);

            CurrentPosition = e.GetCurrentPoint((UIElement)Parent).Position.ToVector2();
            isCaptureOn = true;
            grid.CapturePointer(e.Pointer);
        }
        /// <summary>
        /// Grid Mouse button up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_left_button_up(object sender, PointerRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (isCaptureOn)
            {
                Vector2 diff = e.GetCurrentPoint(Parent as UIElement).Position.ToVector2() - CurrentPosition;
                currentGrid[grid.Name] = currentGrid[grid.Name] + diff;
                grid.ReleasePointerCapture(e.Pointer);
                isCaptureOn = false;
            }       
        }
        /// <summary>
        /// changes ui from combo box util
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void util_cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(util_cmbx.SelectedItem.ToString() == "Caminho mínimo")
            {
                to_utils_dkstra.Visibility = Visibility.Visible;
                from_utils_dkstra.Visibility = Visibility.Visible;
            }
            else
            {
                to_utils_dkstra.Visibility = Visibility.Collapsed;
                from_utils_dkstra.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// shows utils scene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_util_scene(object sender, RoutedEventArgs e)
        {
            Utils_scene.Visibility = Visibility.Visible;
        }
        #endregion

       
    }
}
