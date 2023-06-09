using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace containership
{
    internal class Container
    {
        public int Weight { get; set; }
        public bool Valuable { get; set; }
        public bool Coolable { get; set; }

        public Container(int weight, bool valuable, bool coolable)
        {
            if (MinWeight(weight) == false)
            {
                throw new ArgumentException("container needs to be at least 4000kg");
            }
            else
            {
                Weight = weight;
                Valuable = valuable;
                Coolable = coolable;
            }
        }

        public bool MinWeight(int weight)
        {
            if (weight < 4000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
