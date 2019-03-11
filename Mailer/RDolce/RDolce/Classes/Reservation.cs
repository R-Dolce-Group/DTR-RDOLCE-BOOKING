using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce
{
    public partial class Reservation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [JsonProperty("fields")]
        public ReservationFields Fields { get; set; }
    }

    public partial class ReservationFields
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Reservation ID")]
        public string ReservationId { get; set; }

        [JsonProperty("Date Reservation Created")]
        public string DateReservationCreated { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Duration in Days")]
       // [JsonConverter(typeof(ParseStringConverter))]
        public string DurationInDays { get; set; }

        [JsonProperty("Date Reservation Starts")]
        public string DateReservationStarts { get; set; }

        [JsonProperty("Time Reservation Starts")]
        public string TimeReservationStarts { get; set; }

        [JsonProperty("Date Reservation Ends")]
        public string DateReservationEnds { get; set; }

        [JsonProperty("Time Reservation Ends")]
        public string TimeReservationEnds { get; set; }

        [JsonProperty("Total Guests")]
       // [JsonConverter(typeof(ParseStringConverter))]
        public string TotalGuests { get; set; }

        [JsonProperty("Reservation Guest Special Requests")]
        public string ReservationGuestSpecialRequests { get; set; }

        [JsonProperty("Total Amount")]
        public string TotalAmount { get; set; }

        [JsonProperty("Total Paid Amount")]
        public string TotalPaidAmount { get; set; }

        [JsonProperty("Total Refund Amount")]
        public string TotalRefundAmount { get; set; }

        [JsonProperty("Total Non-Security Deposit Refund Amount")]
        public string TotalNonSecurityDepositRefundAmount { get; set; }

        [JsonProperty("Total Security Deposit Refund Amount")]
        public string TotalSecurityDepositRefundAmount { get; set; }

        [JsonProperty("Total Rates Amount")]
        public string TotalRatesAmount { get; set; }

        [JsonProperty("Total Taxable Fees Amount")]
        public string TotalTaxableFeesAmount { get; set; }

        [JsonProperty("Total Taxes Amount")]
        public string TotalTaxesAmount { get; set; }

        [JsonProperty("Total Non-taxable Fees Amount")]
        public string TotalNonTaxableFeesAmount { get; set; }

        [JsonProperty("Remaining Balance")]
        public string RemainingBalance { get; set; }

        [JsonProperty("Secure Payment Link")]
        public Uri SecurePaymentLink { get; set; }

        [JsonProperty("Guest ID")]
       // [JsonConverter(typeof(ParseStringConverter))]
        public List<string> GuestId { get; set; }

        [JsonProperty("Guest Full Name")]
         public List<string> GuestFullName { get; set; }
//        public string GuestFullName { get; set; }

        [JsonProperty("Guest First Name")]
        public string GuestFirstName { get; set; }

        [JsonProperty("Guest Last Name")]
        public string GuestLastName { get; set; }

        [JsonProperty("Guest Email Address")]
        public string GuestEmailAddress { get; set; }

        [JsonProperty("Guest Phone Number")]
        public string GuestPhoneNumber { get; set; }

       

        [JsonProperty("Property ID")]
        public List<string> PropertyId { get; set; }

        [JsonProperty("Property Name")]
        public string PropertyName { get; set; }

        [JsonProperty("Property Phone Number")]
        public string PropertyPhoneNumber { get; set; }

        [JsonProperty("Property Address Line 1")]
        public string PropertyAddressLine1 { get; set; }

        [JsonProperty("Property Address Line 2")]
        public string PropertyAddressLine2 { get; set; }

        [JsonProperty("Property City")]
        public string PropertyCity { get; set; }

        [JsonProperty("Property State/Province")]
        public string PropertyStateProvince { get; set; }

        [JsonProperty("Property Postal Code")]
        public string PropertyPostalCode { get; set; }

        [JsonProperty("Property Neighborhood")]
         public List<string> PropertyNeighborhood { get; set; }
      //  public string PropertyNeighborhood { get; set; }

        [JsonProperty("Property Latitude")]
        public string PropertyLatitude { get; set; }

        [JsonProperty("Property Longitude")]
        public string PropertyLongitude { get; set; }

        [JsonProperty("Property Bedrooms")]
        public string PropertyBedrooms { get; set; }

        [JsonProperty("Property Bathrooms")]
        public string PropertyBathrooms { get; set; }

        [JsonProperty("Property Sleeps")]
        public string PropertySleeps { get; set; }

        [JsonProperty("Property Internal ID")]
        public string PropertyInternalId { get; set; }

        [JsonProperty("Property Internal Owner Name")]
        public string PropertyInternalOwnerName { get; set; }

        [JsonProperty("Property Internal Owner Phone")]
        public string PropertyInternalOwnerPhone { get; set; }

        [JsonProperty("Property Internal Owner Email")]
        public string PropertyInternalOwnerEmail { get; set; }

        [JsonProperty("Processed")]
        public bool Processed { get; set; }

        [JsonProperty("Guest Address Line 1")]
        public string GuestAddressLine1 { get; set; }

        [JsonProperty("Guest Address Line 2")]
        public string GuestAddressLine2 { get; set; }

        [JsonProperty("Guest City")]
        public string GuestCity { get; set; }


        [JsonProperty("Guest State/Province")]
        public string GuestStateProvince { get; set; }

        [JsonProperty("Guest Postal Code")]
        public string GuestPostalCode{ get; set; }

        [JsonProperty("Guest Country")]
        public string GuestCountry { get; set; }


        [JsonProperty("Guest Message/Notes")]
        public string GuestNotes { get; set; }

    }

    public partial class Reservation
    {
        public static Reservation FromJson(string json) => JsonConvert.DeserializeObject<Reservation>(json, Converter.Settings);
    }

    public partial class ReservationFields
    {
        public static ReservationFields FromJson(string json) => JsonConvert.DeserializeObject<ReservationFields>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Reservation self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
