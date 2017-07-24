using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    class Program
    {
        static void Main(string[] args)
        {
            MP3File file = new MP3File();
            User currentUser = new User();
            IPermissionChecker checker = new PermissionChecker();
            IMP3Renamer renamer = new MP3RenamerWithTimer();

            for (int i = 0; i < 100; i++)
            {
                if(checker.CheckPermission(file,currentUser))
                {
                    renamer.Rename(file);
                }
            }
            Timer renamerTimer = renamer as Timer;
            if (renamerTimer!= null)
                Console.WriteLine("Rename Time: "+ renamerTimer.GetTime());
            Timer checkerTimer = checker as Timer;
            if (checkerTimer != null)
                Console.WriteLine("Permission check Time: " + checkerTimer.GetTime());
            Console.ReadLine();
        }
    }
}
