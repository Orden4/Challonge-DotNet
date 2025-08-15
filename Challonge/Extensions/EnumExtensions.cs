using System;
using System.Linq;
using System.Reflection;
using Challonge.JsonConverters;

namespace Challonge.Extensions.Internal
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
