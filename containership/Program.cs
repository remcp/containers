namespace containership
{
    class Program
    {
        public static void Main(string[] args)
        {
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
                try
                {
                    bool coolable = false;
                    bool valuable = false;

                    Console.WriteLine("container weight:");
                    int weight = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("coolable:");
                    string cool = Console.ReadLine();

                    if (cool == "y")
                    {
                        coolable = true;
                    }


                    Console.WriteLine("valuable:");
                    string value = Console.ReadLine();
                    if (value == "y")
                    {
                        valuable = true;
                    }
                    try
                    {
                        Container container = new Container(weight, valuable, coolable);
                        containerlist.Add(container);
                    }
                    catch (ArgumentException ex) { Console.WriteLine(ex.Message); Console.WriteLine("adding container failded"); }

                    Console.WriteLine("add another container?");
                    string loop = Console.ReadLine();
                    if (loop == "y")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        intcheck = true;
                    }
                }
                catch
                {
                    Console.WriteLine("vul een getal in");
                }
            }

            foreach (var container in containerlist)
            {
                foreach(Ship ship in shiplist)
                {
                    if(ship.addcontainer(container) == true)
                    {
                        break;
                    }
                }

                foreach (Ship ship in shiplist)
                {
                    ship.printblueprint();
                }
            }
            
        }
    }
}