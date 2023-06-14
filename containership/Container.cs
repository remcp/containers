using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace containership
{
    public class Container
    {
        public int Weight { get;  set; }
        public bool Valuable { get; private set; }
        public bool Coolable { get; private set; }

        public Container(int weight, bool valuable, bool coolable)
        {
            if (checkweight(weight) == false)
            {
                throw new ArgumentException("container needs to be at least 4000kg and max 30000kg");
            }
            else
            {
                Weight = weight;
                Valuable = valuable;
                Coolable = coolable;
            }
        }

        public bool checkweight(int weight)
        {
            if (weight < 4000 | weight > 30000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            string coolable = "no";
            if (Coolable)
            {
                coolable = "yes";
            }

            return Weight.ToString();
        }
    }
}
