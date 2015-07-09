using System;
using System.Collections.Generic;
using LogService;

namespace Shared
{
    [Serializable]
    public class Template
    {
        public Template(IList<string> data, string title)
        {
            Title = title;
            Data = data;
            //LOGGING
            Logger.Info("Template was created");
        }

        public IList<string> Data { get; private set; }
        public string Title { get; private set; }
    }
}