using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirtableApiClient;
using RDolce.Interfaces;
using RDolce.DataProvider;
using Mandrill;
using Mandrill.Model;
using RDolce.Property;
using Hangfire;

namespace RDolce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestartController : ControllerBase
    {

        private IReservationFieldsDataProvider reservationsDataProvider;

        private IUnitFieldsDataProvider unitFieldDataProvider;

        private IPropertyDataProvider propertyFieldDataProvider;


        readonly string baseId = "appEe2e6HgYUdSseT";
        readonly string appKey = "keyhSgf5ofHYLZFqx";

        readonly  string Reservations = "00.ReservationsListings";
        readonly  string Communities = "HOA Communities";
        readonly  string Units = "Units";

        readonly static string Properties = "02.PropertiesLookUp";

        public RestartController(IReservationFieldsDataProvider _reservationsDataProvider, IUnitFieldsDataProvider udt, IPropertyDataProvider _propertyFieldDataProvider)
        {
            this.reservationsDataProvider = _reservationsDataProvider;
            this.unitFieldDataProvider = udt;
            this.propertyFieldDataProvider = _propertyFieldDataProvider;
        }


        // GET api/restart
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
           
            AirtableBase airtableBase1 = new AirtableBase(appKey, baseId);

            //  GetReservations();
            //  GetProperties();

           RecurringJob.AddOrUpdate(
            () => GetReservations(),
            Cron.MinuteInterval(1));

                    RecurringJob.AddOrUpdate(
           () => GetProperties(),
           Cron.MinuteInterval(2));

            return "Restarted....";
        }

        public async void Process (ReservationFields data)
        {
            if (data.Processed == false)
            {
                try
                {
                    var prop = await propertyFieldDataProvider.GetPropertyFields(data.PropertyId[0]);
                    SendNotificationEmail(data, prop);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async void SendNotificationEmail(ReservationFields data, PropertFields prop)
        {

            var api = new MandrillApi("ihU2venNJKDKgXmC8AuKBg");
            var message = new MandrillMessage();
            message.FromEmail = "contact@rdolcegroup.com";
           // message.AddTo(prop.PropertyInternalOwnerEmail);
            message.AddTo("a.zayets@gmail.com");
         //   message.AddTo("Marti@rdolcegroup.com");
            message.AddTo("alex@devteam.space");
            //  message.ReplyTo = "contact@rdolcegroup.com";

            message.AddGlobalMergeVars("SUBJECT", prop.PropertyName);


            //supports merge var content as string
            message.AddGlobalMergeVars("WC_PROPERTY_NAME", prop.PropertyName);
            message.AddGlobalMergeVars("X_GUEST_EMAIL", data.GuestEmailAddress);
            message.AddGlobalMergeVars("X_GUEST_NAME", data.GuestFirstName + " " + data.GuestLastName);
            message.AddGlobalMergeVars("X_GUEST_PHONE", data.GuestPhoneNumber);

            message.AddGlobalMergeVars("X_GUEST_ADDRESS1", string.IsNullOrEmpty(data.GuestAddressLine1) ? string.Empty : data.GuestAddressLine1);
            message.AddGlobalMergeVars("X_GUEST_ADDRESS2", string.IsNullOrEmpty(data.GuestAddressLine2) ? string.Empty : data.GuestAddressLine2);
            message.AddGlobalMergeVars("X_GUEST_CITY", string.IsNullOrEmpty(data.GuestCity) ? string.Empty : data.GuestCity);
            message.AddGlobalMergeVars("X_GUEST_STATE", string.IsNullOrEmpty(data.GuestStateProvince) ? string.Empty : data.GuestStateProvince);

            message.AddGlobalMergeVars("X_GUEST_POSTAL_CODE", string.IsNullOrEmpty(data.GuestPostalCode) ? string.Empty : data.GuestPostalCode);
            message.AddGlobalMergeVars("X_GUEST_COUNTRY", string.IsNullOrEmpty(data.GuestCountry) ? string.Empty : data.GuestCountry);



            message.AddGlobalMergeVars("X_START_DATE", data.DateReservationStarts);
            message.AddGlobalMergeVars("X_START_TIME", string.IsNullOrEmpty(data.TimeReservationStarts)?string.Empty: "@ " + data.TimeReservationStarts);
            message.AddGlobalMergeVars("X_END_DATE", data.DateReservationEnds);
            message.AddGlobalMergeVars("X_END_TIME", string.IsNullOrEmpty(data.TimeReservationEnds)?string.Empty :"@ " + data.TimeReservationEnds);
            message.AddGlobalMergeVars("X_TOTAL_GUESTS", data.TotalGuests);
            message.AddGlobalMergeVars("X_GUEST_NOTES", data.GuestNotes);


            //or as objects (handlebar templates only)
            //   message.AddRcptMergeVars("a.zayets@gmail.com", "WC_PROPERTY_NAME", "TEST PROPERTY NAME");

            var res = await api.Messages.SendTemplateAsync(message, "guest-inquiry");
            if ((res[0].Status == MandrillSendMessageResponseStatus.Sent) || ((res[0].Status == MandrillSendMessageResponseStatus.Queued)))
            {
                reservationsDataProvider.SetProcessed(data.Id);
            }

        }

        public async Task GetProperties()
        {
            string offset = null;
            string errorMessage = null;
            var records = new List<AirtableRecord>();
            List<string> fieldsArray = new List<string>();
            string filterByFormula = string.Empty;
            int? maxRecords = 1000;
            int? pageSize = 100;
            List<Sort> sort = new List<Sort>();
            string view = string.Empty;
            records.Clear();
            using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
            {
                //
                // Use 'offset' and 'pageSize' to specify the records that you want
                // to retrieve.
                // Only use a 'do while' loop if you want to get multiple pages
                // of records.
                //
                records.Clear();
                do
                {
                    Task<AirtableListRecordsResponse> task = airtableBase.ListRecords(
                               Properties, offset, fieldsArray,
                             filterByFormula,
                   maxRecords,
                   pageSize,
                   sort,
                   view
                           );

                    AirtableListRecordsResponse response = await task;

                    if (response.Success)
                    {
                        // records.Clear();
                        records.AddRange(response.Records.ToList());
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                        break;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                        break;
                    }
                } while (offset != null);



                foreach (var item in records)
                {
                    try
                    {
                        var output = Newtonsoft.Json.JsonConvert.SerializeObject(item);

                        RDolce.Property.Property res = RDolce.Property.Property.FromJson(output);
                        res.Fields.Id = item.Id;
                       
                        propertyFieldDataProvider.AddPropertyFields(res.Fields);
                    }
                    catch (Exception ex)
                    {
                    }
                    // Do something with the retrieved 'records' and the 'offset'
                    // for the next page of the record list.


                    //  SendEmail();



                }
            }

            var a = await reservationsDataProvider.GetUnProcessed();

            foreach (var item in a)
            {
                Process(item);
                
            }
        }


        public async Task GetReservations()
        {
            string offset = null;
            string errorMessage = null;
            var records = new List<AirtableRecord>();
            List<string> fieldsArray = new List<string>();
            string filterByFormula = string.Empty;
            int? maxRecords = 1000;
            int? pageSize = 100;
            List<Sort> sort = new List<Sort>();
            string view = string.Empty;
            records.Clear();

            using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
            {
                //
                // Use 'offset' and 'pageSize' to specify the records that you want
                // to retrieve.
                // Only use a 'do while' loop if you want to get multiple pages
                // of records.
                //

                do
                {
                    Task<AirtableListRecordsResponse> task = airtableBase.ListRecords(
                           Reservations,
                           offset, fieldsArray,
                             filterByFormula,
                   maxRecords,
                   pageSize,
                   sort,
                   view
                           );

                    AirtableListRecordsResponse response = await task;

                    if (response.Success)
                    {

                        records.AddRange(response.Records.ToList());
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                        break;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                        break;
                    }
                } while (offset != null);
            }
            string temp;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Error reporting
            }
            else
            {
                foreach (var item in records)
                {
                    try
                    {
                        var output = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                        temp = output;
                        Reservation res = Reservation.FromJson(output);
                        res.Fields.Id = item.Id;
                        // GetProperty(res);
                        reservationsDataProvider.AddReservationFields(res.Fields);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                // Do something with the retrieved 'records' and the 'offset'
                // for the next page of the record list.
            }
        }

        public async void SendEmail()
        {
           
        }
    }
}
