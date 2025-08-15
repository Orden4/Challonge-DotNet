using System.Text.Json.Serialization;
using Challonge.JsonConverters;

namespace Challonge.Objects
{
	[Wrapper("participant")]
	public class ParticipantInfo : ChallongeObjectInfo
	{
		[JsonPropertyName("invite_name_or_email"), JsonInclude]
		internal string? InviteNameOrEmail { get; set; }

		private string? challongeUsername;
		[JsonPropertyName("challonge_username")]
		public string? ChallongeUsername
		{
			get => this.challongeUsername;
			set
			{
				this.challongeUsername = value;
				if (string.IsNullOrWhiteSpace(this.email))
					InviteNameOrEmail = value;
			}
		}

		private string? email;
		[JsonPropertyName("email")]
		public string? Email
		{
			get => this.email;
			set
			{
				this.email = value;
				InviteNameOrEmail = value;
			}
		}

		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("seed")]
		public int? Seed { get; set; }

		[JsonPropertyName("misc")]
		public string? Misc { get; set; }
	}
}
