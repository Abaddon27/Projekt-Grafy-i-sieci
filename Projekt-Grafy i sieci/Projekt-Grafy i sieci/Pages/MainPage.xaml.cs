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
            TextBox style = grid1.FindName("s") as TextBox;

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10 ; j++)
                {
                    TextBox xd = new TextBox();
                    xd.Text = "0";
                    xd.Style = style.Style;
                    //nazwa to _(wiersz)(kolumna)
                    xd.Name = "_" + i + j;
                    Grid.SetColumn(xd, j);
                    Grid.SetRow(xd, i);
                    if (i!=j)
                    {
                        xd.Focusable = true;
                    }

                    grid1.Children.Add(xd);
                }
            }
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int row = Grid.GetRow(tb);
            int col = Grid.GetColumn(tb);
            foreach (TextBox item in grid1.Children)
            {
                if (item is TextBox)
                {
                    if (Grid.GetRow(item) == row && (Grid.GetRow(item) != 0 || Grid.GetColumn(item) != 0))
                    {
                        item.Background = Brushes.White;
                        item.Foreground = Brushes.Black;
                    }
                    if (Grid.GetColumn(item) == col && (Grid.GetRow(item) != 0 || Grid.GetColumn(item) != 0))
                    {
                        item.Background = Brushes.White;
                        item.Foreground = Brushes.Black;
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

            foreach (TextBox item in grid1.Children)
            {
                if (item is TextBox)
                {
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
    }
}
