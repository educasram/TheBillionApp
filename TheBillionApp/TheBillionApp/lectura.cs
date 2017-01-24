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
        public float cantidad { get; set; }
        public float q1 { get; set; }
        public float q2 { get; set; }
        public float q3 { get; set; }
        public float q4 { get; set; }
        public float e { get; set; }
        public float r { get; set; }
        public float pm { get; set; }
        public lectura(string f, float c, float q1, float q2, float q3, float q4, float e, float pm)
        {
            fecha = f;
            cantidad = c;
            this.q1 = q1;
            this.q2 = q2;
            this.q3 = q3;
            this.q4 = q4;
            this.e = e;
            this.pm = pm;
        }

    }
}
        