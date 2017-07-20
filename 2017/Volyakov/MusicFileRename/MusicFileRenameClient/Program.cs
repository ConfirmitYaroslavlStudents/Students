using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicFileRenameLibrary;

namespace MusicFileRename
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicRenamerCaller caller = new MusicRenamerCaller();
            try
            {
                caller.CallRenamer(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
