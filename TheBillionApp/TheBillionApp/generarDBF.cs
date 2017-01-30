using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBillionApp
{
    class generarDBF
    {
        public generarDBF()
        {

        }
        public void generar(empresa e)
        {
            string ruta = @"C:\archivos";
            string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                                   ";Data Source = " + ruta +
                                   ";Extended Properties = dBASE IV" +
                                   ";User ID=Admin;Password=;";

            // Se borra el fichero en caso de que exista
            File.Delete(ruta + "\\"+ e.clave+".DBF");

            using (OleDbConnection cn = new OleDbConnection(strConnDbase))
            {
                // Pruebo a crear una tabla directamente
                string sql = "CREATE TABLE "+ e.clave+ "(loctim char(50), kVARhQ1 char(50), kVARhQ2 char(50), kVARhQ3 char(50), kVARhQ4 char(50), kWhE char(50), kWhR char(50), PTxCTPM char(50) )";
                using (OleDbCommand cmd = new OleDbCommand(sql, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }

                cn.Open();
                foreach (lectura l in e.getLectura())
                {

                    string sd = "Insert into salida(Campo1,Campo2) VALUES(@campo1,campo2);";
                    sd = "INSERT INTO " + e.clave + " VALUES('" + l.fecha + "','" + l.q1 + "','" + l.q2 + "','" + l.q3 + "','" + l.q4 + "','" + l.e + "','" + l.r + "','" + l.pm + "')";
                    using (OleDbCommand cmd = new OleDbCommand(sd, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                cn.Close();

              

            }
        }
    }
}
