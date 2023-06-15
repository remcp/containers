using Microsoft.VisualStudio.TestTools.UnitTesting;
using containership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace containership.Tests
{
    [TestClass()]
    public class ShipTests
    {
        [TestMethod()]
        public void addcontainerTest()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);
            int checkwaittime = 0;
            int waittime = 100;

            for (int i = 0; i < 20; i++)
            {
                Container container = new Container(30000, false, false);
                containerlist.Add(container);
            }
            for (int i = 0; i < 10; i++)
            {
                Container container = new Container(30000, false, true);
                containerlist.Add(container);
            }
            //act
            while (containerlist.Count > 0)
            {
                checkwaittime++;
                if (checkwaittime > waittime)
                {
                    break;
                }
                containerlist = containerlist.OrderByDescending(a => a.Coolable).ThenByDescending(a => a.Weight).ThenBy(a => a.Valuable).ToList();
                for (int i = 0; i < containerlist.Count; i++)
                {
                    foreach (Ship ship in shiplist)
                    {
                        try
                        {
                            if (ship.addcontainer(containerlist[0]) == true)
                            {
                                containerlist.Remove(containerlist[0]);
                                break;
                            }
                        }
                        catch (ArgumentException)
                        {
                            try
                            {
                                if (ship.addtwocontainers(containerlist[0], containerlist[1]))
                                {
                                    containerlist.Remove(containerlist[0]);
                                    containerlist.Remove(containerlist[1]);
                                    break;
                                }
                            }
                            catch { }
                        }


                    }
                }
            }
            //assert

            Assert.AreEqual(containerlist.Count, 0);
        }


        [TestMethod()]
        public void placecontainer()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);
            int checkwaittime = 0;
            int waittime = 100;

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, false);
                containerlist.Add(container);
            }
            //act
            while (containerlist.Count > 0)
            {
                checkwaittime++;
                if (checkwaittime > waittime)
                {
                    break;
                }
                containerlist = containerlist.OrderByDescending(a => a.Coolable).ThenByDescending(a => a.Weight).ThenBy(a => a.Valuable).ToList();
                for (int i = 0; i < containerlist.Count; i++)
                {
                    foreach (Ship ship in shiplist)
                    {
                        try
                        {
                            if (ship.addcontainer(containerlist[0]) == true)
                            {
                                containerlist.Remove(containerlist[0]);
                                break;
                            }
                        }
                        catch (ArgumentException)
                        {
                            try
                            {
                                if (ship.addtwocontainers(containerlist[0], containerlist[1]))
                                {
                                    containerlist.Remove(containerlist[0]);
                                    containerlist.Remove(containerlist[1]);
                                    break;
                                }
                            }
                            catch { }
                        }


                    }
                }
            }
            //assert

            Assert.AreEqual(containerlist.Count, 0);
        }

        [TestMethod()]
        public void exceedleftyes()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, false);
                shiplist[0].Containerfields[0].Add(container);
                shiplist[0].LeftWeight = 30000;
            }

            //act
            bool exceed = shiplist[0].exceedleft(30000);

            //assert
            Assert.AreEqual(exceed, true);
        }

        [TestMethod()]
        public void exceedrightyes()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, false);
                shiplist[0].Containerfields[2].Add(container);
                shiplist[0].RightWeight = 30000;
            }

            //act
            bool exceed = shiplist[0].exceedright(30000);

            //assert
            Assert.AreEqual(exceed, true);
        }

        [TestMethod()]
        public void exceedleftno()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, false);
                shiplist[0].Containerfields[2].Add(container);
                shiplist[0].RightWeight = 30000;
            }

            //act
            bool exceed = shiplist[0].exceedleft(30000);

            //assert
            Assert.AreEqual(exceed, false);
        }
        [TestMethod()]
        public void exceedrightno()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, false);
                shiplist[0].Containerfields[0].Add(container);
                shiplist[0].LeftWeight = 30000;
            }

            //act
            bool exceed = shiplist[0].exceedright(30000);

            //assert
            Assert.AreEqual(exceed, false);
        }


        [TestMethod()]
        public void canaddweightontopyes()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 4; i++)
            {
                Container container = new Container(30000, false, false);
                shiplist[0].Containerfields[0].Add(container);
                shiplist[0].LeftWeight = 30000;
            }

            //act
            bool assert = shiplist[0].canaddweightontop(shiplist[0].Containerfields[0]);

            //assert
            Assert.AreEqual(assert, true);
        }

        [TestMethod()]
        public void canaddweightontopno()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 6; i++)
            {
                Container container = new Container(30000, false, false);
                shiplist[0].Containerfields[0].Add(container);
                shiplist[0].LeftWeight = 30000;
            }

            //act
            bool assert = shiplist[0].canaddweightontop(shiplist[0].Containerfields[0]);

            //assert
            Assert.AreEqual(assert, false);
        }

        [TestMethod()]
        public void coolablecheckyes()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, true);
                containerlist.Add(container);
            }

            //act
            bool assert = shiplist[0].coolablecheck(containerlist[0], 0);

            //assert
            Assert.AreEqual(assert, true);
        }

        [TestMethod()]
        public void coolablecheckno()
        {
            //arrange
            List<Ship> shiplist = new List<Ship>();
            List<Container> containerlist = new List<Container>();
            List<Container> newcontainerlist = new List<Container>();
            Ship setship = new Ship(4, 4);
            shiplist.Add(setship);

            for (int i = 0; i < 1; i++)
            {
                Container container = new Container(30000, false, true);
                containerlist.Add(container);
            }

            //act
            bool assert = shiplist[0].coolablecheck(containerlist[0], shiplist[0].Width + 1);

            //assert
            Assert.AreEqual(assert, false);
        }
    }
}