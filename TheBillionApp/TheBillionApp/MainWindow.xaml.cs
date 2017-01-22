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
    public partial class MainWindow : Window
    {
        private List<empresa> empresas;
        
        public MainWindow()
        {
            InitializeComponent();
            descromprimir();
            descromprimir2();
            getEmpesas();
           

        }

        private void descromprimir()
        {
            System.Diagnostics.Process proceso1 = new System.Diagnostics.Process();
           
            string ruta1 = @"c:\archivos\IMP.rar";
          
            string destino1= @"c:\archivos";
            proceso1.StartInfo.FileName = @"C:\Program Files\WinRAR\UnRAR.exe";//tienen que tener instalado winrar
            proceso1.StartInfo.CreateNoWindow = true;
            proceso1.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proceso1.EnableRaisingEvents = false;
            proceso1.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", ruta1, destino1);
            proceso1.Start();


            MessageBox.Show("descompresion terminada archivo IMP");
        }
        private void descromprimir2()
        {
            System.Diagnostics.Process proceso2= new System.Diagnostics.Process();

            string ruta2 = @"c:\archivos\AUTO.rar";

            string destino2 = @"c:\archivos";
            proceso2.StartInfo.FileName = @"C:\Program Files\WinRAR\UnRAR.exe";
            proceso2.StartInfo.CreateNoWindow = true;
            proceso2.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proceso2.EnableRaisingEvents = false;
            proceso2.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", ruta2, destino2);
            proceso2.Start();


            MessageBox.Show("descompresion terminada archivo AUTO");
        }
        private void datos()
        {

            var conn = new OleDbConnection();
            var conn2 = new OleDbConnection();
            var cmd = new OleDbCommand();
            var da = new OleDbDataAdapter();
            var ds = new DataSet();
            var cmd2 = new OleDbCommand();
            var da2 = new OleDbDataAdapter();
            var ds2 = new DataSet();
            try
            {


                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\archivos\IMP" + ";Mode=Read;Extended Properties=Excel 8.0;Persist Security Info=False;";
                conn2.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\archivos\IMP" + ";Mode=Read;Extended Properties=Excel 8.0;Persist Security Info=False;";
                cmd.CommandText = "SELECT * FROM [DA15F203$]"; // no olivdar incluir el simbolo de peso
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                conn.Open();
                da.Fill(ds);


                DataTable tblHojas = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                         new object[] { null, null, null, "TABLE" });
                //OBTENEMOS CANTIDAD DE PESTAÑAS DE ARCHIVO
                int nrorows = tblHojas.Rows.Count;
                string lista = "";
                foreach (DataRow fila in tblHojas.Rows)
                {
                    int indice = existe(fila["TABLE_NAME"].ToString());
                    if (indice > -1)
                    {

                        if (fila["TABLE_NAME"].ToString().EndsWith("$"))
                        {
                            List<lectura> lectura = new List<lectura>();
                             string nombreHoja = fila["TABLE_NAME"].ToString();
             
                            cmd2.CommandText = "SELECT * FROM [" + nombreHoja + "]";
                            cmd2.Connection = conn;
                            da2.SelectCommand = cmd2;
                            conn2.Open();
                            da2.Fill(ds2);
                            foreach (DataRow fila2 in ds2.Tables[0].Rows)
                            {
                                float t = 0.0f;
                                if (!float.TryParse(fila2[6].ToString(), out t))
                                    t = -1;
                                lectura.Add(new lectura(fila2[0].ToString(), t));


                            }
                            conn2.Close();
                           // MessageBox.Show(indice.ToString());

                            empresas[indice-2].lista = lectura;



                        }
                    }
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

        private int existe(string termino)
        {
            int pos = -1;
            int conta = 0;

            foreach(empresa e in empresas)
            {
                conta++;
                if (e.clave == termino)
                    pos = conta;
            }


            return conta;
        }

        public void getEmpesas()
        {
            empresas = new List<empresa>();
            var conn = new OleDbConnection();
            var cmd = new OleDbCommand();
            var da = new OleDbDataAdapter();
            var ds = new DataSet();
            string indice, nombre;
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
                                empresas.Add(new empresa(indice, ex2[0]));
                            }
                        }
                            

                    }
                    



                }
                datos();


            }
            catch (Exception er) { MessageBox.Show(er.ToString());}
        }
    }
}
