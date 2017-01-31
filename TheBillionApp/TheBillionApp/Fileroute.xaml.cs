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
using System.Windows.Shapes;

namespace TheBillionApp
{
    /// <summary>
    /// Interaction logic for Fileroute.xaml
    /// </summary>
    public partial class Fileroute : Window
    {
        
        public static string route1, route2, newroute;
        MainWindow xx;

        public Fileroute(MainWindow x)
        {
            InitializeComponent();
            xx = x;
          


        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void seleccionado(object sender, RoutedEventArgs e)
        {
            route1 = getroute1.Text;
            route2 = getroute2.Text;
            newroute = getnewroute.Text;

   
            xx.getroute1 = route1;
            xx.getroute2 = route2;
            xx.getnewroute = newroute;
            generarDBF.getnewroute2 = newroute;
            generarCSV.getnewroute3 = newroute;
            generarExcel.getnewroute4 = newroute;
           

            this.Hide();
            xx.Show();
            xx.getfileroutes();
            

            this.Close();
        


        }
       
    }
}
