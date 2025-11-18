using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	internal partial class ChallongeMessage
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
