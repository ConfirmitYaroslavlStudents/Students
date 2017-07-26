using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3RenamerLib
{
    public class Arguments
    {
        public bool IsNormalExecution { set; get; }
        public bool IsCheckPermitions { set; get; }
        public bool IsTimeMeasure { set; get; }
    }

    public class ArgumentsParser
    {
        public Arguments ParseArguments(string[] args)
        {
            Arguments arguments = new Arguments();
            foreach (string t in args)
            {
                if (Equals(t, "-normal"))
                    arguments.IsNormalExecution = true;
                else if(Equals(t, "-timer"))
                    arguments.IsTimeMeasure = true;
                else if (Equals(t, "-permitions"))
                    arguments.IsCheckPermitions = true;
                else
                    throw new ArgumentException("Unknown argument!");
            }

            return arguments;
        }
    }
}
