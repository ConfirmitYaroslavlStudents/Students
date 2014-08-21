using System;
using System.Collections.Generic;

namespace HospitalLib.Template
{
    public class Template
    {
        public IEnumerable<Line> Lines { get; set; }
        public string TemplateType { get; private set; }

        public Template(IEnumerable<Line> lines, string type)
        {
            Lines = lines;
            TemplateType = type.Replace(".txt", "");
        }

        public override string ToString()
        {
            var templatestring = "";
            foreach (var line in Lines)
            {
                templatestring += String.Format("<Line Height:{0};  Width:{1};>", line.Height, line.Width);
                foreach (var element in line)
                {
                    templatestring += String.Format("<{0} Height:{1};  Width:{2};>", element.Type, element.Height, element.Width);
                    templatestring += element.Text;
                    templatestring += String.Format("</{0}>", element.Type);
                }
                templatestring += "</Line>";
            }
            return templatestring;
        }
    }
}
