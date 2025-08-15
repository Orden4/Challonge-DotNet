using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challonge.JsonConverters
{
	internal class PrerequisiteMatchIdsJsonConverter : JsonConverter<ICollection<long>>
	{
		public override ICollection<long>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			if (value == null)
				return [];

			var split = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
			var scores = new List<long>(split.Length);
			for (var i = 0; i < split.Length; i++)
			{
				scores.Add(long.Parse(split[i]));
			}
			return scores;
		}

		public override void Write(Utf8JsonWriter writer, ICollection<long> value, JsonSerializerOptions options)
		{
			if (value != null)
			{
				writer.WriteStringValue(string.Join(',', value));
			}
		}
	}
}
