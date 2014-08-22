using System.Collections.Generic;

namespace Shared
{
    public class Template
    {
        public Dictionary<string, string> Data { get; private set; }
        public string Title { get; private set; }

        public Template(Dictionary<string, string> data, string title)
        {
            Title = title;
            Data = data;
        }
    }
}
