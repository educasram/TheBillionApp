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
    
    public partial class opcion : Window
    {
        int opt=0;
        MainWindow t;
        public opcion(MainWindow w)
        {
            InitializeComponent();
            t = w;
        }

        private void seleccionado(object sender, RoutedEventArgs e)
        {
            if (opcion1.IsChecked == true)
                opt = 1;
            else
                opt = 2;
            t.opt = opt;
            this.Close();

        }
    }
}
