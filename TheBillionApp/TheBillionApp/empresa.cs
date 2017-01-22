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

        }
    }
}
