using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	internal class ParticipantWrapper : ChallongeObjectWrapper<Participant>
	{
		[JsonPropertyName("participant")]
		public override Participant Item { get; set; }
	}
}
