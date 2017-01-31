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
        public generarExcel(List<empresa> empresas)
        {
            this.empresas = empresas;
               string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\archivos\TestSO1.xls;Extended Properties=""Excel 8.0;HDR=NO;""";
                DbProviderFactory factory =
                  DbProviderFactories.GetFactory("System.Data.OleDb");

              
            
        }

        public Boolean generarEmpresa(empresa e)
        {

            string nombre = e.clave + ".xls";
            string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=@Fileroute.newroute" + nombre +" ;Extended Properties='';Excel 8.0;HDR=NO;";

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

                    if (e.columnas == 8)
                    {
                        try
                        {
                            command.CommandText = "CREATE TABLE [" + nombrame + "] (LocalTime char(255), kVARhQ1 char(255), kVARhQ2 char(255), kVARhQ3 char(255), kVARhQ4 char(255), kWhE char(255), kWhR char(255))";
                            command.ExecuteNonQuery();

                            foreach (lectura l in e.getLectura())
                            {
                                command.CommandText = "INSERT INTO [" + nombrame + "$]  VALUES('" + l.fecha + "'," + l.q1 + "," + l.q2 + "," + l.q3 + "," + l.q4 + "," + l.cantidad + "," + l.r + ")";
                                command.ExecuteNonQuery();

                            }
                        }
                        catch (Exception er) { MessageBox.Show(er.ToString()); }


                    }
                    if (e.columnas == 9)
                    {
                        try { 
                        command.CommandText = "CREATE TABLE [" + nombrame + "] (LocalTime char(255), kVARhQ1 char(255), kVARhQ2 char(255), kVARhQ3 char(255), kVARhQ4 char(255), kWhE char(255), kWhR char(255), PTxCTPM char(255))";
                        command.ExecuteNonQuery();

                        foreach (lectura l in e.getLectura())
                        {
                            command.CommandText = "INSERT INTO [" + nombrame + "$]  VALUES('" + l.fecha + "'," + l.q1 + "," + l.q2 + "," + l.q3 + "," + l.q4 + "," + l.cantidad + "," + l.r + "," + l.pm + ")";
                            command.ExecuteNonQuery();

                        }
                        }
                        catch (Exception er) { MessageBox.Show(er.ToString()); }

                    }
                    if (e.columnas == 5)
                    {
                        try { 
                        command.CommandText = "CREATE TABLE [" + nombrame + "] (LocalTime char(255), kVARhQ1 char(255), kVARhQ4 char(255), kWhE char(255))";
                        command.ExecuteNonQuery();

                        foreach (lectura l in e.getLectura())
                        {
                            command.CommandText = "INSERT INTO [" + nombrame + "$]  VALUES('" + l.fecha + "'," + l.q1 + "," + l.q4 + "," + l.cantidad +")";
                            command.ExecuteNonQuery();

                        }
                        }
                        catch (Exception er) { MessageBox.Show(er.ToString()); }

                    }











                }
                connection.Close();
            }
            MessageBox.Show("Se genero el XLS de la empresa" + e.clave);


            return true;
        }
    }
}
