using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;



namespace Projekt_Grafy_i_sieci.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            CreateTextBoxMatrix();

        }
        private int CountRow()
        {
            int counter = 0;
            foreach (var item2 in grid1.Children)
            {
                if (item2 is TextBox)
                {
                    TextBox item = item2 as TextBox;
                    if (item.Visibility == Visibility.Visible && Grid.GetRow(item) == 0)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private void CreateTextBoxMatrix()
        {
            TextBox style = grid1.FindName("_01") as TextBox;
            int counter = CountRow();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    TextBox xd = new TextBox();
                    xd.Text = "0";
                    xd.Style = style.Style;                 //nazwa to "_(wiersz)(kolumna)"
                    xd.Name = "_" + i + j;
                    xd.Visibility = Visibility.Hidden;
                    Grid.SetColumn(xd, j);
                    Grid.SetRow(xd, i);
                    if (i != j)
                    {
                        xd.Focusable = true;
                    }

                    grid1.Children.Add(xd);
                }
            }
                string txt = "_" + 1 + 1;
                TextBox tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
                tx.Visibility = Visibility.Visible;
            
                txt = "_" + 1 + 2;
                tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
                tx.Visibility = Visibility.Visible;

                txt = "_" + 2 + 1;
                tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
                tx.Visibility = Visibility.Visible;

                txt = "_" + 2 + 2;
                tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
                tx.Visibility = Visibility.Visible;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int row = Grid.GetRow(tb);
            int col = Grid.GetColumn(tb);
            Regex rx = new Regex("[A-Z]");
            foreach (var item2 in grid1.Children)
                {
                    if (item2 is TextBox)
                    {
                        TextBox item = item2 as TextBox;
                        if (Grid.GetRow(item) == row && (Grid.GetRow(item) != 0 || Grid.GetColumn(item) != 0))
                        {
                            item.Background = Brushes.White;
                            item.Foreground = Brushes.Black;
                            if (rx.IsMatch(item.Text))
                            {
                            item.Background = Brushes.Green;
                            }
                        }
                        if (Grid.GetColumn(item) == col && (Grid.GetRow(item) != 0 || Grid.GetColumn(item) != 0))
                        {
                            item.Background = Brushes.White;
                            item.Foreground = Brushes.Black;
                            if (rx.IsMatch(item.Text))
                            {
                                item.Background = Brushes.Green;
                            }
                    }
                    }
                }
            }


        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int row = Grid.GetRow(tb);
            int col = Grid.GetColumn(tb);
            BrushConverter bc = new BrushConverter();
                foreach (var item2 in grid1.Children)
                {
                    if (item2 is TextBox)
                    {
                        TextBox item = item2 as TextBox;

                        if (Grid.GetRow(item) == row && (Grid.GetRow(item) != 0 || Grid.GetColumn(item) != 0))
                        {
                            item.Background = (Brush)bc.ConvertFrom("#FF767676");
                            item.Foreground = Brushes.White;
                        }
                        if (Grid.GetColumn(item) == col && (Grid.GetColumn(item) != 0 || Grid.GetRow(item) != 0))
                        {
                            item.Background = (Brush)bc.ConvertFrom("#FF767676");
                            item.Foreground = Brushes.White;
                        }
                    }
                }
            }
      
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.SelectAll();
        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            {
                TextBox tb = (sender as TextBox);
                if (tb != null)
                {
                    if (!tb.IsKeyboardFocusWithin)
                    {
                        e.Handled = true;
                        tb.Focus();
                    }
                }
            }
        }

        private void RunAlgorythm(object sender, RoutedEventArgs e)
        {
            int counter = CountRow();
            Graph graph = new Graph(counter, counter);
            int i = 0;
            int j = 0;
            

            foreach (var item2 in grid1.Children)
            {
                if (item2 is TextBox)
                {
                    if (j==counter)
                    {
                        j = 0;
                        i++;
                    }
                    TextBox item = item2 as TextBox;
                    Regex rx = new Regex("[A-Z]");
                    if (rx.IsMatch(item.Text) | item.Visibility == Visibility.Hidden)
                    {
                        continue;
                    }
                    graph.matrix[i,j] = Convert.ToInt32(item.Text);
                    j++;
                }
            }
            for (int k = 0; k < counter; k++)
            {
                for (int l = 0; l < counter; l++)
                {
                    Console.Write(graph.matrix[k,l] + " ");
                }
                Console.WriteLine();
            }

            int a = algorytm2.Algorytm.BestPlace(graph.matrix);
            string txt = "_0" + (a + 1);
            Console.WriteLine(txt);
            try
            {
                TextBox tx = (TextBox)LogicalTreeHelper.FindLogicalNode(grid1, txt);
                Console.WriteLine(tx.Text);

                Button tx2 = (Button)LogicalTreeHelper.FindLogicalNode(grid1, "Wynik");
                tx2.Content = tx.Text;
            }
            catch (Exception)
            {
                Button tx2 = (Button)LogicalTreeHelper.FindLogicalNode(grid1, "Wynik");
                tx2.Content = "Brak";
                return;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("(0)|([1-9])|([1-9][0-9])");

            TextBox tx = sender as TextBox;
            if (!rx.IsMatch(tx.Text))
            {
                tx.Text = "0";
            }
            string txt = "_" + Grid.GetColumn(tx) + Grid.GetRow(tx);
            TextBox tx2 = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
            tx2.Text = tx.Text;
        }

        private void AddTextBoxMatrix(object sender, RoutedEventArgs e)
        {
            int counter = CountRow();
            if (counter == 10)
            {
                return;
            }
            counter++;

            string txt = "_" + counter + counter;
            TextBox tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
            tx.Visibility = Visibility.Visible;

            for (int i = 0; i < counter; i++)
            {
                txt = "_" + counter + i;
                tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
                tx.Visibility = Visibility.Visible;

                txt = "_" + i + counter;
                tx = (TextBox)(LogicalTreeHelper.FindLogicalNode(grid1, txt));
                tx.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
