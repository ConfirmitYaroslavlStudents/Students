using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using HospitalLib.Data;
using HospitalLib.DatebaseModel;
using HospitalLib.Utils.Logger;

namespace HospitalLib.Providers
{
    public class PersonProvider : IPersonsProvider
    {
        private readonly DatabaseProvider _databaseProvider;

        public PersonProvider(DatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new ArgumentNullException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public IList<Person> Load()
        {
            const string query = "SELECT * FROM Persons";
            var command = new SqlCommand(query);

            var stopwtach = new Stopwatch();
            stopwtach.Start();
            SqlDataReader reader = _databaseProvider.GetData(command);
            stopwtach.Stop();

            HospitalLogger.LogInfo("GetData method execution time: {0}", stopwtach.Elapsed.ToString());
            return GetPersons(reader);
        }

        public void Save(ref Person person)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "SavePersonAndGetId"
            };

            AddMainParameters(command, person);
            command.Parameters["@PersonId"].Direction = ParameterDirection.Output;

            SqlParameterCollection parameters = _databaseProvider.PushDataWithOutputParameters(command);
            int id = int.Parse(parameters["@PersonId"].Value.ToString());

            person = new Person(id, person.FirstName, person.LastName, person.MiddleName, person.BirthDate);
        }

        public void Update(Person person)
        {
            const string query =
                "UPDATE Persons SET FirstName = @FirstName, LastName=@LastName, MiddleName=@MiddleName, " +
                "BirthDate=@BirthDate WHERE PersonId=@PersonId";

            var command = new SqlCommand(query);

            AddMainParameters(command, person);
            command.Parameters["@PersonId"].Value = person.Id;

            _databaseProvider.PushData(command);
        }

        public void Remove(Person person)
        {
            new AnalysisProvider(_databaseProvider).Remove(person);
            const string query = "DELETE FROM Persons WHERE PersonId=@PersonId";

            var command = new SqlCommand(query);
            command.Parameters.Add("@PersonId", SqlDbType.Int);
            command.Parameters["@PersonId"].Value = person.Id;

            _databaseProvider.PushData(command);
        }

        public IList<Person> Search(string searchFirstName, string searchLastName)
        {
            const string query = "SELECT * FROM Persons WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName";

            var command = new SqlCommand(query);

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            command.Parameters["@FirstName"].Value = "%" + searchFirstName + "%";
            command.Parameters.Add("@LastName", SqlDbType.NVarChar);
            command.Parameters["@LastName"].Value = "%" + searchLastName + "%";

            SqlDataReader reader = _databaseProvider.GetData(command);

            return GetPersons(reader);
        }

        public int GetCount()
        {
            const string query = "SELECT count(*) FROM Persons";
            int result = _databaseProvider.GetDataScalar(query);
            int count = int.Parse(result.ToString(CultureInfo.InvariantCulture));

            return count;
        }

        private void AddMainParameters(SqlCommand command, Person person)
        {
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            command.Parameters["@FirstName"].Value = person.FirstName;

            command.Parameters.Add("@LastName", SqlDbType.NVarChar);
            command.Parameters["@LastName"].Value = person.LastName;

            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar);
            command.Parameters["@MiddleName"].Value = person.MiddleName;

            command.Parameters.Add("@BirthDate", SqlDbType.VarChar);
            command.Parameters["@BirthDate"].Value = person.BirthDate.ToShortDateString();

            command.Parameters.Add("@PersonId", SqlDbType.Int);
        }

        private IList<Person> GetPersons(SqlDataReader reader)
        {
            var persons = new List<Person>();
            while (reader.Read())
            {
                persons.Add(GetPerson(reader));
            }

            return persons;
        }

        public Person GetPerson(SqlDataReader reader)
        {
            int id = int.Parse(reader["PersonId"].ToString());
            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            DateTime birthDate = DateTime.Parse(reader["BirthDate"].ToString());
            string middleName = reader["MiddleName"].ToString();

            return new Person(id, firstName, lastName, middleName, birthDate);
        }
    }
}