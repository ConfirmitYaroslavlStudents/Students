using System.Data.SqlClient;

namespace HospitalLib.DatebaseModel
{
    public interface IDatabaseProvider
    {
        void PushData(string query);
        void PushData(SqlCommand command);
        SqlDataReader GetData(string query);
        SqlDataReader GetData(SqlCommand command);
        int GetDataScalar(string query);
    }
}
