using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Controls.DataVisualization.Charting;
namespace TheBillionApp
{
    /// <summary>
    /// Lógica de interacción para grafica.xaml
    /// </summary>
    /// 
    public partial class grafica : Window
    {

        public grafica(empresa x)
        {
            InitializeComponent();
            List<lectura> lista = x.getLectura();
            List<KeyValuePair<string, float>> MyValue = new List<KeyValuePair<string, float>>();
            float prom = 0;
            int conta = 0;


            try
            {
                foreach (lectura e in lista)
                {
                    conta++;
                    prom += e.e;

                    if (conta == 12)
                    {
                        MyValue.Add(new KeyValuePair<string, float>(e.fecha, e.e/12));
                        prom = 0;
                        conta = 0;
                    }

                    

                }

                //    for(int i = 0; i < 30; i++)
                //    {
                //         MyValue.Add(new KeyValuePair<string, float>(lista[i].fecha, lista[i].e));
                // }

                AreaChart1.Title = x.clave;
                AreaChart1.DataContext = MyValue;
            }
            catch (Exception er) { }
           // gra.DataContext = MyValue;




        }
    }
}
