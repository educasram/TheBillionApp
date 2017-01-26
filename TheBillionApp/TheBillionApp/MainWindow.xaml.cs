using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using System.IO.Compression;

namespace TheBillionApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private List<empresa> empresas;
        private int totalIntervalos;
       

        public MainWindow()
        {


            InitializeComponent();
            getEmpesas();
            getFecha();
            //ver();
            llenado();           

        }
        public void ver()
        {

            string l = "";
            empresa tem = empresas[0];
            MessageBox.Show(tem.nombre);
        }
        public void llenado()
        {
            tabla.ItemsSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("SERVICIO");
            dt.Columns.Add("NOMBRE");
            dt.Columns.Add("NO DE INTERVALOS");
            dt.Columns.Add("INTERVALOS CON FALLA");
            dt.Columns.Add("LLENAR CON 0");
            dt.Columns.Add("DBF");
            dt.Columns.Add("XLS");
            dt.Columns.Add("CSV");
            
            string[] valores = new string[4];
            string listaRango = "";
            foreach (empresa e in empresas)
            {
                string tem=e.totalDanado.ToString() + "/" + totalIntervalos.ToString();
              //  MessageBox.Show(tem);
                if (e.danado == true)
                {
                    foreach(string t in e.intervaloMal)
                    {
                        listaRango += t + "\n";
                    }
                }
                valores[0] = e.clave.ToString();
                valores[1] = e.nombre;
                valores[2] = tem;
                valores[3] = listaRango;

                dt.Rows.Add(valores);
                listaRango="";


            }
            tabla.ItemsSource = dt.DefaultView;

        }

        private void datos()
        {
            var conn = new OleDbConnection();
            var cmd = new OleDbCommand();
            var da = new OleDbDataAdapter();
            var ds = new DataSet();
            try
            {
                int inidice = -1;
                foreach (empresa fila in empresas)
                {
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\archivos\IMP" + ";Mode=Read;Extended Properties=Excel 8.0;Persist Security Info=False;";

                    cmd.CommandText = "SELECT * FROM ["+fila.clave+"$]"; // no olivdar incluir el simbolo de peso
                    cmd.Connection = conn;
                    da.SelectCommand = cmd;
                    conn.Open();
                    da.Fill(ds);
                    inidice++;
                    int danado = 0;
                    string rango = "",rangoAnterior="";
                    Boolean existeRango = false, existeRango2 = false;
                    List<lectura> lectura = new List<lectura>();
                        foreach (DataRow fila2 in ds.Tables[0].Rows)
                        {
                            float t = 0.0f;
                            if (!float.TryParse(fila2[6].ToString(), out t))
                                t = -1;
                        if (t == -1)
                        {
                            if (rango == "")
                            {
                                rango += fila2[0].ToString();
                                existeRango = true;
                            }else
                                rangoAnterior= fila2[0].ToString();

                            danado++;
                            t = 0;

                        }
                        if(t>0 && existeRango == true)
                        {
                            rango += "-" + rangoAnterior;
                            rangoAnterior = "";
                            empresas[inidice].intervaloMal.Add(rango);
                            rango = "";
                            existeRango = false;
                        }

                            lectura.Add(new lectura(fila2[0].ToString(), t));
                     

                        //   MessageBox.Show(fila2[0].ToString()+" "+t.ToString());
                    }

                    

                    //checar parametro de dañado
                    empresas[inidice].lista = lectura;
                    empresas[inidice].totalDanado = totalIntervalos-danado;
                    if (danado > 0)
                    { empresas[inidice].danado = true; }

                    danado = 0;

                    conn.Close();
                    conn.Dispose();

                    //  MessageBox.Show(lista);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private Boolean existe(string termino)
        {
            int pos = -1;
            int conta = -1;

            foreach(empresa e in empresas)
            {
                conta++;
                if (e.clave == termino)
                    return true;

                              
            }


            return false;
        }

        public void getEmpesas()
        {
            empresas = new List<empresa>();
            var conn = new OleDbConnection();
            var cmd = new OleDbCommand();
            var da = new OleDbDataAdapter();
            var ds = new DataSet();
            string indice, nombre, m = "";
            try
            {

                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\archivos\IMP" + ";Mode=Read;Extended Properties=Excel 8.0;Persist Security Info=False;";
                cmd.CommandText = "SELECT * FROM [|PRIVATE|Status$]"; // no olivdar incluir el simbolo de peso
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                conn.Open();
                da.Fill(ds);
                foreach (DataRow fila2 in ds.Tables[0].Rows)
                {
                    String[] substrings = fila2[1].ToString().Split(':');
                    if(!substrings[0].Equals("Starting report"))
                    {
                        indice = substrings[0];
                        if (substrings.Length > 1)
                        {
                            if (substrings[1].Contains("<")){
                                String[] ex = substrings[1].ToString().Split('<');
                                String[] ex2 = ex[1].ToString().Split('>');
                                if(!existe(indice))
                                empresas.Add(new empresa(indice, ex2[0]));
                                m += indice + "\n";
                            }
                        }
                            

                    }
                    



                }
             //   MessageBox.Show(m);
                datos();


            }
            catch (Exception er) { MessageBox.Show(er.ToString());}
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public void getFecha()
        {
            string fecha;
            fecha = "";
            fecha = empresas[0].lista[0].fecha;
            string[] f = fecha.Split('/');
            string[] m = f[2].Split(' ');
            int a, b;
            int.TryParse(m[0], out b);
            int.TryParse(f[1], out a);
            int dias = System.DateTime.DaysInMonth(b, a);
            totalIntervalos = (((60 / 5) * 24) * dias);
                    

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
