using System.Runtime.Serialization;
using HospitalLib.DatebaseModel;

namespace HospitalLib.Data
{
    [DataContract]
    public class Template
    {
        [DataMember] public string HtmlTemplate { get; set; }
        [DataMember] public string Name { get; private set; }
        [DataMember] public int Id { get; private set; }

        public Template(string name, string html, INewIdProvider newIdProvider)
            : this(name, html, newIdProvider.GetTemplateId()){}

        public Template(string name, string html, int id)
        {
            Name = name;
            HtmlTemplate = html;
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
