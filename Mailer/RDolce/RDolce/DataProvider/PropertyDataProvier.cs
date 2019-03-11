using Dapper;
using RDolce.Interfaces;
using RDolce.Property;
using RDolce.Property;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce.DataProvider
{
    public class PropertyDataProvier : IPropertyDataProvider
    {
        string connectionString = Startup.connectionString; // = "Server=DESKTOP-H8PU1EK\\SQLEXPRESS17;Database=RDolce;Trusted_Connection=True;MultipleActiveResultSets=true";

        private SqlConnection sqlConnection;



        public async Task AddPropertyFields(RDolce.Property.PropertFields propertyFields)
        {

            try
            {

                using (var sqlConnection = new SqlConnection(connectionString))
                {

                    await sqlConnection.OpenAsync();
                    var dynamicParameters = new DynamicParameters();
                    //   dynamicParameters.Add("@UserId", user.UserId);



                    dynamicParameters.Add(@"@PropertyId", propertyFields.@PropertyId);
                    dynamicParameters.Add(@"Id", propertyFields.Id);
                    dynamicParameters.Add(@"PropertyName", propertyFields.PropertyName);
                    dynamicParameters.Add(@"PropertyPhoneNumber", propertyFields.PropertyPhoneNumber);

                    dynamicParameters.Add(@"PropertyBedrooms", propertyFields.PropertyBedrooms);
                    dynamicParameters.Add(@"PropertyBathrooms", propertyFields.PropertyBathrooms);
                    dynamicParameters.Add(@"PropertySleeps", propertyFields.PropertySleeps);
                    dynamicParameters.Add(@"PropertyInternalId", propertyFields.PropertyInternalId);
                    dynamicParameters.Add(@"PropertyInternalOwnerName", propertyFields.PropertyInternalOwnerName);
                    dynamicParameters.Add(@"PropertyInternalOwnerPhone", propertyFields.PropertyInternalOwnerPhone);
                    dynamicParameters.Add(@"PropertyInternalOwnerEmail", propertyFields.PropertyInternalOwnerEmail);


                    await sqlConnection.ExecuteAsync(
                        "InsertIntoProperty",
                        dynamicParameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Task DeletePropertyFields (int propertyId)
        {
            throw new NotImplementedException();
        }



        public async Task<RDolce.Property.PropertFields> GetPropertyFields(string propertyId)
        {
            PropertFields res = new PropertFields();

            try
            {

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                    var reader = await sqlConnection.ExecuteReaderAsync(
                      "GetPropertyById",
                      param: new Dictionary<string, object> { { "@propertyID", propertyId } },
                      commandType: CommandType.StoredProcedure);

                    while (reader.Read())
                    {
                        try
                        {
                            res.PropertyId = reader["PropertyId"].ToString();
                            res.PropertyName = reader["PropertyName"].ToString();
                            res.Id = reader["Id"].ToString();
                            res.PropertyName = reader["PropertyName"].ToString();
                            res.PropertyPhoneNumber = reader["PropertyPhoneNumber"].ToString();
                            res.PropertyBedrooms = reader["PropertyBedrooms"].ToString();
                            res.PropertyBathrooms = reader["PropertyBathrooms"].ToString();
                            res.PropertySleeps = reader["PropertySleeps"].ToString();
                            res.PropertyInternalId = reader["PropertySleeps"].ToString();
                            res.PropertyInternalOwnerName = reader["PropertyInternalOwnerName"].ToString();
                            res.PropertyInternalOwnerPhone = reader["PropertyInternalOwnerPhone"].ToString();
                            res.PropertyInternalOwnerEmail = reader["PropertyInternalOwnerEmail"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public Task<IEnumerable<RDolce.Property.PropertFields>> GetPropertyFields()
        {
            throw new NotImplementedException();
        }

        public Task UpdatePropertyFields(RDolce.Property.PropertFields propertyFields)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReservationFieldsUpdatePropertyFields(RDolce.Property.PropertFields propertyFields)
        {
            throw new NotImplementedException();
        }
    }
}
