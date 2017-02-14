using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheBillionApp
{
    class generarExcel
    {
        List<empresa> empresas;
        public int total { get; set; }
        public static string getnewroute4;
        public generarExcel(List<empresa> empresas)
        {
    
              
            
        }

        public Boolean generarEmpresa(empresa e)
        {

            string nombre =@"\"+ e.clave + ".xls";
            getnewroute4 = @Fileroute.newroute;

            string ruta = getnewroute4;
       
            string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ruta + nombre +" ;Extended Properties='';Excel 8.0;HDR=NO;";

            string connectionString = con;
            int hora = 0;
            int minuto = 0;
            int conta = 0;
            int contaMin = 0;
            total++;
            bool bandera = false;
            DbProviderFactory factory =
              DbProviderFactories.GetFactory("System.Data.OleDb");

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    connection.Open();  //open the connection
                    string nombrame = e.clave;
                    contaMin++;
                    conta++;
                    if (conta % 12 == 0)
                        hora++;
                    minuto = contaMin * 5;
                    if (minuto == 60)
                    {
                        minuto = 0;
                        contaMin = 0;
                        bandera = true;
                    }

                    if (e.clave == "DA013111")
                        nombrame = "error";

                
                  
                        try { 
                        command.CommandText = "CREATE TABLE [" + nombrame + "] (FECHA char(50),HORA char(50),MINUTO char(50), KWH char(50), KVARH char(50), KW char(50))";
                        command.ExecuteNonQuery();

                        foreach (lectura l in e.getLectura())
                        {
                            command.CommandText = "INSERT INTO [" + nombrame + "$]  VALUES('" + l.fecha + "'," + hora + "," + minuto + "," + l.e + "," + l.r + ",0)";
                            command.ExecuteNonQuery();

                        }
                        }
                        catch (Exception er) { MessageBox.Show(er.ToString()); }

                    











                }
                connection.Close();
            }
            MessageBox.Show("Se genero el XLS de la empresa" + e.clave);


            return true;
        }




        public void generarEmpresa(List<empresa> em, generador xxx)
        {
            foreach (empresa e in em)
            {
                string nombre = @"\" + e.clave + ".xls";

                getnewroute4 = @Fileroute.newroute;
                total++;
                string ruta = getnewroute4;
                
                int hora = 0;
                int minuto = 0;
                int conta = 0;
                int contaMin = 0;

                string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ruta + nombre + " ;Extended Properties='';Excel 8.0;HDR=NO;";
                string connectionString = con;

                DbProviderFactory factory =
                  DbProviderFactories.GetFactory("System.Data.OleDb");

                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    using (DbCommand command = connection.CreateCommand())
                    {
                        connection.Open();  //open the connection
                        string nombrame = e.clave;

                        if (e.clave == "DA013111")
                            nombrame = "error";

                      
                            try
                            {
                            command.CommandText = "CREATE TABLE [" + nombrame + "] (FECHA char(50),HORA char(50),MINUTO char(50), KWH char(50), KVARH char(50), KW char(50))";

                            command.ExecuteNonQuery();

                                foreach (lectura l in e.getLectura())
                                {
                                command.CommandText = "INSERT INTO [" + nombrame + "$]  VALUES('" + l.fecha + "'," + hora + "," + minuto + "," + l.e + "," + l.r + ",0)";
                                command.ExecuteNonQuery();

                                }
                            }
                            catch (Exception er) { MessageBox.Show(er.ToString()); }


                        
                        



                    }
                    connection.Close();
                }


                xxx.c1++;
            }
        }
    }
}
