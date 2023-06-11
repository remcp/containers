using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace containership
{
    internal class Ship
    {
        public string Name { get; set; }
        public int Lenght { get; set; }
        public int Width { get; set; }
        public int MaxWeight { get; set; }
        public int LeftWeight { get; set; }
        public int RightWeight { get; set; }

        public Ship(int lenght, int width, int maxweight)
        {
            Lenght = lenght;
            Width = width;
            MaxWeight = maxweight;
        }
    }
}
