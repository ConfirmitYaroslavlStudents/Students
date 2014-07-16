using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest;
using MenuForConsole;

namespace Wooden_application_console_
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://habrahabr.ru/post/144850/

            Console.Title = "Bypass binary trees";
            var birch = new Tree<int>(new int[] { 50, 25, 75, 12, 38, 70, 80, 6, 13, 37, 39, 67, 71, 79, 81 });

            var actionForMenu = new Action[4];
            actionForMenu[0] = new Action(birch.Horizontal);
            actionForMenu[1] = new Action(birch.Prefix);
            actionForMenu[2] = new Action(birch.Infix);
            actionForMenu[3] = new Action(birch.Postfix);
            var arrayOfMenu = new[] { "horizontal (wide)",
                "vertical (in depth) line (prefix, pre-ordered)",
                "vertical (in depth) reverse (infix, in-ordered)",
                "vertical (in depth) terminal (postfix, post-ordered)" };
            var m = new Menu(arrayOfMenu, actionForMenu);
            m.UseIt();
        }
    }
}
