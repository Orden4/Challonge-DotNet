using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challonge.JsonConverters
{
	public class CustomJsonStringEnumConverter<T> : JsonConverter<T>
		where T : struct, Enum
	{
		private static bool initialized;
		private static FrozenDictionary<T, string> namesByValue = FrozenDictionary<T, string>.Empty;
		private static FrozenDictionary<string, T> valuesByName = FrozenDictionary<string, T>.Empty;

		public CustomJsonStringEnumConverter()
		{
			if (initialized)
				return;

			lock (namesByValue)
			{
				if (initialized)
					return;

				var namesByValueDict = new Dictionary<T, string>();
				var valuesByNameDict = new Dictionary<string, T>();
				var type = typeof(T);
				var members = type.GetMembers(BindingFlags.Public | BindingFlags.Static);

				// Use attribute
				foreach (var member in members)
				{
					var name = member.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?.Name;
					var value = Enum.Parse<T>(member.Name);
					name ??= Enum.GetName<T>(value)!;
					namesByValueDict.TryAdd(value, name);
					valuesByNameDict.TryAdd(name, value);
				}

				namesByValue = namesByValueDict.ToFrozenDictionary();
				valuesByName = valuesByNameDict.ToFrozenDictionary();
				initialized = true;
			}
		}

		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return valuesByName[reader.GetString()!];
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(namesByValue[value]);
		}
	}
}
