using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Challonge.Objects
{
	public class Participant : ChallongeObject
	{
		[JsonPropertyName("id")]
		public long Id { get; init; }

		[JsonPropertyName("tournament_id")]
		public long TournamentId { get; init; }

		[JsonPropertyName("name")]
		public string Name { get; init; }

		[JsonPropertyName("seed")]
		public int Seed { get; init; }

		[JsonPropertyName("active")]
		public bool Active { get; init; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; init; }

		[JsonPropertyName("updated_at")]
		public DateTime? UpdatedAt { get; init; }

		[JsonPropertyName("invite_email")]
		public string InviteEmail { get; init; }

		[JsonPropertyName("final_rank")]
		public int? FinalRank { get; init; }

		[JsonPropertyName("misc")]
		public string Misc { get; init; }

		[JsonPropertyName("icon")]
		public string Icon { get; init; }

		[JsonPropertyName("on_waiting_list")]
		public bool OnWaitingList { get; init; }

		[JsonPropertyName("invitation_id")]
		public long? InvitationId { get; init; }

		[JsonPropertyName("group_id")]
		public long? GroupId { get; init; }

		[JsonPropertyName("checked_in_at")]
		public DateTime? CheckedInAt { get; init; }

		[JsonPropertyName("ranked_member_id")]
		public long? RankedMemberId { get; init; }

		[JsonPropertyName("custom_field_response")]
		public string CustomFieldResponse { get; init; }

		[JsonPropertyName("clinch")]
		public string Clinch { get; init; }

		[JsonPropertyName("integration_uids")]
		public IEnumerable<string> IntegrationUids { get; init; }

		[JsonPropertyName("challonge_username")]
		public string ChallongeUsername { get; init; }

		[JsonPropertyName("challonge_email_address_verified")]
		public bool? ChallongeEmailAddressVerified { get; init; }

		[JsonPropertyName("removable")]
		public bool Removable { get; init; }

		[JsonPropertyName("participatable_or_invitation_attached")]
		public bool ParticipatableOrInvitationAttached { get; init; }

		[JsonPropertyName("confirm_remove")]
		public bool ConfirmRemove { get; init; }

		[JsonPropertyName("invitation_pending")]
		public bool InvitationPending { get; init; }

		[JsonPropertyName("display_name_with_invitation_email_address")]
		public string DisplayNameWithInvitationEmailAddress { get; init; }

		[JsonPropertyName("email_hash")]
		public string EmailHash { get; init; }

		[JsonPropertyName("username")]
		public string Username { get; init; }

		[JsonPropertyName("display_name")]
		public string DisplayName { get; init; }

		[JsonPropertyName("attached_participatable_portrait_url")]
		public string AttachedParticipatablePortraitUrl { get; init; }

		[JsonPropertyName("can_check_in")]
		public bool CanCheckIn { get; init; }

		[JsonPropertyName("checked_in")]
		public bool CheckedIn { get; init; }

		[JsonPropertyName("reactivatable")]
		public bool Reactivatable { get; init; }

		[JsonPropertyName("check_in_open")]
		public bool CheckInOpen { get; init; }

		[JsonPropertyName("group_player_ids")]
		public IEnumerable<int> GroupPlayerIds { get; init; }

		[JsonPropertyName("has_irrelevant_seed")]
		public bool HasIrrelevantSeed { get; init; }

		[JsonConstructor]
		internal Participant() { }
	}
}
