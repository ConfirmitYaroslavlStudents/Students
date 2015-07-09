using System;
using System.Data.SqlClient;
using System.IO;

namespace HospitalLib.Providers
{
    public class DatabaseProvider
    {
        private const string DatabaseFileName = "Database\\HospitalDatabase.mdf";

        public void PushData(SqlCommand command)
        {
            SqlConnection connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();
        }

        public SqlDataReader GetData(SqlCommand command)
        {
            SqlConnection connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            SqlDataReader read = command.ExecuteReader();

            return read;
        }

        public SqlParameterCollection PushDataWithOutputParameters(SqlCommand command)
        {
            SqlConnection connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();

            return command.Parameters;
        }

        public int GetDataScalar(string query)
        {
            SqlConnection connection = CreateConection();
            connection.Open();
            var command = new SqlCommand(query, connection);
            object reader = command.ExecuteScalar();

            connection.Close();
            connection.Dispose();

            return (int) reader;
        }

        private static SqlConnection CreateConection()
        {
            string outputFolder = Environment.CurrentDirectory;
            string attachDbFilename = Path.Combine(outputFolder, DatabaseFileName);
            string connectionString =
                string.Format(
                    @"Data Source=(LocalDB)\v11.0;Initial Catalog=HospitalDatabase;AttachDbFilename=""{0}"";Integrated Security=True",
                    attachDbFilename);

            return new SqlConnection(connectionString);
        }
    }
}