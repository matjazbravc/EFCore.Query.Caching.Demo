using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EFCoreQueryCachingDemo.Services.Converters
{
	public class StringConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType) => true;

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			if (reader.ValueType == null)
			{
				var value = serializer.Deserialize<List<string>>(reader);
				return value?.FirstOrDefault();
			}
			if (reader.ValueType == typeof(string))
			{
				return reader.Value as string;
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value == null)
			{
				return;
			}
			JToken token = JToken.FromObject(value);
			if (token.Type != JTokenType.Object)
			{
				token.WriteTo(writer);
			}
			else
			{
				JObject @object = (JObject)token;
				IList<string> propertyNames = @object.Properties().Select(p => p.Name).ToList();
				@object.AddFirst(new JProperty("Keys", new JArray(propertyNames)));
				@object.WriteTo(writer);
			}
		}
	}
}
