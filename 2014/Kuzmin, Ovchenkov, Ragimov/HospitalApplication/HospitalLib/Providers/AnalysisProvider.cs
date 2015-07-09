using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using HospitalLib.Data;
using HospitalLib.DatebaseModel;
using HospitalLib.Utils;

namespace HospitalLib.Providers
{
    public class AnalysisProvider : IAnalysisProvider
    {
        private readonly DatabaseProvider _databaseProvider;

        public AnalysisProvider(DatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new ArgumentNullException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public IList<Analysis> Load(Person person)
        {
            const string query = "SELECT Analysis, AnalysisId FROM Analysis WHERE PersonId = @PersonId";
            var command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonId", person.Id);

            var reader = _databaseProvider.GetData(command);

            return GetAnalyzes(reader);
        }

        public void Save(ref Analysis analysis)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "SaveAnalysisAnGetId"
            };

            AddMainParameters(command, analysis);
            command.Parameters["@AnalysisId"].Direction = ParameterDirection.Output;

            var parameters = _databaseProvider.PushDataWithOutputParameters(command);
            var id = int.Parse(parameters["@AnalysisId"].Value.ToString());

            analysis = new Analysis(analysis.Template, analysis.Person, id);
        }

        public void Update(Analysis analysis)
        {
            const string query = "UPDATE Analysis SET Analysis= @Analysis WHERE AnalysisId = @AnalysisId";
            var command = new SqlCommand(query);

            command.Parameters.Add("@@Analysis", SqlDbType.NText);
            command.Parameters["@@Analysis"].Value = new JsonFormatter().ToJson(analysis);
            command.Parameters.AddWithValue("@AnalysisId", analysis.Id);

            _databaseProvider.PushData(command);
        }

        public void Remove(Analysis analysis)
        {
            const string query = "DELETE FROM Analysis WHERE AnalysisId=AnalysisId";
            var command = new SqlCommand(query);

            command.Parameters.AddWithValue("@AnalysisId", analysis.Id);

            _databaseProvider.PushData(command);
        }

        public void Remove(Person person)
        {
            const string query = "DELETE FROM Analysis WHERE PersonId=@PersonId";
            var command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonId", person.Id);

            _databaseProvider.PushData(command);
        }

        public void Remove(Template template)
        {
            const string query = "DELETE FROM Analysis WHERE TemplateName=@TemplateName";
            var command = new SqlCommand(query);

            command.Parameters.Add("@TemplateName", SqlDbType.NVarChar);
            command.Parameters["@TemplateName"].Value = template.Name;

            _databaseProvider.PushData(command);
        }

        public int GetCount()
        {
            const string query = "SELECT count(*) FROM Analysis";
            var result = _databaseProvider.GetDataScalar(query);
            var count = int.Parse(result.ToString(CultureInfo.InvariantCulture));

            return count;
        }

        private void AddMainParameters(SqlCommand command, Analysis analysis)
        {
            command.Parameters.Add("@PersonId", SqlDbType.Int);
            command.Parameters["@PersonId"].Value = analysis.GetPersonId();

            command.Parameters.Add("@TemplateName", SqlDbType.NVarChar);
            command.Parameters["@TemplateName"].Value = analysis.GetTemplateName();

            command.Parameters.Add("@Analysis", SqlDbType.NText);
            command.Parameters["@Analysis"].Value = new JsonFormatter().ToJson(analysis);

            command.Parameters.Add("@AnalysisId", SqlDbType.Int);
        }

        private static IList<Analysis> GetAnalyzes(SqlDataReader reader)
        {
            IList<Analysis> analyzes = new List<Analysis>();
            while (reader.Read())
            {
                var analysis = GetAnalysis(reader);
                analyzes.Add(analysis);
            }

            return analyzes;
        }

        private static Analysis GetAnalysis(SqlDataReader reader)
        {
            var jsonData = reader["Analysis"].ToString();
            var analysis = new JsonFormatter().FromJson<Analysis>(jsonData);

            return analysis;
        }
    }
}
