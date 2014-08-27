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
         private readonly IDatabaseProvider _databaseProvider;

         public TemplateProvider(IDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new NullReferenceException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public IList<Template> Load()
        {
            const string query = "select * from Template";
            var reader = _databaseProvider.GetData(query);

            return GetTemplates(reader);
        }

        public void Save(Template template)
        {
            const string query = "insert into Template (TemplateId, Template, Name) values(@TemplateId, @Template, @Name)";
            var command = new SqlCommand(query);
            PrepareStatement(command, template);    
        }

        public void Update(Template template)
        {
            const string query = "update Template set Template=@Template, Name= @Name where TemplateId = @TemplateId";
             var command = new SqlCommand(query);
            PrepareStatement(command,template);

        }

        private void PrepareStatement(SqlCommand command, Template template)
        {
            command.Parameters.Add("@Template", SqlDbType.NText);
            command.Parameters["@Template"].Value = template.HtmlTemplate;

            command.Parameters.AddWithValue("@TemplateId", template.Id);
            command.Parameters.AddWithValue("@Name", template.Name);

            _databaseProvider.PushData(command);
        }

        public void Remove(Template template)
        {
            new AnalysisProvider(_databaseProvider).Remove(template);

            var query = string.Format("delete from Template where TemplateId='{0}'", template.Id);
            _databaseProvider.PushData(query);
        }

        public IList<Template> Search(string tempalteName)
        {
            const string query = "select * from Template where Name like @Name"; 

            var command = new SqlCommand(query);

            command.Parameters.Add("@Name", SqlDbType.NVarChar);
            command.Parameters["@Name"].Value = "%" + tempalteName + "%";

            var reader = _databaseProvider.GetData(command);

            return GetTemplates(reader);
        }

        public int GetCount()
        {
            const string query = "select count(*) from Template";
            var result = _databaseProvider.GetDataScalar(query);
            var count = int.Parse(result.ToString(CultureInfo.InvariantCulture));

            return count;
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

        public  Template GetTemplate(SqlDataReader reader)
        {
             var htmlTemplate = reader["Template"].ToString();
           
            var name = reader["Name"].ToString();
            var id = int.Parse(reader["TemplateId"].ToString());

            return new Template(name, htmlTemplate, id);
        }
    }
}
