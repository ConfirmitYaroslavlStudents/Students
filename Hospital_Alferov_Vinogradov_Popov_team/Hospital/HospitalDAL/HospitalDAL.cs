using System;
using System.Collections.Generic;
using System.Data.Common;
using ListFormatter;
using Shared;
using Shared.Interfaces;

namespace HospitalConnectedLayer
{
    public class HospitalDAL : IHospitalDataAccessLayer
    {
        private DbCommand _command;
        private DbProviderFactory _dbProviderFactory;
        private DbConnection _sqlConnection;

        #region Open / Close methods

        public void OpenConnection(string dataProvider, string connectionString)
        {
            _dbProviderFactory = DbProviderFactories.GetFactory(dataProvider);
            _sqlConnection = _dbProviderFactory.CreateConnection();
            _command = _dbProviderFactory.CreateCommand();
            _command.Connection = _sqlConnection;
            _sqlConnection.ConnectionString = connectionString;
            _sqlConnection.Open();
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

        #endregion

        #region Person

        public List<Person> GetPersons(string firstName, string lastName, string policyNumber)
        {
            _command.CommandText =
                string.Format(
                    "SELECT * FROM Persons WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName AND PolicyNumber LIKE @PolicyNumber");

            _command.Parameters.Add(GetParam("@FirstName", string.Format("%{0}%", firstName)));
            _command.Parameters.Add(GetParam("@LastName", string.Format("%{0}%", lastName)));
            _command.Parameters.Add(GetParam("@PolicyNumber", string.Format("%{0}%", policyNumber)));

            var persons = new List<Person>();

            using (DbDataReader dataReader = _command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    persons.Add(new Person(
                        (string) dataReader["FirstName"],
                        (string) dataReader["LastName"],
                        (DateTime) dataReader["DateOfBirth"],
                        (string) dataReader["Address"],
                        (string) dataReader["PolicyNumber"]));
                }
            }

            _command.Parameters.Clear();
            return persons;
        }

        public bool AddPerson(Person person)
        {
            _command.CommandText =
                "INSERT INTO Persons(FirstName, LastName, DateOfBirth, Address, PolicyNumber) VALUES(@FirstName, @LastName, @DateOfBirth, @Address, @PolicyNumber)";

            _command.Parameters.Add(GetParam("@FirstName", person.FirstName));
            _command.Parameters.Add(GetParam("@LastName", person.LastName));
            _command.Parameters.Add(GetParam("@Address", person.Address));
            _command.Parameters.Add(GetParam("@DateOfBirth", person.DateOfBirth));
            _command.Parameters.Add(GetParam("@PolicyNumber", person.PolicyNumber));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
            }

            return true;
        }

        #endregion

        #region Template

        public Template GetTemplate(string title)
        {
            _command.CommandText = "SELECT * FROM Templates WHERE Title = @Title";

            _command.Parameters.Add(GetParam("@Title", title));

            Template template = null;
            var binFormatter = new ListBinaryFormatter<string>();

            using (DbDataReader dataReader = _command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    template = new Template(binFormatter.Deserialize((byte[]) dataReader["Data"]), title);
                }
            }

            _command.Parameters.Clear();
            return template;
        }

        public bool AddTemplate(Template template)
        {
            var binFormatter = new ListBinaryFormatter<string>();

            _command.CommandText = "INSERT INTO Templates(Title, Data) VALUES(@Title, @Data)";

            _command.Parameters.Add(GetParam("@Title", template.Title));
            _command.Parameters.Add(GetParam("@Data", binFormatter.Serialize(template.Data)));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
            }

            return true;
        }

        public List<Template> GetTemplates()
        {
            _command.CommandText = "SELECT * FROM Templates";
            var templates = new List<Template>();
            var binFormatter = new ListBinaryFormatter<string>();

            using (DbDataReader dataReader = _command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var title = (string) dataReader["Title"];
                    IList<string> data = binFormatter.Deserialize((byte[]) dataReader["Data"]);
                    templates.Add(new Template(data, title));
                }
            }

            _command.Parameters.Clear();
            return templates;
        }

        #endregion

        #region Analysis

        public void AddAnalysis(string policyNumber, Analysis analysis)
        {
            var binFormatter = new ListBinaryFormatter<string>();
            _command.CommandText =
                "INSERT INTO Analyzes(PolicyNumber, TemplateTitle, Data, Date) VALUES(@PolicyNumber, @TemplateTitle, @Data, @Date)";

            _command.Parameters.Add(GetParam("@TemplateTitle", analysis.TemplateTitle));
            _command.Parameters.Add(GetParam("@Data", binFormatter.Serialize(analysis.Data)));
            _command.Parameters.Add(GetParam("@Date", analysis.Date));
            _command.Parameters.Add(GetParam("@PolicyNumber", policyNumber));
            _command.ExecuteNonQuery();

            _command.Parameters.Clear();
        }

        public List<Analysis> GetAnalyzes(string policyNumber)
        {
            _command.CommandText = "SELECT TemplateTitle, Data, Date FROM Analyzes WHERE PolicyNumber = @PolicyNumber";

            _command.Parameters.Add(GetParam("@PolicyNumber", policyNumber));

            var analyzes = new List<Analysis>();
            var binFormatter = new ListBinaryFormatter<string>();

            using (DbDataReader dataReader = _command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var data = (byte[]) dataReader["Data"];
                    var title = (string) dataReader["TemplateTitle"];
                    var date = (DateTime) dataReader["Date"];
                    analyzes.Add(new Analysis(binFormatter.Deserialize(data), title, date));
                }
            }

            _command.Parameters.Clear();
            return analyzes;
        }

        #endregion

        private DbParameter GetParam(string paramName, object paramValue)
        {
            DbParameter param = _dbProviderFactory.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            return param;
        }
    }
}