using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\archivos\TestSO1.xls;Extended Properties=""Excel 8.0;HDR=NO;""";
            DbProviderFactory factory =
              DbProviderFactories.GetFactory("System.Data.OleDb");

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    connection.Open();  //open the connection

                  
                            command.CommandText = "CREATE TABLE [" + e.clave + "] (LocalTime char(255), kVARhQ1 char(255), kVARhQ2 char(255), kVARhQ3 char(255), kVARhQ4 char(255), kWhE char(255), kWhR char(255), PTxCTPM char(255))";
                            command.ExecuteNonQuery();

                            foreach (lectura l in e.getLectura())
                            {
                                command.CommandText = "INSERT INTO [" + e.clave + "$]  VALUES('" + l.fecha + "'," + l.q1 + "," + l.q2 + "," + l.q3 + "," + l.q4 + "," + l.e + "," + l.r + "," + l.pm + ")";
                                command.ExecuteNonQuery();

                            }
                        


                   

                  
                }
                connection.Close();
            }


            return true;
        }
    }
}
