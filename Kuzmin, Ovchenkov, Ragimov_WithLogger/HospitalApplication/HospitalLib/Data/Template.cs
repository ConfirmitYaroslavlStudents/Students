using System.Runtime.Serialization;
using HospitalLib.Utils.Validators;

namespace HospitalLib.Data
{
    [DataContract]
    public class Template
    {
        [DataMember]
        private string _name;

        [DataMember]
        public string HtmlTemplate { get; set; }
        [DataMember]
        public string Name
        {
            get { return _name; }
            private set
            {
                Validator.ValidateTitleField(value);
                _name = value;
            }
        }

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
