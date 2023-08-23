using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class UserData
    {
        private readonly string _connectionStringName;
        private readonly IConfiguration _config;

        public UserData(string connectionStringName, IConfiguration config)
        {
            _connectionStringName = connectionStringName;
            _config = config;
        }

        public List<UserModel> GetUserById(string id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id = id };

            var output = sql.LoadData<UserModel, dynamic>("spUserLookup", p, _connectionStringName);

            return output;
        }

        public List<UserModel> GetAll()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { };

            var output = sql.LoadData<UserModel, dynamic>("spUser_GetAll", p, _connectionStringName);

            return output;
        }
    }
}
