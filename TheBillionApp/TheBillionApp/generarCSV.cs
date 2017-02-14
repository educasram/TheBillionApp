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
        public int total { get; set; }
        public generarCSV() { }
      
        public Boolean generar(empresa e)
        {
            getnewroute3 = @Fileroute.newroute;
            //Exportamos el CVS ...
            StringBuilder tmpCSV = new StringBuilder();
            string tmpLinea = "";
            int hora = 0;
            int minuto = 0;
            int conta = 0;
            int contaMin = 0;
            bool bandera = false;

        

                tmpCSV.Append("FECHA,HORA,MINUTO,KWH,KVARH,KW");
                tmpCSV.Append("\r\n");
                foreach (lectura l in e.getLectura())
                {
                tmpLinea = "";
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

                tmpLinea += l.fecha + ",";
                    tmpLinea += hora + ",";
                    tmpLinea += minuto + ",";
                    tmpLinea += l.e + ",";
                tmpLinea += l.r + ",";
                tmpLinea +=  "0";
                


                    tmpCSV.Append(tmpLinea + "\r\n");
                }

                try
                {
                    string csvFile = e.clave + ".csv";
                    
                    string csvFilePath = getnewroute3 + @"\" + csvFile;
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
            

         
            return true;
        }



        public void generar(List<empresa> e,generador xxx)
        {
            getnewroute3 = @Fileroute.newroute;
            //Exportamos el CVS ...
            
            string tmpLinea = "";
            foreach (empresa em in e)
            {
                StringBuilder tmpCSV = new StringBuilder();
                total++;


                int hora = 0;
                int minuto = 0;
                int conta = 0;
                int contaMin = 0;
                bool bandera = false;



                tmpCSV.Append("FECHA,HORA,MINUTO,KWH,KVARH,KW");
                tmpCSV.Append("\r\n");
                    foreach (lectura l in em.getLectura())
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

                    tmpLinea = "";

                    tmpLinea += l.fecha + ",";
                    tmpLinea += hora + ",";
                    tmpLinea += minuto + ",";
                    tmpLinea += l.e + ",";
                    tmpLinea += l.r + ",";
                    tmpLinea += "0";


                    tmpCSV.Append(tmpLinea + "\r\n");
                    }

                  
                        string csvFile = em.clave + ".csv";

                        string csvFilePath = getnewroute3+@"\" + csvFile;
                        using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                        {
                            sw.Write(tmpCSV.ToString());
                            sw.Close();
                        }

           
                


                xxx.c3++;




            }//foreach
        }
    }
}
