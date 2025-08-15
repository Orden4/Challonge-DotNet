using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	public class Match : ChallongeObject
	{
		[JsonPropertyName("id")]
		public long Id { get; init; }

		[JsonPropertyName("tournament_id")]
		public long TournamentId { get; init; }

		[JsonPropertyName("state")]
		public MatchState State { get; init; }

		[JsonPropertyName("player1_id")]
		public long? Player1Id { get; init; }

		[JsonPropertyName("player2_id")]
		public long? Player2Id { get; init; }

		[JsonPropertyName("player1_prereq_match_id")]
		public long? Player1PrereqMatchId { get; init; }

		[JsonPropertyName("player2_prereq_match_id")]
		public long? Player2PrereqMatchId { get; init; }

		[JsonPropertyName("player1_is_prereq_match_loser")]
		public bool Player1IsPrereqMatchLoser { get; init; }

		[JsonPropertyName("player2_is_prereq_match_loser")]
		public bool Player2IsPrereqMatchLoser { get; init; }

		[JsonPropertyName("winner_id")]
		public long? WinnerId { get; init; }

		[JsonPropertyName("loser_id")]
		public long? LoserId { get; init; }

		[JsonPropertyName("started_at")]
		public DateTime? StartedAt { get; init; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; init; }

		[JsonPropertyName("updated_at")]
		public DateTime? UpdatedAt { get; init; }

		[JsonPropertyName("identifier")]
		public string Identifier { get; init; }

		[JsonPropertyName("has_attachment")]
		public bool HasAttachment { get; init; }

		[JsonPropertyName("round")]
		public int Round { get; init; }

		[JsonPropertyName("player1_votes")]
		public int? Player1Votes { get; init; }

		[JsonPropertyName("player2_votes")]
		public int? Player2Votes { get; init; }

		[JsonPropertyName("group_id")]
		public long? GroupId { get; init; }

		[JsonPropertyName("attachment_count")]
		public int? AttachmentCount { get; init; }

		[JsonPropertyName("scheduled_time")]
		public DateTime? ScheduledTime { get; init; }

		[JsonPropertyName("location")]
		public string Location { get; init; }

		[JsonPropertyName("underway_at")]
		public DateTime? UnderwayAt { get; init; }

		[JsonPropertyName("optional")]
		public bool? Optional { get; init; }

		[JsonPropertyName("rushb_id")]
		public long? RushbId { get; init; }

		[JsonPropertyName("completed_at")]
		public DateTime? CompletedAt { get; init; }

		[JsonPropertyName("suggested_play_order")]
		public int? SuggestedPlayOrder { get; init; }

		[JsonPropertyName("forfeited")]
		public bool? Forfeited { get; init; }

		[JsonPropertyName("open_graph_image_file_name")]
		public string OpenGraphImageFileName { get; init; }

		[JsonPropertyName("open_graph_image_content_type")]
		public string OpenGraphImageContentType { get; init; }

		[JsonPropertyName("open_graph_image_file_size")]
		public double? OpenGraphImageFileSize { get; init; }

		[JsonPropertyName("prerequisite_match_ids_csv")]
		[JsonConverter(typeof(PrerequisiteMatchIdsJsonConverter))]
		public ICollection<long> PrerequisiteMatchIds { get; init; }

		[JsonPropertyName("scores_csv")]
		[JsonConverter(typeof(ScoresJsonConverter))]
		public ICollection<Score> Scores { get; init; }

		[JsonConstructor]
		internal Match() { }
	}
}
