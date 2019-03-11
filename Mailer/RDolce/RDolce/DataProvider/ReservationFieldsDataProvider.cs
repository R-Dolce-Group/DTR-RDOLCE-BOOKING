using Dapper;
using Microsoft.Extensions.Configuration;
using RDolce.Classes;
using RDolce.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce.DataProvider
{
    public class ReservationFieldsDataProvider : IReservationFieldsDataProvider
    {

        //string connectionString = "Server=DESKTOP-H8PU1EK\\SQLEXPRESS17;Database=RDolce;Trusted_Connection=True;MultipleActiveResultSets=true";

        string connectionString = Startup.connectionString;
        //string connectionString = new IConfiguration().GetConnectionString("LocalDBConnectionString");
        private readonly IConfiguration configuration;

        // string connectionString = 
        private SqlConnection sqlConnection;

        public async Task AddReservationFields(ReservationFields reservationFields)
        {
          //  string connectionString= configuration.GetConnectionString("LocalDBConnectionString");


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

                    dynamicParameters.Add(@"ReservationId", reservationFields.ReservationId);
                    dynamicParameters.Add(@"Id", reservationFields.Id);
                    dynamicParameters.Add(@"DateReservationCreated", reservationFields.DateReservationCreated);
                    dynamicParameters.Add(@"Status", reservationFields.Status);

                    dynamicParameters.Add(@"Source", reservationFields.Source);
                    dynamicParameters.Add(@"DurationInDays", reservationFields.DurationInDays);
                    dynamicParameters.Add(@"DateReservationStarts", reservationFields.DateReservationStarts);
                    dynamicParameters.Add(@"TimeReservationStarts", reservationFields.TimeReservationStarts);
                    dynamicParameters.Add(@"DateReservationEnds", reservationFields.DateReservationEnds);
                    dynamicParameters.Add(@"TimeReservationEnds", reservationFields.TimeReservationEnds);
                    dynamicParameters.Add(@"TotalGuests", reservationFields.TotalGuests);
                    dynamicParameters.Add(@"ReservationGuestSpecialRequests", reservationFields.ReservationGuestSpecialRequests);
                    dynamicParameters.Add(@"TotalAmount", reservationFields.TotalAmount);
                    dynamicParameters.Add(@"TotalPaidAmount", reservationFields.TotalPaidAmount);
                    dynamicParameters.Add(@"TotalRefundAmount", reservationFields.TotalRefundAmount);
                    dynamicParameters.Add(@"TotalNonSecurityDepositRefundAmount", reservationFields.TotalNonSecurityDepositRefundAmount);
                    dynamicParameters.Add(@"TotalSecurityDepositRefundAmount", reservationFields.TotalSecurityDepositRefundAmount);
                    dynamicParameters.Add(@"TotalRatesAmount", reservationFields.TotalRatesAmount);
                    dynamicParameters.Add(@"TotalTaxableFeesAmount", reservationFields.TotalTaxableFeesAmount);
                    dynamicParameters.Add(@"TotalTaxesAmount", reservationFields.TotalTaxesAmount);
                    dynamicParameters.Add(@"TotalNonTaxableFeesAmount", reservationFields.TotalNonTaxableFeesAmount);
                    dynamicParameters.Add(@"RemainingBalance", reservationFields.RemainingBalance);
                    dynamicParameters.Add(@"SecurePaymentLink", reservationFields.SecurePaymentLink.OriginalString);
                    string guestID = string.Empty;
                    if (reservationFields.GuestId != null)
                    {
                        foreach (var item in reservationFields.GuestId)
                        {
                            guestID = guestID + ", " + item;
                        }

                        guestID = guestID.Trim().Trim(',').Trim();
                    }
                    dynamicParameters.Add(@"GuestId", guestID);
                    string GuestFullName = string.Empty;
                    if (reservationFields.GuestFullName != null)
                    {
                        foreach (var item in reservationFields.GuestFullName)
                        {
                            GuestFullName = GuestFullName + ", " + item;
                        }

                        GuestFullName = GuestFullName.Trim().Trim(',').Trim();
                    }

                    // dynamicParameters.Add(@"GuestFullName", reservationFields.GuestFullName);
                    dynamicParameters.Add(@"GuestFullName", GuestFullName);
                    dynamicParameters.Add(@"GuestFirstName", reservationFields.GuestFirstName);
                    dynamicParameters.Add(@"GuestLastName", reservationFields.GuestLastName);
                    dynamicParameters.Add(@"GuestEmailAddress", reservationFields.GuestEmailAddress);
                    dynamicParameters.Add(@"GuestPhoneNumber", reservationFields.GuestPhoneNumber);
                    string PropertyId = string.Empty;
                    if (reservationFields.PropertyId != null)
                    {
                        foreach (var item in reservationFields.PropertyId)
                        {
                            PropertyId = PropertyId + ", " + item;
                        }

                        PropertyId = PropertyId.Trim().Trim(',').Trim();
                    }
                    dynamicParameters.Add(@"PropertyId", PropertyId);
                    dynamicParameters.Add(@"PropertyName", reservationFields.PropertyName);
                    dynamicParameters.Add(@"PropertyPhoneNumber", reservationFields.PropertyPhoneNumber);
                    dynamicParameters.Add(@"PropertyAddressLine1", reservationFields.PropertyAddressLine1);
                    dynamicParameters.Add(@"PropertyAddressLine2", reservationFields.PropertyAddressLine2);
                    dynamicParameters.Add(@"PropertyCity", reservationFields.PropertyCity);
                    dynamicParameters.Add(@"PropertyStateProvince", reservationFields.PropertyStateProvince);
                    dynamicParameters.Add(@"PropertyPostalCode", reservationFields.PropertyPostalCode);
                    dynamicParameters.Add(@"PropertyNeighborhood", reservationFields.PropertyNeighborhood[0]);
                    dynamicParameters.Add(@"PropertyLatitude", reservationFields.PropertyLatitude);
                    dynamicParameters.Add(@"PropertyLongitude", reservationFields.PropertyLongitude);
                    dynamicParameters.Add(@"PropertyBedrooms", reservationFields.PropertyBedrooms);
                    dynamicParameters.Add(@"PropertyBathrooms", reservationFields.PropertyBathrooms);
                    dynamicParameters.Add(@"PropertySleeps", reservationFields.PropertySleeps);
                    dynamicParameters.Add(@"PropertyInternalId", reservationFields.PropertyInternalId);
                    dynamicParameters.Add(@"PropertyInternalOwnerName", reservationFields.PropertyInternalOwnerName);
                    dynamicParameters.Add(@"PropertyInternalOwnerPhone", reservationFields.PropertyInternalOwnerPhone);
                    dynamicParameters.Add(@"PropertyInternalOwnerEmail", reservationFields.PropertyInternalOwnerEmail);

                    dynamicParameters.Add(@"GuestAddressLine1", reservationFields.GuestAddressLine1);

                    dynamicParameters.Add(@"GuestAddressLine2", reservationFields.GuestAddressLine2);

                    dynamicParameters.Add(@"GuestCity", reservationFields.GuestCity);
                    dynamicParameters.Add(@"GuestStateProvince", reservationFields.GuestStateProvince);

                    dynamicParameters.Add(@"GuestPostalCode", reservationFields.GuestPostalCode);
                    dynamicParameters.Add(@"GuestCountry", reservationFields.GuestCountry);

                    dynamicParameters.Add(@"GuestNotes", reservationFields.GuestNotes);



                    await sqlConnection.ExecuteAsync(
                        "InsertReservationFields",
                        dynamicParameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<ReservationFields>> GetUnProcessed()
        {
            List<ReservationFields> res = new List<ReservationFields>();
            //string connectionString = configuration.GetConnectionString("LocalDBConnectionString");
            try
            {

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                   var reader =  await sqlConnection.ExecuteReaderAsync(
                     "GetUnprocessedReservations",
                     commandType: CommandType.StoredProcedure);


                    while (reader.Read())
                    {
                        try
                        {
                            res.Add(
                                new ReservationFields()
                                {
                                    Id = reader["Id"].ToString(),
                                    ReservationId = reader["ReservationId"].ToString(),
                                    DateReservationCreated = reader["DateReservationCreated"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    Source = reader["Source"].ToString(),
                                    DurationInDays = reader["DurationInDays"].ToString(),
                                    DateReservationStarts = reader["DateReservationStarts"].ToString(),
                                    TimeReservationStarts = reader["TimeReservationStarts"].ToString(),
                                    DateReservationEnds = reader["DateReservationEnds"].ToString(),
                                    TimeReservationEnds = reader["TimeReservationEnds"].ToString(),
                                    TotalGuests = reader["TotalGuests"].ToString(),
                                    ReservationGuestSpecialRequests = reader["ReservationGuestSpecialRequests"].ToString(),
                                    TotalAmount = reader["TotalAmount"].ToString(),
                                    TotalPaidAmount = reader["TotalPaidAmount"].ToString(),
                                    TotalRefundAmount = reader["TotalRefundAmount"].ToString(),
                                    TotalNonSecurityDepositRefundAmount = reader["TotalNonSecurityDepositRefundAmount"].ToString(),
                                    TotalSecurityDepositRefundAmount = reader["TotalSecurityDepositRefundAmount"].ToString(),
                                    TotalRatesAmount = reader["TotalRatesAmount"].ToString(),
                                    TotalTaxableFeesAmount = reader["TotalTaxableFeesAmount"].ToString(),
                                    TotalTaxesAmount = reader["TotalTaxesAmount"].ToString(),
                                    TotalNonTaxableFeesAmount = reader["TotalNonTaxableFeesAmount"].ToString(),
                                    RemainingBalance = reader["RemainingBalance"].ToString(),
                                    SecurePaymentLink = new Uri(reader["SecurePaymentLink"].ToString()),
                                    GuestId = new List<string>
                                        { reader["GuestId"].ToString()},
                                      GuestFullName = new List<string>
                                        { reader["GuestFullName"].ToString() },
                                    //GuestFullName = reader["GuestFullName"].ToString() ,
                                    GuestFirstName = reader["GuestFirstName"].ToString(),
                                    GuestLastName = reader["GuestLastName"].ToString(),
                                    GuestEmailAddress = reader["GuestEmailAddress"].ToString(),
                                    GuestPhoneNumber = reader["GuestPhoneNumber"].ToString(),
                                    PropertyId = new List<string>
                                        { reader["PropertyId"].ToString()},
                                    PropertyName = reader["PropertyName"].ToString(),
                                    PropertyPhoneNumber = reader["PropertyPhoneNumber"].ToString(),
                                    PropertyAddressLine1 = reader["PropertyAddressLine1"].ToString(),
                                    PropertyAddressLine2 = reader["PropertyAddressLine2"].ToString(),
                                    PropertyCity = reader["PropertyCity"].ToString(),
                                    PropertyStateProvince = reader["PropertyStateProvince"].ToString(),
                                    PropertyPostalCode = reader["PropertyPostalCode"].ToString(),
                                    // PropertyNeighborhood = reader["PropertyNeighborhood"].ToString(),
                                    PropertyNeighborhood = new List<string>
                                        { reader["PropertyNeighborhood"].ToString()},
                                    PropertyLatitude = reader["PropertyLatitude"].ToString(),
                                    PropertyLongitude = reader["PropertyLongitude"].ToString(),
                                    PropertyBedrooms = reader["PropertyBedrooms"].ToString(),
                                    PropertyBathrooms = reader["PropertyBathrooms"].ToString(),
                                    PropertySleeps = reader["PropertySleeps"].ToString(),
                                    PropertyInternalId = reader["PropertyInternalId"].ToString(),
                                    PropertyInternalOwnerName = reader["PropertyInternalOwnerName"].ToString(),
                                    PropertyInternalOwnerPhone = reader["PropertyInternalOwnerPhone"].ToString(),
                                    PropertyInternalOwnerEmail = reader["PropertyInternalOwnerEmail"].ToString(),

                                    GuestAddressLine1 = reader["GuestAddressLine1"].ToString(),
                                    GuestAddressLine2 = reader["GuestAddressLine2"].ToString(),
                                    GuestCity = reader["GuestCity"].ToString(),
                                    GuestStateProvince = reader["GuestStateProvince"].ToString(),
                                    GuestPostalCode = reader["GuestPostalCode"].ToString(),
                                    GuestCountry = reader["GuestCountry"].ToString(),

                                    GuestNotes = reader["GuestNotes"].ToString(),

                              

                        });
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


        public Task DeleteReservationFields(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservationFields>> GetReservationFields()
        {
            throw new NotImplementedException();
        }

        public Task<ReservationFields> GetReservationFields(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReservationFields(ReservationFields reservationFields)
        {
            throw new NotImplementedException();
        }

        public async Task SetProcessed(string reservationId)
        {
          //  string connectionString = configuration.GetConnectionString("LocalDBConnectionString");
            using (var sqlConnection = new SqlConnection(connectionString))
            {

                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //   dynamicParameters.Add("@UserId", user.UserId);
                //  dynamicParameters.Add("@UserName", user.UserName);
                //   dynamicParameters.Add("@Email", user.Email);
                // dynamicParameters.Add("@Password", user.Password);

                dynamicParameters.Add(@"ReservationId", reservationId);



                await sqlConnection.ExecuteAsync(
                    "SetProcessedReservation",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);

            }
        }
    }
}
