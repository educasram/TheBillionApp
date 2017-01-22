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
        public List<lectura> lista;
        public empresa(string c, string n)
        {
            nombre = n;
            clave = c;
            lista = new List<lectura>();

        }
    }
}
