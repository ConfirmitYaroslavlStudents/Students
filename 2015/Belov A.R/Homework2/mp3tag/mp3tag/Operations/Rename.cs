using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;

namespace mp3tager
{
    class Rename:Operation
    {
        public override void Call()
        {
            var tager = new Tager(new FileLoader());
            //tager.Load(@"C:\Users\Alexandr\Desktop\TEST\1 Holiday.mp3");
            if (!tager.Load(Menu.GetUserInput("path:")))
            {
                throw new FileNotFoundException("File does not exist");
            }
            Menu.PrintHelp();
            tager.ChangeName(new Mask(Menu.GetUserInput("mask:")));
            tager.Save();
            Menu.PrintSuccessMessage();
        }
    }
}
