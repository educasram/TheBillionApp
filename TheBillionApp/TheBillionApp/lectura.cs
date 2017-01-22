using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBillionApp
{
    class lectura
    {
        public string fecha { get; set; }
        public double cantidad { get; set; }
        public lectura(string f, float c)
        {
            fecha = f;
            cantidad = c;
        }

    }
}
        