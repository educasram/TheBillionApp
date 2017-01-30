using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBillionApp
{
    class empresa
    {
        public string clave { get; set; }
        public string nombre { get; set; }
        public Boolean genero0 { get; set; }
        public Boolean generoDBF { get; set; }
        public Boolean generoXLS { get; set; }
        public Boolean generoCSV { get; set; }
        public List<lectura> lista { get; set; }
        public List<string> intervaloMal { get; set; }
        public Boolean danado { get; set; }
        public int totalDanado { get; set; }
        public empresa(string c, string n)
        {
            nombre = n;
            clave = c;
            lista = new List<lectura>();
            intervaloMal = new List<string>();
            danado = false;
            totalDanado = 0;
            genero0 = false;
            generoDBF = false;
            generoXLS = false;
            generoCSV = false;



        }

        public List<string> getIntervaloMal() { return intervaloMal; }
        public List<lectura> getLectura() { return lista; }
        public Boolean getDano() { return danado; }
        public int getTotalDano() { return totalDanado; }

        public void setLectura(lectura m) { lista.Add(m); }
        public void setIntervaloMal(string m) { intervaloMal.Add(m); }
        public void setDanoa(Boolean m) { danado = m; }
        public void setTotalDano(int m) { totalDanado = m; }

        public lectura getElementoLectura(int a) { return lista[a]; }

        public void addIntervaloMal(string t) { intervaloMal.Add(t); }

        public void setListaLectura(List<lectura> m) { lista=m; }


        public string imprimeRango(int a)
        {
            string rango = "";
            foreach(string tem in intervaloMal)
            {
                rango += tem + "\n";
                
            }
            return rango;
        }

    }
}
