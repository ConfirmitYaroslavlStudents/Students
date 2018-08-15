using System;
using System.Collections.Generic;
using System.Threading;
using MenuForConsole;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainMenu = new List<MenuItem>
            {
                new MenuItem(One, "Display the standings."),
                new MenuItem(Two, "Upcoming matches."),
                new MenuItem(Three, "Start. Enter the data for the stage.")
            };

            Menu mnMenu1 = new Menu(mainMenu,"MyApp");
            mnMenu1.Start();
        }

        static void One()
        {
            Console.Beep();
            Thread.Sleep(250);
        }
        static void Two()
        {
            var sumMenu = new List<MenuItem>
            {
                new MenuItem(Alpha, "alpha menu item"),
                new MenuItem(Beta, "beta menu item"),
                new MenuItem(Gamma, "gamma menu item")
            };
            Menu mnMenu2 = new Menu(sumMenu, "MyApp/second menu item");
            mnMenu2.Start();
        }
        static void Three()
        {
            Console.Beep();
            Thread.Sleep(250);
            Console.Beep();
            Thread.Sleep(250);
            Console.Beep();
            Thread.Sleep(250);
        }

        static void Alpha()
        {
            Console.Beep(1500,500);
            Thread.Sleep(250);
        }
        static void Beta()
        {
            Console.Beep(1500, 500);
            Thread.Sleep(250);
            Console.Beep(1500, 500);
            Thread.Sleep(250);
        }
        static void Gamma()
        {
            Console.Beep(1500, 500);
            Thread.Sleep(250);
            Console.Beep(1500, 500);
            Thread.Sleep(250);
            Console.Beep(1500, 500);
            Thread.Sleep(250);
        }
    }
}
