using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using ListFormatter;
using Shared;
using Shared.Interfaces;

namespace HospitalConnectedLayer
{
    public class HospitalDAL : IHospitalDataAccessLayer
    {
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly DbConnection _sqlConnection;

        #region Open / Close methods

        public HospitalDAL(string dataProvider, string connectionString)
        {
            try
            {
                _dbProviderFactory = DbProviderFactories.GetFactory(dataProvider);
                _sqlConnection = _dbProviderFactory.CreateConnection();
                _sqlConnection.ConnectionString = connectionString;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Can't load data storage!");
            }
        }

        private void OpenConnection()
        {
            try
            {
                _sqlConnection.Open();
            }
            catch (Exception)
            {
                _sqlConnection.Close();
                throw new InvalidOperationException("Can't load data storage!");
            }
        }

        #endregion

        #region Person

        public List<Person> GetPersons(string firstName, string lastName, string policyNumber)
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandText =
                        string.Format(
                            "SELECT * FROM Persons WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName AND PolicyNumber LIKE @PolicyNumber");

                command.Parameters.Add(GetParam("@FirstName", string.Format("%{0}%", firstName)));
                command.Parameters.Add(GetParam("@LastName", string.Format("%{0}%", lastName)));
                command.Parameters.Add(GetParam("@PolicyNumber", string.Format("%{0}%", policyNumber)));

                var persons = new List<Person>();

                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            persons.Add(new Person(
                                (string)dataReader["FirstName"],
                                (string)dataReader["LastName"],
                                (DateTime)dataReader["DateOfBirth"],
                                (string)dataReader["Address"],
                                (string)dataReader["PolicyNumber"]));
                        }
                    }
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't get persons!");
                }
                finally
                {
                    _sqlConnection.Close();
                }
                
                return persons; 
            }
        }


        public void AddPerson(Person person)
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandText =
                    "INSERT INTO Persons(FirstName, LastName, DateOfBirth, Address, PolicyNumber) VALUES(@FirstName, @LastName, @DateOfBirth, @Address, @PolicyNumber)";

                command.Parameters.Add(GetParam("@FirstName", person.FirstName));
                command.Parameters.Add(GetParam("@LastName", person.LastName));
                command.Parameters.Add(GetParam("@Address", person.Address));
                command.Parameters.Add(GetParam("@DateOfBirth", person.DateOfBirth));
                command.Parameters.Add(GetParam("@PolicyNumber", person.PolicyNumber));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new InvalidOperationException("This person already exists!");
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't add person!");
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        #endregion

        #region Template

        public Template GetTemplate(string title)
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandText = "SELECT * FROM Templates WHERE Title = @Title";

                command.Parameters.Add(GetParam("@Title", title));

                Template template = null;
                var binFormatter = new ListBinaryFormatter<string>();

                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            template = new Template(binFormatter.Deserialize((byte[]) dataReader["Data"]), title);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't get template!");
                }
                finally
                {
                    _sqlConnection.Close();
                }

                return template;
            }
        }

        public void AddTemplate(Template template)
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                var binFormatter = new ListBinaryFormatter<string>();

                command.CommandText = "INSERT INTO Templates(Title, Data) VALUES(@Title, @Data)";

                command.Parameters.Add(GetParam("@Title", template.Title));
                command.Parameters.Add(GetParam("@Data", binFormatter.Serialize(template.Data)));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new InvalidOperationException("This template already exists!");
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't add template!");
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public List<Template> GetTemplates()
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandText = "SELECT * FROM Templates";
                var templates = new List<Template>();
                var binFormatter = new ListBinaryFormatter<string>();

                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var title = (string) dataReader["Title"];
                            IList<string> data = binFormatter.Deserialize((byte[]) dataReader["Data"]);
                            templates.Add(new Template(data, title));
                        }
                    }
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't get templates!");
                }
                finally
                {
                    _sqlConnection.Close();
                }

                return templates;
            }
        }

        #endregion

        #region Analysis

        public void AddAnalysis(string policyNumber, Analysis analysis)
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                var binFormatter = new ListBinaryFormatter<string>();
                command.CommandText =
                    "INSERT INTO Analyzes(PolicyNumber, TemplateTitle, Data, Date) VALUES(@PolicyNumber, @TemplateTitle, @Data, @Date)";

                command.Parameters.Add(GetParam("@TemplateTitle", analysis.TemplateTitle));
                command.Parameters.Add(GetParam("@Data", binFormatter.Serialize(analysis.Data)));
                command.Parameters.Add(GetParam("@Date", analysis.Date));
                command.Parameters.Add(GetParam("@PolicyNumber", policyNumber));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't add analysis!");
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        public List<Analysis> GetAnalyzes(string policyNumber)
        {
            OpenConnection();

            using (DbCommand command = _dbProviderFactory.CreateCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandText =
                    "SELECT TemplateTitle, Data, Date FROM Analyzes WHERE PolicyNumber = @PolicyNumber";

                command.Parameters.Add(GetParam("@PolicyNumber", policyNumber));

                var analyzes = new List<Analysis>();
                var binFormatter = new ListBinaryFormatter<string>();

                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var data = (byte[]) dataReader["Data"];
                            var title = (string) dataReader["TemplateTitle"];
                            var date = (DateTime) dataReader["Date"];
                            analyzes.Add(new Analysis(binFormatter.Deserialize(data), title, date));
                        }
                    }
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Can't get analyzes!");
                }
                finally
                {
                    _sqlConnection.Close();
                }

                return analyzes;
            }
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