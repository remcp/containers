namespace containership
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool intcheck = false;
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            while (true)
            {
                while (intcheck == false)
                {
                    try
                    {
                        Console.WriteLine("boat max weight:");
                        int maxweight = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("boat length:");
                        int length = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("boat width:");
                        int width = Convert.ToInt32(Console.ReadLine());
                        Ship ship = new Ship(length, width, maxweight);
                        shiplist.Add(ship);

                        Console.WriteLine("add more boats?");
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

                        Container container = new Container(weight, valuable, coolable);
                        containerlist.Add(container);

                        Console.WriteLine("add more boats?");
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

                }


            }
        }
    }
}