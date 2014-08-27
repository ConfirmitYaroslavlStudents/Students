using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using HospitalLib.Providers;

namespace HospitalLib.Data
{
    [DataContract]
    public class Analysis
    {
        [DataMember] private IDictionary<string, string> _data;
        [DataMember] public Template Template { get; set; }
        [DataMember] public Person Person { get; set; }
        [DataMember] public int Id { get; private set; }
        [DataMember] public DateTime CreationTime { get; private set; }
        public bool New = true;

        public Analysis(Template template, Person person, NewIdProvider provider)
        {
            if (provider == null)
                throw new NullReferenceException("databaseProvider");

            _data = new Dictionary<string, string>();
            Template = template;
            Person = person;
            Id = provider.GetAnalysisId();
            CreationTime = DateTime.Now;
        }

        public int GetTemplateId()
        {
            return Template.Id;
        }

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

        public IDictionary<string, string> GetDictionary()
        {
            return _data;
        }

        public void AddData(string key, string value)
        {
            _data[key] = value;
        }

        public string ToJson()
        {
            var serializer = new DataContractJsonSerializer(typeof(Analysis));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, this);

            stream.Position = 0;
            var jsonResult = new StreamReader(stream).ReadToEnd();

            return jsonResult;
        }

        public static Analysis FromJson(string json)
        {
            var byteArray = Encoding.UTF8.GetBytes(json);
            var serializer = new DataContractJsonSerializer(typeof(Analysis));
            var stream = new MemoryStream(byteArray);
            var result = (Analysis)serializer.ReadObject(stream);

            return result;
        }
    }
}
