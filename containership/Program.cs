﻿using System.ComponentModel;

namespace containership
{
    class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();
            int width = 0;
            bool intcheck = false;
            
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            List<Container> couldnotaddlist = new List<Container>();
            List<Container> valuablecontainerlist = new List<Container>();
            Ship setship = new Ship(0, 0);

            while (intcheck == false)
            {
                try
                {

                    Console.WriteLine("boat length:");
                    int length = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("boat width:");
                    width = Convert.ToInt32(Console.ReadLine());
                    setship = new Ship(length, width);
                    shiplist.Add(setship);
                    if(length < 3 | width < 3)
                    {
                        throw new Exception();
                    }
                    intcheck = true;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("vul een getal in, een boot is minimaal 3 bij 3");

                    Console.WriteLine();
                }
            }

            intcheck = false;

            while (intcheck == false)
            {
                for (int i = 0; i < 10; i++)
                {
                    Container container = new Container(30000, false, false);
                    containerlist.Add(container);
                }
                for (int i = 0; i < 6; i++)
                {
                    Container container = new Container(10000, false, false);
                    containerlist.Add(container);
                }
                for (int i = 0; i < 1; i++)
                {
                    Container container = new Container(20000, false, false);
                    containerlist.Add(container);
                }
                for (int i = 0; i < 10; i++)
                {
                    Container container = new Container(30000, true, false);
                    containerlist.Add(container);
                }
                for (int i = 0; i < 16; i++)
                {
                    Container container = new Container(30000, false, true);
                    containerlist.Add(container);
                }
                intcheck = true;
            }

            int waittime = 5;
            int checkwaittime = 0;
            while (containerlist.Count > 0)
            {
                containerlist = containerlist.OrderByDescending(a => a.Coolable).ThenByDescending(a => a.Weight).ThenBy(a => a.Valuable).ToList();

                int shipcount = 0;
                checkwaittime++;
                if (checkwaittime > waittime)
                {
                    break;
                }
                
                for (int i = 0; i < containerlist.Count + 1; i++)
                {
                    for (int j = 0; j < shiplist.Count + 1; j++)
                    {
                       
                        try
                        {
                            if (shiplist[j].addcontainer(containerlist[0]) == true)
                            {
                                containerlist.Remove(containerlist[0]);
                                break;
                            }
                            else if(shipcount > shiplist.Count)
                            {

                                couldnotaddlist.Add(containerlist[0]);
                                containerlist.Remove(containerlist[0]);

                            }
                        }
                        catch(ArgumentException)
                        {
                            try
                            {
                                if (shiplist[j].addtwocontainers(containerlist[0], containerlist[1]))
                                {
                                    containerlist.Remove(containerlist[0]);
                                    containerlist.Remove(containerlist[1]);
                                    break;
                                }
                            }
                            catch { }
                        }
                        shipcount++;
                    }
                }
            }
            foreach (Ship ship in shiplist)
            {
                ship.printblueprint();
                Console.WriteLine();
                ship.printlists();
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("overgebleven containers");
            Console.WriteLine();

            foreach(Container container in couldnotaddlist)
            {
                containerlist.Add(container);
            }
            foreach (Container container in containerlist)
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

        //public bool addcontainer()
        //{
        //    bool intcheck = false;
        //    bool coolable = false;
        //    bool valuable = false;

        //    Console.WriteLine("container weight:");
        //    int weight = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("coolable:");
        //    string cool = Console.ReadLine();

        //    if (cool == "y")
        //    {
        //        coolable = true;
        //    }


        //    Console.WriteLine("valuable:");
        //    string value = Console.ReadLine();
        //    if (value == "y")
        //    {
        //        valuable = true;
        //    }
        //    try
        //    {
        //        Container container = new Container(weight, valuable, coolable);
        //        containerlist.Add(container);
        //    }
        //    catch (ArgumentException ex) { Console.WriteLine(ex.Message); Console.WriteLine("adding container failded"); }

        //    Console.WriteLine("add another container?");
        //    string loop = Console.ReadLine();
        //    if (loop == "y")
        //    {
        //        Console.Clear();
        //    }
        //    else
        //    {
        //        intcheck = true;
        //    }
        //    return intcheck;
        //
    }
}