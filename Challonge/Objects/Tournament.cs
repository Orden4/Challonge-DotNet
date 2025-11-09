using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	public class Tournament : ChallongeObject
	{
		public class NonEliminationData
		{
			[JsonPropertyName("current_round")]
			public int? CurrentRound { get; init; }

			[JsonPropertyName("participants_per_match")]
			[JsonConverter(typeof(NullableStringIntegerConverter))]
			public int? ParticipantsPerMatch { get; init; }

			[JsonConstructor]
			internal NonEliminationData() { }
		}

		[JsonPropertyName("id")]
		public long Id { get; init; }

		[JsonPropertyName("name")]
		public string Name { get; init; }

		[JsonPropertyName("url")]
		public string Url { get; init; }

		[JsonPropertyName("description")]
		public string Description { get; init; }

		[JsonPropertyName("tournament_type")]
		public TournamentType TournamentType { get; init; }

		[JsonPropertyName("started_at")]
		public DateTime? StartedAt { get; init; }

		[JsonPropertyName("completed_at")]
		public DateTime? CompletedAt { get; init; }

		[JsonPropertyName("require_score_agreement")]
		public bool RequireScoreAgreement { get; init; }

		[JsonPropertyName("notify_users_when_matches_open")]
		public bool NotifyUsersWhenMatchesOpen { get; init; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; init; }

		[JsonPropertyName("updated_at")]
		public DateTime? UpdatedAt { get; init; }

		[JsonPropertyName("state")]
		public TournamentState State { get; init; }

		[JsonPropertyName("open_signup")]
		public bool OpenSignup { get; init; }

		[JsonPropertyName("notify_users_when_the_tournament_ends")]
		public bool NotifyUsersWhenTheTournamentEnds { get; init; }

		[JsonPropertyName("progress_meter")]
		public int ProgressMeter { get; init; }

		[JsonPropertyName("quick_advance")]
		public bool QuickAdvance { get; init; }

		[JsonPropertyName("hold_third_place_match")]
		public bool HoldThirdPlaceMatch { get; init; }

		[JsonPropertyName("pts_for_game_win")]
		public double PtsForGameWin { get; init; }

		[JsonPropertyName("pts_for_game_tie")]
		public double PtsForGameTie { get; init; }

		[JsonPropertyName("pts_for_match_win")]
		public double PtsForMatchWin { get; init; }

		[JsonPropertyName("pts_for_match_tie")]
		public double PtsForMatchTie { get; init; }

		[JsonPropertyName("pts_for_bye")]
		public double PtsForBye { get; init; }

		[JsonPropertyName("swiss_rounds")]
		public int SwissRounds { get; init; }

		[JsonPropertyName("private")]
		public bool Private { get; init; }

		[JsonPropertyName("ranked_by")]
		public RankingMethod? RankedBy { get; init; }

		[JsonPropertyName("show_rounds")]
		public bool ShowRounds { get; init; }

		[JsonPropertyName("hide_forum")]
		public bool HideForum { get; init; }

		[JsonPropertyName("sequential_pairings")]
		public bool SequentialPairings { get; init; }

		[JsonPropertyName("accept_attachments")]
		public bool AcceptAttachments { get; init; }

		[JsonPropertyName("rr_pts_for_game_win")]
		public double RRPtsForGameWin { get; init; }

		[JsonPropertyName("rr_pts_for_game_tie")]
		public double RRPtsForGameTie { get; init; }

		[JsonPropertyName("rr_pts_for_match_win")]
		public double RRPtsForMatchWin { get; init; }

		[JsonPropertyName("rr_pts_for_match_tie")]
		public double RRPtsForMatchTie { get; init; }

		[JsonPropertyName("created_by_api")]
		public bool CreatedByApi { get; init; }

		[JsonPropertyName("credit_capped")]
		public bool CreditCapped { get; init; }

		[JsonPropertyName("category")]
		public string Category { get; init; }

		[JsonPropertyName("hide_seeds")]
		public bool HideSeeds { get; init; }

		[JsonPropertyName("prediction_method")]
		public PredictionMethod PredictionMethod { get; init; }

		[JsonPropertyName("predictions_opened_at")]
		public DateTime? PredictionsOpenedAt { get; init; }

		[JsonPropertyName("anonymous_voting")]
		public bool AnonymousVoting { get; init; }

		[JsonPropertyName("max_predictions_per_user")]
		public int MaxPredictionsPerUser { get; init; }

		[JsonPropertyName("signup_cap")]
		public int? SignupCap { get; init; }

		[JsonPropertyName("game_id")]
		public long? GameId { get; init; }

		[JsonPropertyName("participants_count")]
		public int ParticipantsCount { get; init; }

		[JsonPropertyName("group_stages_enabled")]
		public bool GroupStagesEnabled { get; init; }

		[JsonPropertyName("allow_participant_match_reporting")]
		public bool AllowParticipantMatchReporting { get; init; }

		[JsonPropertyName("teams")]
		public bool? Teams { get; init; }

		[JsonPropertyName("check_in_duration")]
		public int? CheckInDuration { get; init; }

		[JsonPropertyName("start_at")]
		public DateTime? StartAt { get; init; }

		[JsonPropertyName("started_checking_in_at")]
		public DateTime? StartedCheckingInAt { get; init; }

		[JsonPropertyName("tie_breaks")]
		public ICollection<TieBreak> TieBreaks { get; init; }

		[JsonPropertyName("locked_at")]
		public DateTime? LockedAt { get; init; }

		[JsonPropertyName("event_id")]
		public long? EventId { get; init; }

		[JsonPropertyName("public_predictions_before_start_time")]
		public bool? PublicPredictionsBeforeStartTime { get; init; }

		[JsonPropertyName("ranked")]
		public bool? Ranked { get; init; }

		[JsonPropertyName("grand_finals_modifier")]
		public GrandFinalsModifier? GrandFinalsModifier { get; init; }

		[JsonPropertyName("predict_the_losers_bracket")]
		public bool? PredictTheLosersBracket { get; init; }

		[JsonPropertyName("spam")]
		public bool? Spam { get; init; }

		[JsonPropertyName("ham")]
		public bool? Ham { get; init; }

		[JsonPropertyName("rr_iterations")]
		public int? RRIterations { get; init; }

		[JsonPropertyName("tournament_registration_id")]
		public long? TournamentRegistrationId { get; init; }

		[JsonPropertyName("donation_contest_enabled")]
		public bool? DonationContestEnabled { get; init; }

		[JsonPropertyName("mandatory_donation")]
		public bool? MandatoryDonation { get; init; }

		[JsonPropertyName("non_elimination_tournament_data")]
		public NonEliminationData NonEliminationTournamentData { get; init; }

		[JsonPropertyName("auto_assign_stations")]
		public bool? AutoAssignStations { get; init; }

		[JsonPropertyName("only_start_matches_with_stations")]
		public bool? OnlyStartMatchesWithStations { get; init; }

		[JsonPropertyName("registration_fee")]
		public double? RegistrationFee { get; init; }

		[JsonPropertyName("registration_type")]
		public string RegistrationType { get; init; }

		[JsonPropertyName("split_participants")]
		public bool? SplitParticipants { get; init; }

		[JsonPropertyName("allowed_regions")]
		public IEnumerable<string> AllowedRegions { get; init; }

		[JsonPropertyName("show_participant_country")]
		public bool? ShowParticipantCountry { get; init; }

		[JsonPropertyName("description_source")]
		public string DescriptionSource { get; init; }

		[JsonPropertyName("subdomain")]
		public string Subdomain { get; init; }

		[JsonPropertyName("full_challonge_url")]
		public string FullChallongeUrl { get; init; }

		[JsonPropertyName("live_image_url")]
		public string LiveImageUrl { get; init; }

		[JsonPropertyName("sign_up_url")]
		public string SignUpUrl { get; init; }

		[JsonPropertyName("review_before_finalizing")]
		public bool ReviewBeforeFinalizing { get; init; }

		[JsonPropertyName("accepting_predictions")]
		public bool AcceptingPredictions { get; init; }

		[JsonPropertyName("participants_locked")]
		public bool ParticipantsLocked { get; init; }

		[JsonPropertyName("game_name")]
		public string GameName { get; init; }

		[JsonPropertyName("participants_swappable")]
		public bool ParticipantsSwappable { get; init; }

		[JsonPropertyName("team_convertable")]
		public bool TeamConvertable { get; init; }

		[JsonPropertyName("group_stages_were_started")]
		public bool GroupStagesWereStarted { get; init; }

		[JsonConstructor]
		internal Tournament() { }
	}
}
