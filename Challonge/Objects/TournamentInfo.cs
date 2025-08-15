using System;
using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	[Wrapper("tournament")]
	public class TournamentInfo : ChallongeObjectInfo
	{
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("tournament_type")]
		public TournamentType? TournamentType { get; set; }

		[JsonPropertyName("url")]
		public string? Url { get; set; }

		[JsonPropertyName("subdomain")]
		public string? Subdomain { get; set; }

		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonPropertyName("open_signup")]
		public bool? OpenSignup { get; set; }

		[JsonPropertyName("hold_third_place_match")]
		public bool? HoldThirdPlaceMatch { get; set; }

		[JsonPropertyName("pts_for_match_win")]
		public double? PtsForMatchWin { get; set; }

		[JsonPropertyName("pts_for_match_tie")]
		public double? PtsForMatchTie { get; set; }

		[JsonPropertyName("pts_for_game_win")]
		public double? PtsForGameWin { get; set; }

		[JsonPropertyName("pts_for_game_tie")]
		public double? PtsForGameTie { get; set; }

		[JsonPropertyName("pts_for_bye")]
		public double? PtsForBye { get; set; }

		[JsonPropertyName("swiss_rounds")]
		public int? SwissRounds { get; set; }

		[JsonPropertyName("ranked_by")]
		public RankingMethod? RankedBy { get; set; }

		[JsonPropertyName("rr_pts_for_match_win")]
		public double? RRPtsForMatchWin { get; set; }

		[JsonPropertyName("rr_pts_for_match_tie")]
		public double? RRPtsForMatchTie { get; set; }

		[JsonPropertyName("rr_pts_for_game_win")]
		public double? RRPtsForGameWin { get; set; }

		[JsonPropertyName("rr_pts_for_game_tie")]
		public double? RRPtsForGameTie { get; set; }

		[JsonPropertyName("accept_attachments")]
		public bool? AcceptAttachments { get; set; }

		[JsonPropertyName("hide_forum")]
		public bool? HideForum { get; set; }

		[JsonPropertyName("show_rounds")]
		public bool? ShowRounds { get; set; }

		[JsonPropertyName("private")]
		public bool? Private { get; set; }

		[JsonPropertyName("notify_users_when_matches_open")]
		public bool? NotifyUsersWhenMatchesOpen { get; set; }

		[JsonPropertyName("notify_users_when_the_tournament_ends")]
		public bool? NotifyUsersWhenTheTournamentEnds { get; set; }

		[JsonPropertyName("sequential_pairings")]
		public bool? SequentialPairings { get; set; }

		[JsonPropertyName("signup_cap")]
		public int? SignupCap { get; set; }

		[JsonPropertyName("start_at")]
		public DateTime? StartAt { get; set; }

		[JsonPropertyName("check_in_duration")]
		public int? CheckInDuration { get; set; }

		[JsonPropertyName("prediction_method")]
		public PredictionMethod? PredictionMethod { get; set; }

		[JsonPropertyName("grand_finals_modifier")]
		public GrandFinalsModifier? GrandFinalsModifier { get; set; }

		[JsonPropertyName("game_name")]
		public string? GameName { get; set; }
	}
}
