using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using HospitalLib.Data;
using HospitalLib.DatebaseModel;

namespace HospitalLib.Providers
{
    public class AnalysisProvider : IAnalysisProvider
    {
        private readonly IDatabaseProvider _databaseProvider;

        public AnalysisProvider(IDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new NullReferenceException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public IList<Analysis> Load(Person person)
        {
            const string query = "select Analysis, AnalysisId from Analysis  where PersonId=@PersonId";
            var command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PersonId", person.Id);
            _databaseProvider.GetData(command);

            var reader = _databaseProvider.GetData(command);
            IList<Analysis> analyzes = new List<Analysis>();
            while (reader.Read())
            {
                var analysis = GetAnalysis(reader);
                analyzes.Add(analysis);
            }

            return analyzes;
        }

        public void Save(Analysis analysis)
        {
            const string query = "insert into Analysis (AnalysisId, Analysis, TemplateId, PersonId) values(@AnalysisId, @Analysis, @TemplateId, @PersonId)";

            var command = new SqlCommand(query);
            command.Parameters.Add("@Analysis", SqlDbType.NText);
            command.Parameters["@Analysis"].Value = analysis.ToJson(); 

            command.Parameters.AddWithValue("@PersonId", analysis.GetPersonId());
            command.Parameters.AddWithValue("@TemplateId", analysis.GetTemplateId());
            command.Parameters.AddWithValue("@AnalysisId", analysis.Id);

            _databaseProvider.PushData(command);
        }

        public void Update(Analysis analysis)
        {
            const string query = "update Analysis set Analysis= @Analysis, PersonId=@PersonId, TemplateId=@TemplateId where AnalysisId = @AnalysisId";

            var command = new SqlCommand(query);

            command.Parameters.Add("@Analysis", SqlDbType.NText);
            command.Parameters["@Analysis"].Value = analysis.ToJson();

            command.Parameters.AddWithValue("@PersonId", analysis.GetPersonId());
            command.Parameters.AddWithValue("@TemplateId", analysis.GetTemplateId());
            command.Parameters.AddWithValue("@AnalysisId", analysis.Id);

            _databaseProvider.PushData(command);
        }

        public void Remove(Analysis analysis)
        {
            var query = string.Format("delete from Analysis where AnalysisId='{0}'", analysis.Id);
            _databaseProvider.PushData(query);
        }

        public void Remove(Person person)
        {
            var query = string.Format("delete from Analysis where PersonId='{0}'", person.Id);
            _databaseProvider.PushData(query);
        }

        public void Remove(Template template)
        {
            var query = string.Format("delete from Analysis where TemplateId='{0}'", template.Id);
            _databaseProvider.PushData(query);
        }

        public int GetCount()
        {
            const string query = "select count(*) from Analysis";
            var result = _databaseProvider.GetDataScalar(query);
            var count = int.Parse(result.ToString(CultureInfo.InvariantCulture));

            return count;
        }

        private static Analysis GetAnalysis(SqlDataReader reader)
        {
            var jsonData = reader["Analysis"].ToString();
            var analysis = Analysis.FromJson(jsonData);

            return analysis;
        }
    }
}
