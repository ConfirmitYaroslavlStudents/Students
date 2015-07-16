using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MP3_tager
{
    static class CodeBehind
    {
        public static void TagFile(string path, string pattern)
        {
            var tagDictionary = GetTags(path, pattern);


        }

        private static Dictionary<TagTypes,string> GetTags(string path, string pattern)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(Messeges.FileNotExist);
                Environment.Exit(1);
            }

            throw new NotImplementedException();
        }

        private static void InsertTo(string path, Dictionary<TagTypes,String> tags)
        {
            throw new NotImplementedException();
        }
    }
}
