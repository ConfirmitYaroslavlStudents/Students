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
            var connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();
        }

        public SqlDataReader GetData(SqlCommand command)
        {
            var connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            var read = command.ExecuteReader();

            return read;
        }

        public SqlParameterCollection PushDataWithOutputParameters(SqlCommand command)
        {
            var connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();

            return command.Parameters;
        }

        public int GetDataScalar(string query)
        {
            var connection = CreateConection();
            connection.Open();
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteScalar();

            connection.Close();
            connection.Dispose();

            return (int) reader;
        }

        private static SqlConnection CreateConection()
        {
            var outputFolder = Environment.CurrentDirectory;
            var attachDbFilename = Path.Combine(outputFolder, DatabaseFileName);
            var connectionString =
                string.Format(
                    @"Data Source=(LocalDB)\v11.0;Initial Catalog=HospitalDatabase;AttachDbFilename=""{0}"";Integrated Security=True",
                    attachDbFilename);

            return new SqlConnection(connectionString);
        }
    }
}
