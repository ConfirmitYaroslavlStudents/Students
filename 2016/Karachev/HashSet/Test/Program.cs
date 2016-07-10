using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySet=HashSet;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var set=new MySet.HashSet<int>();
            var micrset=new HashSet<int>();
            
            var watch = new Stopwatch();
            var rnd = new Random();

            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                set.Add(rnd.Next(0, 100000));
            }


            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            watch.Reset();

            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                micrset.Add(rnd.Next(50000, 150000));
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            watch.Reset();
            watch.Start();
           micrset.SymmetricExceptWith(set);
            watch.Stop();
            Console.WriteLine("\n\n"+watch.Elapsed+"\n\n");
            watch.Reset();
            var words = GetWords();

            watch.Restart();
            var my = GetTable(words);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            watch.Restart();
            
            var notmy = GetDictionary(words);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            Console.WriteLine();
            var notnot=new HashSet<int>();
            Console.ReadKey();
        }

        public static char[] GetChars()
        {
            var list = new List<char>();
            for (char ch = char.MinValue; ch < 0x3000; ch++)
            {
                if (char.IsPunctuation(ch)) list.Add(ch);

            }
            list.Add(' ');
            return list.ToArray();
        }



        public static MySet.HashSet<string> GetTable(string[] words)
        {
            var table = new HashSet.HashSet<string>();
            foreach (var word in words)
            {
                    table.Add(word);
                
            }
            return table;
        }

        public static HashSet<string> GetDictionary(string[] words)
        {
            var dict = new HashSet<string>();

            foreach (var word in words)
            {

                    dict.Add(word);
                
            }

            return dict;
        }

        public static string[] GetWords()
        {
            var list = new List<string>();
            var delimited = GetChars();

            using (var sr = new StreamReader("war.txt", Encoding.Default))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var words = line.Split(delimited, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        list.Add(word);
                    }
                    line = sr.ReadLine();
                }
            }
            return list.ToArray();
        }

    }
}
