using System;
using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	public class MatchAttachment : ChallongeObject
	{
		[JsonPropertyName("id")]
		public long Id { get; init; }

		[JsonPropertyName("match_id")]
		public long MatchId { get; init; }

		[JsonPropertyName("user_id")]
		public long UserId { get; init; }

		[JsonPropertyName("description")]
		public string Description { get; init; }

		[JsonPropertyName("url")]
		public string Url { get; init; }

		[JsonPropertyName("original_file_name")]
		public string OriginalFileName { get; init; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; init; }

		[JsonPropertyName("updated_at")]
		public DateTime? UpdatedAt { get; init; }

		[JsonPropertyName("asset_file_name")]
		public string AssetFileName { get; init; }

		[JsonPropertyName("asset_content_type")]
		public string AssetContentType { get; init; }

		[JsonPropertyName("asset_file_size")]
		public long? AssetFileSize { get; init; }

		[JsonPropertyName("asset_url")]
		public string AssetUrl { get; init; }

		[JsonConstructor]
		internal MatchAttachment() { }
	}
}
