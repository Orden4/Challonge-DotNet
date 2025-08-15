using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	internal class MatchWrapper : ChallongeObjectWrapper<Match>
	{
		[JsonPropertyName("match")]
		public override Match Item { get; set; }
	}
}
