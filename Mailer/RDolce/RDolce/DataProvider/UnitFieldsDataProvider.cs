using Dapper;
using RDolce.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce.DataProvider
{
    public class UnitFieldsDataProvider: IUnitFieldsDataProvider
    {
        string connectionString = "Server=DESKTOP-H8PU1EK\\SQLEXPRESS17;Database=RDolce;Trusted_Connection=True;MultipleActiveResultSets=true";

        private SqlConnection sqlConnection;

        public async Task AddUnitFields(UnitFields unitFields)
        {
            try
            {

                using (var sqlConnection = new SqlConnection(connectionString))
                {

                    await sqlConnection.OpenAsync();
                    var dynamicParameters = new DynamicParameters();
                    //   dynamicParameters.Add("@UserId", user.UserId);
                    //  dynamicParameters.Add("@UserName", user.UserName);
                    //   dynamicParameters.Add("@Email", user.Email);
                    // dynamicParameters.Add("@Password", user.Password);

                    dynamicParameters.Add(@"Id", unitFields.Id);
                    string building = string.Empty;
                    if (unitFields.Building != null)
                    {
                        foreach (var item in unitFields.Building)
                        {
                            building = building + ", " + item;
                        }

                        building = building.Trim().Trim(',').Trim();
                    }



                    dynamicParameters.Add(@"Building", building);
                    dynamicParameters.Add(@"Address", unitFields.Address);

                    dynamicParameters.Add(@"City", unitFields.City);
                    dynamicParameters.Add(@"Zip", unitFields.Zip);
                    dynamicParameters.Add(@"Field22", unitFields.Field22);
                    dynamicParameters.Add(@"Description", unitFields.Description);
                    dynamicParameters.Add(@"Sleeps", unitFields.Sleeps);
                    dynamicParameters.Add(@"Bedrooms", unitFields.Bedrooms);
                    dynamicParameters.Add(@"Baths", unitFields.Baths);
                    dynamicParameters.Add(@"PropertyType", unitFields.PropertyType);
                    dynamicParameters.Add(@"State", unitFields.State);
                    dynamicParameters.Add(@"AnnualRent", unitFields.AnnualRent);
                    dynamicParameters.Add(@"SeasonalJanMar", unitFields.SeasonalJanMar);
                    dynamicParameters.Add(@"OffSeasonalAprilDec", unitFields.OffSeasonalAprilDec);
                    dynamicParameters.Add(@"StandardSeasonalWeekly", unitFields.StandardSeasonalWeekly);
                    dynamicParameters.Add(@"StandardSeasonalDaily", unitFields.StandardSeasonalDaily);
                    dynamicParameters.Add(@"OffSeasonalWeekly", unitFields.OffSeasonalWeekly);
                    dynamicParameters.Add(@"OffSeasonalDaily", unitFields.OffSeasonalDaily);
                    dynamicParameters.Add(@"Table62", unitFields.Table62);


                    string hoa = string.Empty;
                    if (unitFields.HoaCommunitiesCopy != null)
                    {
                        foreach (var item in unitFields.HoaCommunitiesCopy)
                        {
                            hoa = hoa + ", " + item;
                        }

                        hoa = hoa.Trim().Trim(',').Trim();
                    }
                    dynamicParameters.Add(@"HoaCommunitiesCopy", hoa);

                    dynamicParameters.Add(@"unitID", unitFields.unitId);

                    await sqlConnection.ExecuteAsync(
                        "InsertIntoUnitField",
                        dynamicParameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Task DeleteUnitFields(int unitId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UnitFields>> GetUnitFields()
        {
            throw new NotImplementedException();
        }

        public Task<UnitFields> GetUnitFields(int unitId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUnitFields(UnitFields unitFields)
        {
            throw new NotImplementedException();
        }
    }
}
