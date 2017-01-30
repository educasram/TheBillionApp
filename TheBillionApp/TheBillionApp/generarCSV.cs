using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBillionApp
{
    class generarCSV
    {
        public generarCSV() { }

        public Boolean generar(empresa e)
        {
           
            //Exportamos el CVS ...
            StringBuilder tmpCSV = new StringBuilder();
            string tmpLinea = "";

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
                tmpLinea += l.e + ";";
                tmpLinea += l.r + ";";
                tmpLinea += l.pm;
             

                tmpCSV.Append(tmpLinea + "\r\n");
            }

            try
            {
                string csvFile = e.clave + ".csv";
                string csvFilePath = @"C:\archivos\" + csvFile;
                using (StreamWriter sw = new StreamWriter(@csvFilePath, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(tmpCSV.ToString());
                    sw.Close();
                }
                /*
                //Preparamos el response ...
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=clientes.csv");
                Response.ContentType = "application/vnd.csv";
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                //Cargamos el archivo en memoria ...
                byte[] MyData = (byte[])System.IO.File.ReadAllBytes(csvFilePath);
                Response.BinaryWrite(MyData);

                //Eliminamos el archivo ...
                System.IO.File.Delete(csvFilePath);

                //Terminamos el response ..
                Response.End();
                */
            }
            catch (Exception ex)
            {
                //  ClientScript.RegisterClientScriptBlock(this.GetType(), "CSV", "alert('Error al exportar el fichero')", true);
                return false;
            }
            return true;
        }
    }
}
