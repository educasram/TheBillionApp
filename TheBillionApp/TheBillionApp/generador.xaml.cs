using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TheBillionApp
{
    /// <summary>
    /// Lógica de interacción para generador.xaml
    /// </summary>
    public partial class generador : Window
    {
        
        MainWindow xx;
        
        int total = 0, completado = 0;
        public int c1 = 0, c2 = 0, c3 = 0;
        private object dispatcherTimer;

        public generador(MainWindow x)
        {
          
        
            InitializeComponent();


            xx=x;
            total = xx.empresas.Count();
            xls.Minimum = 0;
            xls.Maximum = total;
            csv.Minimum = 0;
            csv.Maximum = total;
            dbf.Minimum = 0;
            dbf.Maximum = total;



            DispatcherTimer dispathcer = new DispatcherTimer();

  
            dispathcer.Interval = new TimeSpan(0, 0, 1);

            dispathcer.Tick += (s, a) =>
            {

                Timer1_Tick();


            };
            dispathcer.Start();

            Thread te = new Thread(generaXLS);
            te.Start();
            Thread te2 = new Thread(generaDBF);
            te2.Start();
            Thread te3 = new Thread(generaCSV);
            te3.Start();
        }

    

        private void completadolisto()
        {
            if(xls.Value==total && csv.Value == total && dbf.Value == total)
            {
                this.Hide();
                xx.Show();
                this.Close();
            }
        }
        private void Timer1_Tick()
        {
            masXls();
            masDbf();
            masCsv();
            completadolisto();

        }
        public void masXls()
        {    
            xls.Value=c1;
        }
        public void masDbf()
        {
            dbf.Value=c2;
        }
        public void masCsv()
        {
            csv.Value=c3;
        }
        private void generaXLS()
        {
       
            generarExcel t2 = new generarExcel(xx.empresas);
            t2.generarEmpresa(xx.empresas,this);
        }
        private void generaDBF()
        {
            generarDBF m = new generarDBF();
            m.generar(xx.empresas,this);
  
        }
        private void generaCSV()
        {
  
            generarCSV t1 = new generarCSV();
            t1.generar(xx.empresas,this);
  
        }
    }
}
