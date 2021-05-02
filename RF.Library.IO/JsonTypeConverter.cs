namespace RF.Library.IO
{
    using System;
    using Newtonsoft.Json;

    public class JsonAbstractTypeConverter<TReal, TAbstract> : JsonConverter where TReal : TAbstract
    {
        public override bool CanConvert(Type objectType)
        {
            var msg = string.Format("This converter should be applied directly with [JsonProperty(ItemConverterType = typeof(TypeConverter<{0}>))] or [JsonProperty(typeof(TypeConverter<{0}>))]",
                typeof(TAbstract));
            throw new NotImplementedException(msg);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<TReal>(reader);
        }
    }
}