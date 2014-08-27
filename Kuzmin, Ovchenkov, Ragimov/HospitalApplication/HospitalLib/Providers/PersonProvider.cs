using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using HospitalLib.Data;
using HospitalLib.DatebaseModel;

namespace HospitalLib.Providers
{
    public class PersonProvider : IPersonsProvider
    {
        private readonly IDatabaseProvider _databaseProvider;

        public PersonProvider(IDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new NullReferenceException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public IList<Person> Load()
        {
            const string query = "select * from Persons";
            var reader = _databaseProvider.GetData(query);

            return GetPersons(reader);
        }

        public void Save(Person person)
        {
            const string query =
                "insert into Persons (PersonId, FirstName, LastName, MiddleName, BirthDate)" +
                " values(@PersonId, @FirstName, @LastName, @MiddleName, @BirthDate)";

            var command = new SqlCommand(query);

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            command.Parameters["@FirstName"].Value = person.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar);
            command.Parameters["@LastName"].Value = person.LastName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar);
            command.Parameters["@MiddleName"].Value = person.MiddleName;

            command.Parameters.AddWithValue("@PersonId",  person.Id);
            command.Parameters.AddWithValue("@BirthDate", person.BirthDate.ToShortDateString());

            _databaseProvider.PushData(command);
        }

        public void Update(Person person)
        {
            const string query = "update Persons set FirstName = @FirstName, LastName=@LastName, MiddleName=@MiddleName, " +
                                 "BirthDate=@BirthDate where PersonId=@PersonId";

            var command = new SqlCommand(query);
            PrepareStatent(command,person);

        }

        private void PrepareStatent(SqlCommand command, Person person)
        {
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            command.Parameters["@FirstName"].Value = person.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar);
            command.Parameters["@LastName"].Value = person.LastName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar);
            command.Parameters["@MiddleName"].Value = person.MiddleName;

            command.Parameters.AddWithValue("@PersonId", person.Id);
            command.Parameters.AddWithValue("@BirthDate", person.BirthDate.ToShortDateString());

            _databaseProvider.PushData(command);
        }

        public void Remove(Person person)
        {
            new AnalysisProvider(_databaseProvider).Remove(person);
            var query = string.Format("delete from Persons where PersonId='{0}'", person.Id);

            _databaseProvider.PushData(query);
        }

        public IList<Person> Search(string searchFirstName, string searchLastName)
        {
            const string query = "select * from Persons where FirstName like @FirstName and LastName like @LastName";

            var command = new SqlCommand(query);

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            command.Parameters["@FirstName"].Value = "%" + searchFirstName + "%";
            command.Parameters.Add("@LastName", SqlDbType.NVarChar);
            command.Parameters["@LastName"].Value = "%" + searchLastName + "%";

            var reader = _databaseProvider.GetData(command);
            
            return GetPersons(reader);
        }

        public int GetCount()
        {
            const string query = "select count(*) from Persons";
            var result = _databaseProvider.GetDataScalar(query);
            var count = int.Parse(result.ToString(CultureInfo.InvariantCulture));

            return count;
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
            var id = int.Parse(reader["PersonId"].ToString());
            var firstName = reader["FirstName"].ToString();
            var lastName = reader["LastName"].ToString();
            var birthDate = DateTime.Parse(reader["BirthDate"].ToString());
            var middleName = reader["MiddleName"].ToString();
           
            return new Person(id, firstName, lastName, middleName, birthDate);
        }
    }
}
