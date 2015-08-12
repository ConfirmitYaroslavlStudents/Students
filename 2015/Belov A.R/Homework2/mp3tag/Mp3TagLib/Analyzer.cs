using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp3TagLib
{
    public class Analyzer
    {
        private Tager _tager;
        private Func<string, bool> _filtr; 

        public Analyzer(Tager tager)
        {
            _tager = tager;
            SynchronizedFiles = new List<IMp3File>();
            NotSynchronizedFiles = new List<IMp3File>();
            ErrorFiles = new Dictionary<string, string>();
        }

        public Analyzer(Tager tager,Func<string, bool> filtr):this(tager)
        {
            _filtr = filtr;
        }

        public List<IMp3File> SynchronizedFiles { get; private set; }

        public List<IMp3File> NotSynchronizedFiles { get; private set; }

        public Dictionary<string, string> ErrorFiles { get; private set; }


        IEnumerable<string> Filtrate(IEnumerable<string> paths)
        {
            if (_filtr != null)
                return paths.Where(_filtr);
            return paths;
        }

        public void Analyze(IEnumerable<string> paths,Mask mask)
        {
            foreach (var path in Filtrate(paths))
            {
                if (_tager.Load(path))
                {
                    if (!_tager.ValidateFileName(mask))
                    {
                        NotSynchronizedFiles.Add(_tager.CurrentFile);
                    }
                    else
                    {
                        SynchronizedFiles.Add(_tager.CurrentFile);
                    }
                }
                else
                {
                    ErrorFiles.Add(path,"load error");
                }
            }
        }      
    }
}
