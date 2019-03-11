
//
//    using RDolce;
//
//    var welcome = Welcome.FromJson(jsonString);

namespace RDolce.Property
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Property
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [JsonProperty("fields")]
        public PropertFields Fields { get; set; }
    }

    public partial class PropertFields
    {
        [JsonProperty("Property ID")]
       // [JsonConverter(typeof(ParseStringConverter))]
        public string PropertyId { get; set; }
        [JsonProperty("ID")]
     
        public string Id { get; set; }

        [JsonProperty("Property Name")]
        public string PropertyName { get; set; }

        [JsonProperty("Property Phone Number")]
        public string PropertyPhoneNumber { get; set; }

        [JsonProperty("Property Bedrooms")]
     //   [JsonConverter(typeof(ParseStringConverter))]
        public string PropertyBedrooms { get; set; }

        [JsonProperty("Property Bathrooms")]
       /// [JsonConverter(typeof(ParseStringConverter))]
        public string PropertyBathrooms { get; set; }

        [JsonProperty("Property Sleeps")]
       // [JsonConverter(typeof(ParseStringConverter))]
        public string PropertySleeps { get; set; }

        [JsonProperty("Property Internal ID")]
        public string PropertyInternalId { get; set; }

        [JsonProperty("Property Internal Owner Name")]
        public string PropertyInternalOwnerName { get; set; }

        [JsonProperty("Property Internal Owner Phone")]
        public string PropertyInternalOwnerPhone { get; set; }

        [JsonProperty("Property Internal Owner Email")]
        public string PropertyInternalOwnerEmail { get; set; }
    }

    public partial class Property
    {
        public static Property FromJson(string json) => JsonConvert.DeserializeObject<Property>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Property self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
