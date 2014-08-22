using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using DictionaryFormatter;
using Shared;
using System.Configuration;

namespace DataStorageLibrary
{
    public class DataStorage : IDataStorage
    {
        private readonly SqlCeConnection _connection;
        private readonly SqlCeCommand _command;

        public DataStorage()
        {
            _connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            _command = _connection.CreateCommand();
        }

        public List<Person> GetPersons(string fieldName, string fieldValue)
        {
            _connection.Open();
            _command.CommandText = string.Format("SELECT * FROM Persons WHERE ({0} LIKE ?)", fieldName);
            _command.Parameters.AddWithValue(fieldName, string.Format("%{0}%", fieldValue));
            var dataReader = _command.ExecuteReader();
            var persons = new List<Person>();

            while (dataReader.Read())
            {
                string firstName = dataReader["FirstName"].ToString();
                string lastName = dataReader["LastName"].ToString();
                var dateOfBirth = (DateTime)dataReader["DateOfBirth"];
                string address = dataReader["Address"].ToString();
                string policyNumber = dataReader["PolicyNumber"].ToString();

                persons.Add(new Person(firstName, lastName, dateOfBirth, address, policyNumber));
            }
  
            CloseConnection();
            return persons;
        }

        private void CloseConnection()
        {
            _connection.Close();
            _command.Parameters.Clear();
        }

        public DataSet GetPersonsInDataSet(string fieldName, string fieldValue)
        {
            _connection.Open();
            string query = string.Format("SELECT * FROM Persons WHERE ({0} LIKE %{1}%)", fieldName, fieldValue);
            var adapter = new SqlCeDataAdapter(query, _connection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "PersonsDataBinding");
            
            _connection.Close();
            return dataSet;
        }

        public Template GetTemplate(string title)
        {
            _connection.Open();
            _command.CommandText = "SELECT * FROM Templates WHERE Title = ?";
            _command.Parameters.AddWithValue("Title", title);
            var dataReader = _command.ExecuteReader();
            Template template = null;
            var xmlFormatter = new XmlFormatter<string, string>();

            while (dataReader.Read())
            {
                template = new Template(xmlFormatter.Deserialize(dataReader["Data"].ToString()), title);
            }

            CloseConnection();
            return template;
        }

        public List<Template> GetTemplates()
        {
            _connection.Open();
            _command.CommandText = "SELECT * FROM Templates";
            var dataReader = _command.ExecuteReader();
            var templates = new List<Template>();
            var xmlFormatter = new XmlFormatter<string, string>();

            while (dataReader.Read())
            {
                string title = dataReader["Title"].ToString();
                var data = xmlFormatter.Deserialize(dataReader["Data"].ToString());
                templates.Add(new Template(data, title));
            }

            CloseConnection();
            return templates;
        }

        public bool AddPerson(Person person)
        {
            _connection.Open();
            _command.CommandText = "INSERT INTO Persons(FirstName, LastName, DateOfBirth, Address, PolicyNumber) VALUES(?, ?, ?, ?, ?)";

            _command.Parameters.AddWithValue("FirstName", person.FirstName);
            _command.Parameters.AddWithValue("LastName", person.LastName);
            _command.Parameters.AddWithValue("DateOfBirth", person.DateOfBirth);
            _command.Parameters.AddWithValue("Address", person.Address);
            _command.Parameters.AddWithValue("PolicyNumber", person.PolicyNumber);

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return true;
        }

        public bool AddTemplate(Template template)
        {
            _connection.Open();
            var xmlFormatter = new XmlFormatter<string, string>();

            _command.CommandText = "INSERT INTO Templates(Title, Data) VALUES(?, ?)";
            _command.Parameters.AddWithValue("Title", template.Title);
            _command.Parameters.AddWithValue("Data", xmlFormatter.Serialize(template.Data));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return true;
        }

        public void AddAnalysis(string policyNumber, Analysis analysis)
        {
            _connection.Open();
            var xmlFormatter = new XmlFormatter<string, string>();
            _command.CommandText = "INSERT INTO Analyzes(PolicyNumber, TemplateTitle, Data, Date) VALUES(?, ?, ?, ?)";
            
            _command.Parameters.AddWithValue("PolicyNumber", policyNumber);
            _command.Parameters.AddWithValue("TemplateTitle", analysis.TemplateTitle);
            _command.Parameters.AddWithValue("Data", xmlFormatter.Serialize(analysis.Data));
            _command.Parameters.AddWithValue("Date", analysis.Date);
            _command.ExecuteNonQuery();

            CloseConnection();
        }

        public List<Analysis> GetAnalyzes(string policyNumber)
        {
            _connection.Open();
            _command.CommandText = "SELECT TemplateTitle, Data, Date FROM Analyzes WHERE PolicyNumber = ?";
            _command.Parameters.AddWithValue("PolicyNumber", policyNumber);
            var dataReader = _command.ExecuteReader();
            var analyzes = new List<Analysis>();
            var xmlFormatter = new XmlFormatter<string, string>();

            while (dataReader.Read())
            {
                string data = dataReader["Data"].ToString();
                string title = dataReader["TemplateTitle"].ToString();
                var date = (DateTime)dataReader["Date"];
                analyzes.Add(new Analysis(new Template(xmlFormatter.Deserialize(data), title), date));
            }

            CloseConnection();
            return analyzes;
        }
    }
}
