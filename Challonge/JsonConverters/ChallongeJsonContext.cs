using System.Text.Json.Serialization;
using Challonge.Exceptions;
using Challonge.Objects;

namespace Challonge.JsonConverters
{
	[JsonSourceGenerationOptions(
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		GenerationMode = JsonSourceGenerationMode.Metadata,
		NumberHandling = JsonNumberHandling.AllowReadingFromString,
		PropertyNameCaseInsensitive = true,
		PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
		WriteIndented = false
	)]
	[JsonSerializable(typeof(ChallongeMessage))]
	[JsonSerializable(typeof(ErrorResponse))]
	[JsonSerializable(typeof(MatchAttachmentInfo))]
	[JsonSerializable(typeof(MatchAttachmentWrapper))]
	[JsonSerializable(typeof(MatchAttachmentWrapper[]))]
	[JsonSerializable(typeof(MatchInfo))]
	[JsonSerializable(typeof(MatchWrapper))]
	[JsonSerializable(typeof(MatchWrapper[]))]
	[JsonSerializable(typeof(ParticipantInfo))]
	[JsonSerializable(typeof(ParticipantWrapper))]
	[JsonSerializable(typeof(ParticipantWrapper[]))]
	[JsonSerializable(typeof(TournamentInfo))]
	[JsonSerializable(typeof(TournamentWrapper))]
	[JsonSerializable(typeof(TournamentWrapper[]))]
	internal partial class ChallongeJsonContext : JsonSerializerContext
	{
	}
}
