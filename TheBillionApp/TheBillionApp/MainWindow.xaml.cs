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
            getEmpesas();

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
                            MessageBox.Show(indice.ToString());

                            empresas[indice].lista = lectura;



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
