namespace containership
{
    class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();
            bool intcheck = false;
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            Ship setship = new Ship(0, 0);

            while (intcheck == false)
            {
                try
                {

                    Console.WriteLine("boat length:");
                    int length = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("boat width:");
                    int width = Convert.ToInt32(Console.ReadLine());
                    setship = new Ship(length, width);
                    shiplist.Add(setship);
                    intcheck = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("vul een getal in");
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
                //for (int i = 0; i < 10; i++)
                //{
                //    Container container = new Container(30000, true, false);
                //    containerlist.Add(container);
                //}
                for (int i = 0; i < 10; i++)
                {
                    Container container = new Container(30000, false, true);
                    containerlist.Add(container);
                }
                intcheck = true;
            }

            containerlist = containerlist.OrderByDescending(a => a.Coolable).ThenBy(a => a.Valuable).ToList();
            foreach (Container container in containerlist)
            {
                foreach (Ship ship in shiplist)
                {
                    if (ship.addcontainer(container) == true)
                    {
                        break;
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
        //}

    }
}