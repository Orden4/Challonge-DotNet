using System;

namespace Challonge.JsonConverters
{
	[AttributeUsage(AttributeTargets.Class)]
	public class WrapperAttribute(string name) : Attribute
	{
		public string Name { get; } = name;
	}
}
