using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test.Model;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void ShowGraph()
        {
            var result = new Dictionary<int, int>();

            var s = LB1.Items[0];
            KeyValuePair<string, List<Users>> sel = (KeyValuePair<string, List<Users>>)s;

            for (int i = 1; i < 31; i++)
            {
                result.Add(i, sel.Value[i - 1].Steps);
            }

            ((LineSeries)mcChart.Series[0]).ItemsSource = result;
        }

        public void ShowGraph(object s)
        {
            var result = new Dictionary<int, int>();

            //s = LB1.SelectedItem;
            KeyValuePair<string, List<Users>> sel = (KeyValuePair<string, List<Users>>)s;

            for (int i = 1; i < sel.Value.Count; i++)
            {
                result.TryAdd(i, sel.Value[i - 1].Steps);
            }

            ((LineSeries)mcChart.Series[0]).ItemsSource = result;
        }



        public MainWindow()
        {
            var d = Users.GetUsers();
            Resources["Users"] = d;

            InitializeComponent();
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var s = LB1.SelectedItem;
            MessageBox.Show(s.ToString());
        }

        private void LineSeries_Loaded(object sender, RoutedEventArgs e)
        {
            ShowGraph();
        }

        private void LB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowGraph(LB1.SelectedItem);
        }
    }
}
