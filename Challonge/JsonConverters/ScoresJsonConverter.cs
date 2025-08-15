using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Challonge.Objects;

namespace Challonge.JsonConverters
{
	internal class ScoresJsonConverter : JsonConverter<ICollection<Score>>
	{
		public override ICollection<Score>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			if (value == null)
				return [];

			var split = value.Split((char[])[',', '-'], StringSplitOptions.RemoveEmptyEntries);
			var scores = new List<Score>(split.Length / 2);
			for (var i = 0; i < split.Length - 1; i += 2)
			{
				scores.Add(new(int.Parse(split[i]), int.Parse(split[i + 1])));
			}
			return scores;
		}

		public override void Write(Utf8JsonWriter writer, ICollection<Score> value, JsonSerializerOptions options)
		{
			if (value != null)
			{
				writer.WriteStringValue(string.Join(',', value.Select(s => $"{s.PlayerOneScore}-{s.PlayerTwoScore}")));
			}
		}
	}
}
