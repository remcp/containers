using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public float Width { get; set; }
        public int MaxWeight { get; set; }
        public int LeftWeight { get; set; }
        public int RightWeight { get; set; }
        public int MiddleWeight { get; set; }

        public Ship(int lenght, int width)
        {
            Length = lenght;
            Width = width;
            MaxWeight = (lenght * width) * 150000;
            setcontainerfields();
        }

        public void setcontainerfields()
        {
            int amount = Length * Convert.ToInt32(Width);
            for (int i = 0; i < amount; i++)
            {
                Containerfields.Add(new List<Container>());
            }
        }

        public void printblueprint()
        {
            int containerfield = 0;
            string fill = "";
            if (Length * Width > 9)
            {
                fill = " ";
            }
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("|" + containerfield + fill + "|");
                    containerfield++;
                    if (containerfield > 9)
                    {
                        fill = "";
                    }
                }
                Console.WriteLine();
            }
        }
        public void printlists()
        {
            for (int i = 0; i < Containerfields.Count; i++)
            {
                Console.WriteLine("field " + i);
                Console.WriteLine();
                foreach (Container container in Containerfields[i])
                {
                    string iscoolable = "no";
                    string isvaluable = "no";
                    if (container.Coolable == true)
                    {
                        iscoolable = "yes";
                    }
                    if (container.Valuable == true)
                    {
                        isvaluable = "yes";
                    }
                    Console.WriteLine(container.Weight + " " + " coolable = " + iscoolable + " valuable = " + isvaluable);
                }
            }
        }


        public bool addcontainer(Container container)
        {
            bool added = false;
            float middle = Width/ 2;
            for (int i = 0; i < Containerfields.Count; i++)
            {
                //check if container does not exceed ship maximum weight
                if (LeftWeight + RightWeight + MiddleWeight + container.Weight < MaxWeight)
                {
                    //check if the left side of the ship is not more than 20% heavier than the right side of the ship
                    if (exceedleft(container.Weight) == false)
                    {
                        //container can only be placed on the left side of the ship or in the middle row
                        if (i % Convert.ToInt32(Width) < middle)
                        {
                            added = placecontainer(container, i);
                            if (added == true) 
                            {
                                LeftWeight = LeftWeight + container.Weight;
                                break; 
                            }
                            
                        }
                        else
                        {
                            if (i % Width == Width / 2)
                            {
                                added = placecontainer(container, i);
                                if (added == true)
                                {
                                    MiddleWeight = MiddleWeight + container.Weight;
                                    break;
                                }
                            }
                        }
                    }
                    //check if the right side of the ship is not more than 20% heavier than the left side of the ship
                    else if (exceedright(container.Weight) == false)
                    {
                        //container can only be placed on the right side of the ship or in the middle row
                        if (i % Width > Width / 2)
                        {
                            added = placecontainer(container, i);
                            if (added == true) 
                            { 
                                RightWeight = RightWeight + container.Weight;
                                break; 
                            }
                        }
                        else
                        {
                            if (i % Width == Width / 2)
                            {
                                added = placecontainer(container, i);
                                if (added == true)
                                {
                                    MiddleWeight = MiddleWeight + container.Weight;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return added;
        }

        public bool checkodd()
        {
            bool isodd = false;
            if (Width % 2 == 0)
            {
                isodd = false;
            }
            else
            {
                isodd = true;
            }
            return isodd;
        }
        public bool placecontainer(Container container, int i)
        {
            bool added = false;

            //check if valuable container is still reachable
            if (checknexttovaluable(container, i) == false)
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
                    }
                }
            }
            return added;
        }

        public bool exceedleft(int containerweight)
        {
            int totalweight = LeftWeight + RightWeight + MiddleWeight;
            int newleftweight = LeftWeight + containerweight;
            int newpercentageleft = 0;
            try
            {
                newpercentageleft = newleftweight / totalweight * 100;
            }
            catch (DivideByZeroException)
            {
                return false;
            }
            int percentageright = RightWeight / totalweight * 100;

            if (newpercentageleft !> percentageright + 20)
            {
                return true;
            }
            else return false;
        }

        public bool exceedright(int containerweight)
        {
            int totalweight = LeftWeight + RightWeight + MiddleWeight;
            int newrightweight = RightWeight + containerweight;
            int newpercentageright = 0;

            try
            {
                newpercentageright = newrightweight / totalweight * 100;
            }
            catch (DivideByZeroException)
            {
                return false;
            }
            int percentageleft = LeftWeight / totalweight * 100;

            if (newpercentageright > percentageleft + 20)
            {
                return true;
            }
            else return false;
        }

        public List<Container> weightsort(List<Container> containerlist)
        {
            List<Container> sortedlist = containerlist.OrderByDescending(o => o.Weight).ToList();
            foreach (Container container in sortedlist)
            {
                if (container.Valuable == true)
                {
                    containerlist.Remove(container);
                    containerlist.Add(container);
                }
            }

            return sortedlist;
        }


        public bool canaddweightontop(List<Container> fieldlist)
        {
            int weightontop = 0;
            for (int i = 0; i < fieldlist.Count; i++)
            {
                if (i != 0)
                {
                    weightontop = weightontop + fieldlist[i].Weight;
                }
            }

            //check if list has more than one valluable container

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
                    if (Containerfields[i + Convert.ToInt32(Width)][Containerfields[i].Count].Valuable == true)
                    {
                        nextto = true;
                    }
                }
                catch { }
                try
                {
                    if (Containerfields[i - Convert.ToInt32(Width)][Containerfields[i].Count].Valuable == true)
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
                    if (Containerfields[i + Convert.ToInt32(Width)][Containerfields[i].Count] == null)
                    {
                        nextto = true;
                    }
                }
                catch { }
                try
                {
                    if (Containerfields[i - Convert.ToInt32(Width)][Containerfields[i].Count] == null)
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
            if (container.Coolable == true && i > Convert.ToInt32(Width))
            {
                return false;
            }
            else return true;

        }
    }
}
