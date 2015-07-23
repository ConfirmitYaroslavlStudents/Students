using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;

namespace Mp3TagTest
{
    internal class TestMp3File : IMp3File
    {
        public Mp3Tags Tags { get; set; }
        public string Path { get; set; }
        public bool SaveFlag { get; set; }

        public string Name
        {
            get { return Path; }
        }

        public void SetTags(Mp3Tags tags)
        {
            Tags = tags;
        }

        public void Save()
        {
            SaveFlag = true;
        }
    }
}
