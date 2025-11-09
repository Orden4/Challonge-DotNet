using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challonge.JsonConverters
{
	internal class NullableStringIntegerConverter : JsonConverter<int?>
	{
		public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var strValue = reader.GetString();
				if (int.TryParse(strValue, out var value))
					return value;
			}
			else if (reader.TokenType == JsonTokenType.Number)
			{
				return reader.GetInt32();
			}


			return null;
		}

		public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
		{
			if (value.HasValue)
			{
				writer.WriteNumberValue(value.Value);
			}
		}
	}
}
