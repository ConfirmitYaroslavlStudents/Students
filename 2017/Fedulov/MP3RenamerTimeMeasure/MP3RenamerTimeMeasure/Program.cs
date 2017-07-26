using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3RenamerTimeMeasure
{
    class Program
    {
        static void Main(string[] args)
        {
            MP3File[] files = new MP3File[10];

            for (int i = 0; i < 10; ++i)
            {
                files[i] = new MP3File(i + ".mp3");
                files[i].FilePermitions = Permitions.Administrator;
            }

            Processor processor = new Processor();
            processor.Process(args, files);
        }
    }
}
