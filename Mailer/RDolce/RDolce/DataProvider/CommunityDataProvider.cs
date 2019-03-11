using Dapper;
using RDolce.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce.DataProvider
{
    public class CommunityDataProvider : ICommunityDataProvider
    {
        private readonly string connectionString = AppSettingsJson.GetAppSettings()["LocalDBConnectionString"]   ;//"Server=MAHESHI;Database=Login;Trusted_Connection=True;";

        private SqlConnection sqlConnection;


        public async Task AddCommunity(Community community)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //   dynamicParameters.Add("@UserId", user.UserId);
                //  dynamicParameters.Add("@UserName", user.UserName);
                //   dynamicParameters.Add("@Email", user.Email);
                // dynamicParameters.Add("@Password", user.Password);

                await sqlConnection.ExecuteAsync(
                    "spAddUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task DeleteCommunity(int CommunityId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CommunityId", CommunityId);
                await sqlConnection.ExecuteAsync(
                    "spDeleteUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<Community> GetCommunity(int UserId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", UserId);
                return await sqlConnection.QuerySingleOrDefaultAsync<Community>(
                    "spGetUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Community>> GetCommunity()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Community>(
                    "spGetUsers",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task UpdateCommunity(Community community)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //   dynamicParameters.Add("@UserId", user.UserId);
                //   dynamicParameters.Add("@UserName", user.UserName);
                //   dynamicParameters.Add("@Email", user.Email);
                //   dynamicParameters.Add("@Password", user.Password);
                //  await sqlConnection.ExecuteAsync(
               // "spUpdateUser",
                 //   dynamicParameters,
                //    commandType: CommandType.StoredProcedure);
            }
        }
    }
}

