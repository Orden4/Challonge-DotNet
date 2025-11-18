using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Challonge.Extensions
{
	internal static class EnumExtensions
	{
		internal static string GetEnumMemberValue(this Enum e)
		{
			var value = e.ToString();

			var attribute = e.GetType()
				.GetMember(value)
				.Single()
				.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()
				?.Name;

			return attribute ?? value;
		}
	}
}
