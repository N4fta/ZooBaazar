using Microsoft.Data.SqlClient;

namespace Data_Access
{
    public abstract class DatabaseConnection
    {
        private readonly string connectionString = "Server=mssqlstud.fhict.local;Database=dbi540432_zoobaazar;User Id=dbi540432_zoobaazar;Password=ZooBaazar12345;TrustServerCertificate=True";

        protected SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;

        }
    }
}

