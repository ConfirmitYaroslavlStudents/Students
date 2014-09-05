using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using HospitalLib.Data;
using HospitalLib.DatebaseModel;

namespace HospitalLib.Providers
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly DatabaseProvider _databaseProvider;

        public TemplateProvider(DatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new ArgumentNullException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public IList<Template> Load()
        {
            const string query = "SELECT * FROM Template";
            var command = new SqlCommand(query);
            SqlDataReader reader = _databaseProvider.GetData(command);

            return GetTemplates(reader);
        }

        public void Save(Template template)
        {
            const string query = "INSERT INTO Template (Template, Name) VALUES(@Template, @Name)";
            var command = new SqlCommand(query);

            PrepareStatement(command, template);
        }

        public void Update(Template template)
        {
            const string query = "UPDATE Template SET Template=@Template WHERE Name=@Name";
            var command = new SqlCommand(query);

            PrepareStatement(command, template);
        }


        public void Remove(Template template)
        {
            new AnalysisProvider(_databaseProvider).Remove(template);

            const string query = "DELETE FROM Template WHERE Name=@Name";
            var command = new SqlCommand(query);

            command.Parameters.Add("@Name", SqlDbType.NVarChar);
            command.Parameters["@Name"].Value = template.Name;

            _databaseProvider.PushData(command);
        }

        public IList<Template> Search(string tempalteName)
        {
            const string query = "SELECT * FROM Template WHERE Name LIKE @Name";
            var command = new SqlCommand(query);

            command.Parameters.Add("@Name", SqlDbType.NVarChar);
            command.Parameters["@Name"].Value = "%" + tempalteName + "%";

            SqlDataReader reader = _databaseProvider.GetData(command);

            return GetTemplates(reader);
        }

        public int GetCount()
        {
            const string query = "SELECT count(*) FROM Template";
            int result = _databaseProvider.GetDataScalar(query);
            int count = int.Parse(result.ToString(CultureInfo.InvariantCulture));

            return count;
        }

        private void PrepareStatement(SqlCommand command, Template template)
        {
            command.Parameters.Add("@Template", SqlDbType.NText);
            command.Parameters["@Template"].Value = template.HtmlTemplate;

            command.Parameters.Add("@Name", SqlDbType.NVarChar);
            command.Parameters["@Name"].Value = template.Name;

            _databaseProvider.PushData(command);
        }

        private IList<Template> GetTemplates(SqlDataReader reader)
        {
            IList<Template> templates = new List<Template>();
            while (reader.Read())
            {
                templates.Add(GetTemplate(reader));
            }

            return templates;
        }

        public Template GetTemplate(SqlDataReader reader)
        {
            string htmlTemplate = reader["Template"].ToString();
            string name = reader["Name"].ToString();

            return new Template(name, htmlTemplate);
        }
    }
}