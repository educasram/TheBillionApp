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
                    lista += fila["TABLE_NAME"].ToString() + "\n";

                    if (fila["TABLE_NAME"].ToString().EndsWith("$"))
                    {
                        string nombreHoja = fila["TABLE_NAME"].ToString();
                        MessageBox.Show(nombreHoja);
                        cmd2.CommandText = "SELECT * FROM [" + nombreHoja + "]";
                        cmd2.Connection = conn;
                        da2.SelectCommand = cmd2;
                        conn2.Open();
                        da2.Fill(ds2);
                        foreach (DataRow fila2 in ds2.Tables[0].Rows)
                        {

                            MessageBox.Show(fila2[0] + " - " + fila2[6]);


                        }
                        MessageBox.Show("Fichero Procesado Correctamente");
                        conn2.Close();



                    }
                }
                MessageBox.Show(lista);

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

        public void getEmpesas()
        {
            var conn = new OleDbConnection();
            var conn2 = new OleDbConnection();
            var cmd = new OleDbCommand();
            var da = new OleDbDataAdapter();
            var ds = new DataSet();
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

                    MessageBox.Show(fila2[0] + " - " + fila2[1]);


                }


            }
            catch (Exception er) { }
        }
    }
}
