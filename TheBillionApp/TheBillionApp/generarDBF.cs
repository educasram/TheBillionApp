using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheBillionApp
{
    class generarDBF
    {
        public static string getnewroute2;
        public int total { get; set; }

        public generarDBF()
        {
           
        }
      
        public void generar(empresa e)
        {
            int hora = 0;
            int minuto = 0;
            int conta = 0;
            int contaMin = 0;
            bool bandera = false;
            getnewroute2 = @Fileroute.newroute;
            string ruta = getnewroute2;
            string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                                   ";Data Source = " + ruta +
                                   ";Extended Properties = dBASE IV" +
                                   ";User ID=Admin;Password=;";

            // Se borra el fichero en caso de que exista
            File.Delete(ruta + "\\"+ e.clave+".DBF");



            using (OleDbConnection cn = new OleDbConnection(strConnDbase))
            {
              
                    string sql = "CREATE TABLE " + e.clave + "(FECHA char(50),HORA char(50),MINUTO char(50), KWH char(50), KVARH char(50), KW char(50) )";
                    using (OleDbCommand cmd = new OleDbCommand(sql, cn))
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }

                    cn.Open();
                    foreach (lectura l in e.getLectura())
                    {
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
          

                        string sd = "Insert into salida(Campo1,Campo2) VALUES(@campo1,campo2);";
                        sd = "INSERT INTO " + e.clave + " VALUES('" + l.fecha + "','" + hora+ "','" + minuto + "','" + l.e+ "','" + l.r + "','0')";
                        using (OleDbCommand cmd = new OleDbCommand(sd, cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    cn.Close();
               

             
                MessageBox.Show("Se genero el DBF de la empresa " + e.clave);



            }
        }




        public void generar(List<empresa> em, generador xxx)
        {
            foreach(empresa e in em) {
                int hora = 0;
                int minuto = 0;
                int conta = 0;
                int contaMin = 0;
                total++;
               bool  bandera = false;
            getnewroute2 = @Fileroute.newroute;
            string ruta = getnewroute2;
            string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                                   ";Data Source = " + ruta +
                                   ";Extended Properties = dBASE IV" +
                                   ";User ID=Admin;Password=;";

            // Se borra el fichero en caso de que exista
            File.Delete(ruta + "\\" + e.clave + ".DBF");



                using (OleDbConnection cn = new OleDbConnection(strConnDbase))
                {



                    string sql = "CREATE TABLE " + e.clave + "(FECHA char(50),HORA char(50),MINUTO char(50), KWH char(50), KVARH char(50), KW char(50) )";
                    using (OleDbCommand cmd = new OleDbCommand(sql, cn))
                        {
                            cn.Open();
                            cmd.ExecuteNonQuery();
                            cn.Close();
                        }

                        cn.Open();
                        foreach (lectura l in e.getLectura())
                        {
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

                        string sd = "Insert into salida(Campo1,Campo2) VALUES(@campo1,campo2);";
                        sd = "INSERT INTO " + e.clave + " VALUES('" + l.fecha + "','" + hora + "','" + minuto + "','" + l.e + "','" + l.r + "','0')";
                        using (OleDbCommand cmd = new OleDbCommand(sd, cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        cn.Close();
                    
                }



                xxx.c2++;
            }
        }
    }
}
