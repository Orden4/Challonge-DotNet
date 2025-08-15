using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	internal class MatchAttachmentWrapper : ChallongeObjectWrapper<MatchAttachment>
	{
		[JsonPropertyName("match_attachment")]
		public override MatchAttachment Item { get; set; }
	}
}
