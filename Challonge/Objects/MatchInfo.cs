using System.Collections.Generic;
using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	[Wrapper("match")]
	public class MatchInfo : ChallongeObjectInfo
	{
		[JsonPropertyName("winner_id"), JsonInclude]
		internal string? WinnerIdElement { get; set; }

		[JsonIgnore]
		public long? WinnerId
		{
			get => long.TryParse(WinnerIdElement, out var value) ? value : null;
			set => WinnerIdElement = value.ToString()!;
		}

		[JsonIgnore]
		public bool ResultIsTie
		{
			get => WinnerIdElement == "tie";
			set
			{
				if (value)
				{
					WinnerIdElement = "tie";
				}
				else if (!WinnerId.HasValue)
				{
					WinnerIdElement = null;
				}
			}
		}

		[JsonPropertyName("scores_csv")]
		[JsonConverter(typeof(ScoresJsonConverter))]
		public ICollection<Score>? Scores { get; set; }

		[JsonPropertyName("player1_votes")]
		public int? PlayerOneVotes { get; set; }

		[JsonPropertyName("player2_votes")]
		public int? PlayerTwoVotes { get; set; }
	}
}
