using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace containership
{
    internal class Ship
    {
        public List<List<Container>> Containerfields = new List<List<Container>>();

        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int MaxWeight { get; set; }
        public int LeftWeight { get; set; }
        public int RightWeight { get; set; }

        public Ship(int lenght, int width)
        {
            Length = lenght;
            Width = width;
            MaxWeight = (lenght * width) * 150;
            
        }

        public void setcontainerfields()
        {
            int amount = Length * Width;
            for(int i = 0; i < amount; i++)
            {
                Containerfields.Add(new List<Container>());
            }
        }

        public void printblueprint()
        {
            int containerfield = 0;
            string fill = "";
            if(Length * Width > 9)
            {
                fill = " ";
            }
            for(int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("|"+containerfield+fill+"|");
                    containerfield++;
                    if(containerfield > 9)
                    {
                        fill = "";
                    }
                }
                Console.WriteLine();
            }
        }



        public bool addcontainer(Container container)
        {
            bool added = false;
            for(int i = 0; i < Containerfields.Count; i++) 
            {
                //check if container does not exceed ship maximum weight
                if (MaxWeight + container.Weight < MaxWeight)
                {
                    //check if the left side of the ship is not more than 20% heavier than the right side of the ship
                    if (exceedleft(container.Weight) == false)
                    {
                        //container can only be placed on the left side of the ship or in the middle row
                        if(i % Width <= Width / 2)
                        {
                            //check if valuable container is still reachable
                            if (checknexttovaluable(container,i) == false)
                            {
                                if (coolablecheck(container, i) == true)
                                {
                                    //check if containerfield can have all the containers while not exceeding more than 130 ton on top of the first one
                                    List<Container> dummycontainerlist = Containerfields[i];
                                    dummycontainerlist.Add(container);
                                    dummycontainerlist = weightsort(dummycontainerlist);
                                    if (canaddweightontop(dummycontainerlist) == true)
                                    {
                                        Containerfields[i] = dummycontainerlist;
                                        added = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //check if the right side of the ship is not more than 20% heavier than the left side of the ship
                    else if (exceedright(container.Weight) == false)
                    {
                        //container can only be placed on the right side of the ship or in the middle row
                        if (i % Width >= Width / 2)
                        {
                            if (checknexttovaluable(container,i) == false)
                            {
                                if (coolablecheck(container, i) == true)
                                {
                                    //check if containerfield can have all the containers while not exceeding more than 130 ton on top of the first one
                                    List<Container> dummycontainerlist = Containerfields[i];
                                    dummycontainerlist.Add(container);
                                    dummycontainerlist = weightsort(dummycontainerlist);
                                    if (canaddweightontop(dummycontainerlist) == true)
                                    {
                                        Containerfields[i] = dummycontainerlist;
                                        added = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return added;
        }

        public bool exceedleft(int containerweight)
        {
            int totalweight = LeftWeight+ RightWeight;
            int newleftweight = LeftWeight + containerweight;
            int newpercentageleft = newleftweight / totalweight * 100;
            int percentageright = RightWeight / totalweight * 100;

            if(newpercentageleft > percentageright + 20) 
            {
                return false;
            }
            else return true;
        }

        public bool exceedright(int containerweight)
        {
            int totalweight = LeftWeight + RightWeight;
            int newrightweight = RightWeight + containerweight;
            int newpercentageright = newrightweight / totalweight * 100;
            int percentageleft = LeftWeight / totalweight * 100;

            if (newpercentageright > percentageleft + 20)
            {
                return false;
            }
            else return true;
        }

        public List<Container> weightsort(List<Container> containerlist)
        {
            List<Container> sortedlist = containerlist.OrderBy(o => o.Weight).ToList();
            foreach(Container container in sortedlist)
            {
                if(container.Valuable == true)
                {
                    containerlist.Remove(container);
                    containerlist.Add(container);
                }
            }

            return sortedlist;
        }


        public bool canaddweightontop(List<Container>fieldlist)
        {
            int weightontop = 0;
            for(int i = 0; i < fieldlist.Count; i++)
            {
                if(i != 0)
                {
                    weightontop = weightontop + fieldlist[i].Weight;
                }
            }

            //check if list has more than one valluable container
            int valueableammount = 0;
            for(int i =0; i < fieldlist.Count; i++)
            {
                if (fieldlist[i].Valuable == true)
                {
                    valueableammount++;
                }
                if(valueableammount > 1)
                {
                    return false;
                }
            }
            

            if (weightontop > 120)
            {
                return false;
            }
            else return true;
        }

        public bool checknexttovaluable(Container container, int i)
        {
            bool nextto = false;

            if (container.Valuable == false)
            {
                try
                {
                    if (Containerfields[i + Width][Containerfields[i].Count].Valuable == true)
                    {
                        nextto = true;
                    }
                }
                catch { }
                try
                {
                    if (Containerfields[i - Width][Containerfields[i].Count].Valuable == true)
                    {
                        nextto = true;
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    if (Containerfields[i + Width][Containerfields[i].Count] == null)
                    {
                        nextto = true;
                    }
                }
                catch { }
                try
                {
                    if (Containerfields[i - Width][Containerfields[i].Count] == null)
                    {
                        nextto = true;
                    }
                }
                catch { }
            }

            return nextto;
        }

        public bool coolablecheck(Container container, int i)
        {
            if (container.Coolable == true && i > Width)
            {
                return false;
            }
            else return true;

        }
    }
}
