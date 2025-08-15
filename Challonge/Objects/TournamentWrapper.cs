using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	internal class TournamentWrapper : ChallongeObjectWrapper<Tournament>
	{
		[JsonPropertyName("tournament")]
		public override Tournament Item { get; set; }
	}
}
