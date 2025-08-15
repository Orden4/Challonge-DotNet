using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	[JsonConverter(typeof(CustomJsonStringEnumConverter<TournamentState>))]
	public enum TournamentState
	{
		[JsonStringEnumMemberName("all")]
		All,
		[JsonStringEnumMemberName("pending")]
		Pending,
		[JsonStringEnumMemberName("in_progress")]
		InProgress,
		[JsonStringEnumMemberName("underway")]
		Underway,
		[JsonStringEnumMemberName("ended")]
		Ended,
		[JsonStringEnumMemberName("checking_in")]
		CheckingIn,
		[JsonStringEnumMemberName("checked_in")]
		CheckedIn,
		[JsonStringEnumMemberName("accepting_predictions")]
		AcceptingPredictions,
		[JsonStringEnumMemberName("awaiting_review")]
		AwaitingReview,
		[JsonStringEnumMemberName("complete")]
		Complete
	}
	[JsonConverter(typeof(CustomJsonStringEnumConverter<TournamentType>))]
	public enum TournamentType
	{
		[JsonStringEnumMemberName("single elimination")]
		SingleElimination,
		[JsonStringEnumMemberName("double elimination")]
		DoubleElimination,
		[JsonStringEnumMemberName("round robin")]
		RoundRobin,
		[JsonStringEnumMemberName("swiss")]
		Swiss,
		[JsonStringEnumMemberName("free for all")]
		FreeForAll,
		[JsonStringEnumMemberName("leaderboard")]
		Leaderboard,
		[JsonStringEnumMemberName("time trial")]
		TimeTrial,
		[JsonStringEnumMemberName("single race")]
		SingleRace,
		[JsonStringEnumMemberName("grand prix")]
		GrandPrix,
	}
	[JsonConverter(typeof(CustomJsonStringEnumConverter<RankingMethod>))]
	public enum RankingMethod
	{
		[JsonStringEnumMemberName("match wins")]
		MatchWins,
		[JsonStringEnumMemberName("game wins")]
		GameWins,
		[JsonStringEnumMemberName("points scored")]
		PointsScored,
		[JsonStringEnumMemberName("points difference")]
		PointsDifference,
		[JsonStringEnumMemberName("custom")]
		Custom
	}
	public enum PredictionMethod
	{
		Default,
		ExponentialScoring,
		LinearScoring
	}
	[JsonConverter(typeof(CustomJsonStringEnumConverter<GrandFinalsModifier>))]
	public enum GrandFinalsModifier
	{
		[JsonStringEnumMemberName("single match")]
		SingleMatch,
		[JsonStringEnumMemberName("skip")]
		Skip
	}
	[JsonConverter(typeof(CustomJsonStringEnumConverter<MatchState>))]
	public enum MatchState
	{
		[JsonStringEnumMemberName("all")]
		All,
		[JsonStringEnumMemberName("pending")]
		Pending,
		[JsonStringEnumMemberName("open")]
		Open,
		[JsonStringEnumMemberName("complete")]
		Complete
	}

	[JsonConverter(typeof(CustomJsonStringEnumConverter<TieBreak>))]
	public enum TieBreak
	{
		[JsonStringEnumMemberName("match wins")]
		MatchWins,
		[JsonStringEnumMemberName("game wins")]
		GameWins,
		[JsonStringEnumMemberName("game win percentage")]
		GameWinPercentage,
		[JsonStringEnumMemberName("points scored")]
		PointsScored,
		[JsonStringEnumMemberName("points difference")]
		PointsDifference,
		[JsonStringEnumMemberName("match wins vs tied")]
		MatchWinsVsTied,
		[JsonStringEnumMemberName("median buchholz")]
		MedianBuchholz
	}
}
