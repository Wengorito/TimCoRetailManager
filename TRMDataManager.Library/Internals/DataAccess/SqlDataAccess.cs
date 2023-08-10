using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TRMDataManager.Library.Internals.DataAccess
{
    internal class SqlDataAccess
    {

        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parametres, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parametres, commandType: CommandType.StoredProcedure)
                    .ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameteres, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameteres, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
