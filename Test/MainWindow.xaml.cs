using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.Json.Nodes;
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

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
       where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }
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

            s = LB1.SelectedItem;
            KeyValuePair<string, List<Users>> sel = (KeyValuePair<string, List<Users>>)s;

            for (int i = 1; i < sel.Value.Count; i++)
            {
                result.TryAdd(i, sel.Value[i - 1].Steps);
            }

            ((LineSeries)mcChart.Series[0]).ItemsSource = result;
        }

        public void MarkDiff()
        {
            for (int i = 0; i < 99; i++)
            {
                //Начинаем с элемента ListBox. В примере используется текущий элемент,
                //Вы будете обходить элементы в списке
                ListBoxItem myListBoxItem =
                    (ListBoxItem)(LB1.ItemContainerGenerator.ContainerFromItem(LB1.Items.GetItemAt(i)));
                if (myListBoxItem == null)
                    continue;

                // Для элемента получаем ContentPresenter
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Шаблон данных
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                //И уже из него по имени вытаскиваем TextBlock
                TextBlock avgTextBlock = (TextBlock)myDataTemplate.FindName("Avg", myContentPresenter);
                TextBlock maxTextBlock = (TextBlock)myDataTemplate.FindName("Max", myContentPresenter);
                TextBlock minTextBlock = (TextBlock)myDataTemplate.FindName("Min", myContentPresenter);
                if (Convert.ToInt32(avgTextBlock.Text) < Convert.ToInt32(maxTextBlock.Text) * 0.8 || Convert.ToInt32(avgTextBlock.Text) < Convert.ToInt32(minTextBlock.Text) * 1.2)
                {
                    avgTextBlock.Background = Brushes.Yellow;
                }
            }
            
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
            SaveBtn.IsEnabled = true; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MarkDiff();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var obj = LB1.SelectedItem;
            var user = new Users();
            user.GetJSON(obj);

        }
    }
}
