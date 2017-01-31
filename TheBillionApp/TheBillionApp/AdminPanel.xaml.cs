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
//using MySql.Data.MySqlClient;
using System.Data;

namespace TheBillionApp
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
      
        public string MyConnectionString = "Server=apprentix.com.mx.mysql; Database=apprentix_com_mx_billion; Uid=apprentix_com_mx_billion@10.24.2.48; Pwd=V8A2IJ6iM6; Port=3306; ";
   
        public AdminPanel()
        {
            InitializeComponent();
            //hola test XD
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
           /* string idservicio = txtServicio.Text;
            string nombreservicio = txtNServicio.Text;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
           
            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO servicios(id, servicio, nombre) VALUES(@id,@servicio,@nombre)";
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@servicio", idservicio);
                cmd.Parameters.AddWithValue("@nombre", nombreservicio);
            }
            catch (Exception)
            {
                throw;
             
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Clone();
                    LoadData();
                }
            }
            */

        }

        private void LoadData()
        {
           /* MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "Select * From servicios";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adap.Fill(ds);
            
                grid1.ItemsSource = ds.DefaultView;
              

            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Clone();
                   
                }
            }*/
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
