﻿using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	internal class ChallongeMessage
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
