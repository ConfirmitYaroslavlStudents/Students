using System;
using System.Collections.Generic;

namespace Shared
{
    [Serializable]
    public class Template
    {
        public Template(IList<string> data, string title)
        {
            Title = title;
            Data = data;
        }

        public IList<string> Data { get; private set; }
        public string Title { get; private set; }
    }
}