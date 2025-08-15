using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	[Wrapper("match_attachment")]
	public class MatchAttachmentInfo : ChallongeObjectInfo
	{
		[JsonIgnore]
		public MatchAttachmentAsset? Asset { get; set; }

		[JsonPropertyName("url")]
		public string? Url { get; set; }

		[JsonPropertyName("description")]
		public string? Description { get; set; }
	}
}
