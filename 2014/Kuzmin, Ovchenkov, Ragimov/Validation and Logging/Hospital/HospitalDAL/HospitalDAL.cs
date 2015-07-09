using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using ListFormatter;
using LogService;
using Shared;
using Shared.Interfaces;

namespace HospitalConnectedLayer
{
    public class HospitalDAL : IHospitalDataAccessLayer
    {
        private readonly DbCommand _command;
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly DbConnection _sqlConnection;

        #region Open / Close methods

        public HospitalDAL(string dataProvider, string connectionString)
        {
            try
            {
                _dbProviderFactory = DbProviderFactories.GetFactory(dataProvider);
                _sqlConnection = _dbProviderFactory.CreateConnection();
                _command = _dbProviderFactory.CreateCommand();
                _command.Connection = _sqlConnection;
                _sqlConnection.ConnectionString = connectionString;
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't load data storage!",ex);
                throw new InvalidOperationException("Can't load data storage!");
            }
        }

        private void CloseConnection()
        {
            _command.Parameters.Clear();
            _sqlConnection.Close();
            //LOGGING
            Logger.Info("Sql connection was closed");
        }

        private void OpenConnection()
        {
            try
            {
                _sqlConnection.Open();
                //LOGGING
                Logger.Info("Sql connection was opened");
            }
            catch (Exception ex)
            {
                CloseConnection();
                //LOGGING
                Logger.Error("Can't load data storage!",ex);
                throw new InvalidOperationException("Can't load data storage!");
            }
        }

        #endregion

        #region Person

        public List<Person> GetPersons(string firstName, string lastName, string policyNumber)
        {
            OpenConnection();
            _command.CommandText =
                string.Format(
                    "SELECT * FROM Persons WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName AND PolicyNumber LIKE @PolicyNumber");

            _command.Parameters.Add(GetParam("@FirstName", string.Format("%{0}%", firstName)));
            _command.Parameters.Add(GetParam("@LastName", string.Format("%{0}%", lastName)));
            _command.Parameters.Add(GetParam("@PolicyNumber", string.Format("%{0}%", policyNumber)));

            var persons = new List<Person>();

            try
            {
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
                //LOGGING
                Logger.Info("Persons were obtained from database");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't get persons!",ex);
                throw new InvalidOperationException("Can't get persons!");
            }
            finally
            {
                CloseConnection();
            }

            return persons;
        }


        public void AddPerson(Person person)
        {
            OpenConnection();
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
                //LOGGING
                Logger.Info("Person was added in database");
            }
            catch (SqlException ex)
            {
                //LOGGING
                Logger.Error("This person already exists!",ex);
                throw new InvalidOperationException("This person already exists!");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't add person!",ex);
                throw new InvalidOperationException("Can't add person!");
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region Template

        public Template GetTemplate(string title)
        {
            OpenConnection();
            _command.CommandText = "SELECT * FROM Templates WHERE Title = @Title";

            _command.Parameters.Add(GetParam("@Title", title));

            Template template = null;
            var binFormatter = new ListBinaryFormatter<string>();

            try
            {
                using (DbDataReader dataReader = _command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        template = new Template(binFormatter.Deserialize((byte[]) dataReader["Data"]), title);
                    }
                }
                //LOGGING
                Logger.Info("Template was obtained");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't get template!",ex);
                throw new InvalidOperationException("Can't get template!");
            }
            finally
            {
                CloseConnection();
            }

            return template;
        }

        public void AddTemplate(Template template)
        {
            OpenConnection();
            var binFormatter = new ListBinaryFormatter<string>();

            _command.CommandText = "INSERT INTO Templates(Title, Data) VALUES(@Title, @Data)";

            _command.Parameters.Add(GetParam("@Title", template.Title));
            _command.Parameters.Add(GetParam("@Data", binFormatter.Serialize(template.Data)));

            try
            {
                _command.ExecuteNonQuery();
                //LOGGING
                Logger.Info("Template was added");
            }
            catch (SqlException ex)
            {
                //LOGGING
                Logger.Error("This template already exists!",ex);
                throw new InvalidOperationException("This template already exists!");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't add template!",ex);
                throw new InvalidOperationException("Can't add template!");
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Template> GetTemplates()
        {
            OpenConnection();
            _command.CommandText = "SELECT * FROM Templates";
            var templates = new List<Template>();
            var binFormatter = new ListBinaryFormatter<string>();

            try
            {
                using (DbDataReader dataReader = _command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var title = (string) dataReader["Title"];
                        IList<string> data = binFormatter.Deserialize((byte[]) dataReader["Data"]);
                        templates.Add(new Template(data, title));
                    }
                }
                //LOGGING
                Logger.Info("Templates were obtained");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't get templates!",ex);
                throw new InvalidOperationException("Can't get templates!");
            }
            finally
            {
                CloseConnection();
            }

            return templates;
        }

        #endregion

        #region Analysis

        public void AddAnalysis(string policyNumber, Analysis analysis)
        {
            OpenConnection();
            var binFormatter = new ListBinaryFormatter<string>();
            _command.CommandText =
                "INSERT INTO Analyzes(PolicyNumber, TemplateTitle, Data, Date) VALUES(@PolicyNumber, @TemplateTitle, @Data, @Date)";

            _command.Parameters.Add(GetParam("@TemplateTitle", analysis.TemplateTitle));
            _command.Parameters.Add(GetParam("@Data", binFormatter.Serialize(analysis.Data)));
            _command.Parameters.Add(GetParam("@Date", analysis.Date));
            _command.Parameters.Add(GetParam("@PolicyNumber", policyNumber));

            try
            {
                _command.ExecuteNonQuery();
                //LOGGING
                Logger.Info("Analysis was added");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't add analysis!",ex);
                throw new InvalidOperationException("Can't add analysis!");
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Analysis> GetAnalyzes(string policyNumber)
        {
            OpenConnection();
            _command.CommandText = "SELECT TemplateTitle, Data, Date FROM Analyzes WHERE PolicyNumber = @PolicyNumber";

            _command.Parameters.Add(GetParam("@PolicyNumber", policyNumber));

            var analyzes = new List<Analysis>();
            var binFormatter = new ListBinaryFormatter<string>();

            try
            {
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
                //LOGGING
                Logger.Info("Analyzes were obtained");
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Can't get analyzes!", ex);
                throw new InvalidOperationException("Can't get analyzes!");
            }
            finally
            {
                CloseConnection();
            }

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