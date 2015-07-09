using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HospitalLib.Utils.Validators;

namespace HospitalLib.Data
{
    [DataContract]
    public class Analysis
    {
        public bool New = true;
        [DataMember] private IDictionary<string, string> _data;

        public Analysis(Template template, Person person)
        {
            _data = new Dictionary<string, string>();
            Template = template;
            Person = person;
            CreationTime = DateTime.Now;
        }

        internal Analysis(Template template, Person person, int id) : this(template, person)
        {
            Id = id;
        }

        [DataMember]
        public Template Template { get; set; }

        [DataMember]
        public Person Person { get; set; }

        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public DateTime CreationTime { get; private set; }

        public string GetTemplateName()
        {
            return Template.Name;
        }

        public int GetPersonId()
        {
            return Person.Id;
        }

        public string GetData(string key)
        {
            if (!_data.ContainsKey(key))
                return null;

            return _data[key];
        }

        public void AddData(string key, string value)
        {
            Validator.ValidateAnalysisField(value);
            _data[key] = value;
        }

        public IDictionary<string, string> GetDictionary()
        {
            return _data;
        }
    }
}