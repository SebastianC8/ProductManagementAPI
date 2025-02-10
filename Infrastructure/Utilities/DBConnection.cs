using Infrastructure.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Utilities
{
    public class DBConnection
    {
        public static SqlConnection Connection()
        {
            string? strConnection = ConfigManager.AppSetting.GetConnectionString("storeConnection");
            SqlConnection sqlConnection = new SqlConnection(strConnection);
            return sqlConnection;
        }
    }
}
