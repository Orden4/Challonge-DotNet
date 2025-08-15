using System;

namespace Challonge.JsonConverters
{
	[AttributeUsage(AttributeTargets.Field)]
	public class JsonStringEnumMemberNameAttribute(string name) : Attribute
	{
		public string Name { get; } = name;
	}
}
