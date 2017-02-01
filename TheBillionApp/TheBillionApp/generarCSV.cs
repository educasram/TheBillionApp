using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheBillionApp
{
    class generarCSV
    {
        public static string getnewroute3;
        public generarCSV() { }
      
        public Boolean generar(empresa e)
        {
            getnewroute3 = @Fileroute.newroute;
            //Exportamos el CVS ...
            StringBuilder tmpCSV = new StringBuilder();
            string tmpLinea = "";
     
            if (e.columnas == 9)
            {

                tmpCSV.Append("(LocalTime;kVARhQ1;kVARhQ2;kVARhQ3;kVARhQ4;kWhE;kWhR;PTxCTPM");
                tmpCSV.Append("\r\n");
                foreach (lectura l in e.getLectura())
                {

                    tmpLinea = "";

                    tmpLinea += l.fecha + ";";
                    tmpLinea += l.q1 + ";";
                    tmpLinea += l.q2 + ";";
                    tmpLinea += l.q3 + ";";
                    tmpLinea += l.q4 + ";";
                    tmpLinea += l.cantidad + ";";
                    tmpLinea += l.r + ";";
                    tmpLinea += l.pm;


                    tmpCSV.Append(tmpLinea + "\r\n");
                }

                try
                {
                    string csvFile = e.clave + ".csv";
                    
                    string csvFilePath = getnewroute3 + csvFile;
                    using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                    {
                        sw.Write(tmpCSV.ToString());
                        sw.Close();
                    }
       
                }
                catch (Exception ex)
                {
             
                    return false;
                }
            }

            if (e.columnas == 8)
            {

                tmpCSV.Append("(LocalTime;kVARhQ1;kVARhQ2;kVARhQ3;kVARhQ4;kWhE;kWhR;");
                tmpCSV.Append("\r\n");
                foreach (lectura l in e.getLectura())
                {

                    tmpLinea = "";

                    tmpLinea += l.fecha + ";";
                    tmpLinea += l.q1 + ";";
                    tmpLinea += l.q2 + ";";
                    tmpLinea += l.q3 + ";";
                    tmpLinea += l.q4 + ";";
                    tmpLinea += l.cantidad + ";";
                    tmpLinea += l.r;
   


                    tmpCSV.Append(tmpLinea + "\r\n");
                }

                try
                {
                    string csvFile = e.clave + ".csv";
                 
                    string csvFilePath = getnewroute3 + csvFile;
                    using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                    {
                        sw.Write(tmpCSV.ToString());
                        sw.Close();
                    }

                }
                catch (Exception ex)
                {

                    return false;
                }
            }

            if (e.columnas == 5)
            {

                tmpCSV.Append("(LocalTime;kVARhQ1;kVARhQ4;kWh");
                tmpCSV.Append("\r\n");
                foreach (lectura l in e.getLectura())
                {

                    tmpLinea = "";

                    tmpLinea += l.fecha + ";";
                    tmpLinea += l.q1 + ";";
                    tmpLinea += l.q4 + ";";
                    tmpLinea += l.cantidad;


                    tmpCSV.Append(tmpLinea + "\r\n");
                }

                try
                {
                    string csvFile = e.clave + ".csv";
                   
                    string csvFilePath = getnewroute3+@"\" + csvFile;
                    using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                    {
                        sw.Write(tmpCSV.ToString());
                        sw.Close();
                    }

                }
                catch (Exception ex)
                {

                    return false;
                }
            }
         
            return true;
        }




        public void generar(List<empresa> e)
        {
            getnewroute3 = @Fileroute.newroute;
            //Exportamos el CVS ...
            StringBuilder tmpCSV = new StringBuilder();
            string tmpLinea = "";
            foreach (empresa em in e)
            {
                if (em.columnas == 9)
                {

                    tmpCSV.Append("(LocalTime;kVARhQ1;kVARhQ2;kVARhQ3;kVARhQ4;kWhE;kWhR;PTxCTPM");
                    tmpCSV.Append("\r\n");
                    foreach (lectura l in em.getLectura())
                    {

                        tmpLinea = "";

                        tmpLinea += l.fecha + ";";
                        tmpLinea += l.q1 + ";";
                        tmpLinea += l.q2 + ";";
                        tmpLinea += l.q3 + ";";
                        tmpLinea += l.q4 + ";";
                        tmpLinea += l.cantidad + ";";
                        tmpLinea += l.r + ";";
                        tmpLinea += l.pm;


                        tmpCSV.Append(tmpLinea + "\r\n");
                    }

                    try
                    {
                        string csvFile = em.clave + ".csv";

                        string csvFilePath = getnewroute3 + csvFile;
                        using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                        {
                            sw.Write(tmpCSV.ToString());
                            sw.Close();
                        }

                    }
                    catch (Exception ex)
                    {

                     
                    }
                }

                if (em.columnas == 8)
                {

                    tmpCSV.Append("(LocalTime;kVARhQ1;kVARhQ2;kVARhQ3;kVARhQ4;kWhE;kWhR;");
                    tmpCSV.Append("\r\n");
                    foreach (lectura l in em.getLectura())
                    {

                        tmpLinea = "";

                        tmpLinea += l.fecha + ";";
                        tmpLinea += l.q1 + ";";
                        tmpLinea += l.q2 + ";";
                        tmpLinea += l.q3 + ";";
                        tmpLinea += l.q4 + ";";
                        tmpLinea += l.cantidad + ";";
                        tmpLinea += l.r;



                        tmpCSV.Append(tmpLinea + "\r\n");
                    }

                    try
                    {
                        string csvFile = em.clave + ".csv";

                        string csvFilePath = getnewroute3 + csvFile;
                        using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                        {
                            sw.Write(tmpCSV.ToString());
                            sw.Close();
                        }

                    }
                    catch (Exception ex)
                    {

         
                    }
                }

                if (em.columnas == 5)
                {

                    tmpCSV.Append("(LocalTime;kVARhQ1;kVARhQ4;kWh");
                    tmpCSV.Append("\r\n");
                    foreach (lectura l in em.getLectura())
                    {

                        tmpLinea = "";

                        tmpLinea += l.fecha + ";";
                        tmpLinea += l.q1 + ";";
                        tmpLinea += l.q4 + ";";
                        tmpLinea += l.cantidad;


                        tmpCSV.Append(tmpLinea + "\r\n");
                    }

                    try
                    {
                        string csvFile = em.clave + ".csv";

                        string csvFilePath = getnewroute3 + @"\" + csvFile;
                        using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                        {
                            sw.Write(tmpCSV.ToString());
                            sw.Close();
                        }

                    }
                    catch (Exception ex)
                    {

                      
                    }
                }

            }//foreach
        }
    }
}
