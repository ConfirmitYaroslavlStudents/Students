using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    public class MP3File
    {
        public string FileName { get; set; }
        public int Permission { get; private set; }

        public MP3File()
        {
            FileName = "file";
            Permission = 0;
        }

        public MP3File(string fileName, int permission)
        {
            FileName = fileName;
            Permission = permission;
        }
    }
}
