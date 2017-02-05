using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBillionApp
{
    class dato
    {

        public dato(string Name, float Population)    // Constructor
        {
            this.Name = Name;
            this.Population = Population;
        }
        public string Name                // Name Property
        {
            get;
            set;
        }
        public float Population                // Population Property
        {
            get;
            set;
        }
    }
}
