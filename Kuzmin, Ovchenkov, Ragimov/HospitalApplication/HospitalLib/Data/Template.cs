using System.Runtime.Serialization;

namespace HospitalLib.Data
{
    [DataContract]
    public class Template
    {
        [DataMember] public string HtmlTemplate { get; set; }
        [DataMember] public string Name { get; private set; }

        public Template(string name, string html)
        {
            Name = name;
            HtmlTemplate = html;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
