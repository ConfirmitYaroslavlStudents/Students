using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Logger;
using RenamerLib;

namespace MP3Renamer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProcessExecutor processExecutor = new ProcessExecutor();
            processExecutor.Execute(args);
        }
    }
}
