using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Challonge.Exceptions;
using Challonge.Extensions.Internal;
using Challonge.Helpers;
using Challonge.Objects;

namespace Challonge.Api
{
	/// <inheritdoc cref="IChallongeClient"/>
	public sealed class ChallongeClient : IChallongeClient
	{
		internal static JsonSerializerOptions JsonSerializerOptions { get; } = new()
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
			NumberHandling = JsonNumberHandling.AllowReadingFromString,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		};
		private readonly HttpClient client;

		public ChallongeClient(HttpClient client, IChallongeCredentials credentials)
		{
			client.BaseAddress = new Uri("https://api.challonge.com/v1/");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic",
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credentials.Username}:{credentials.ApiKey}"))
			);
			this.client = client;
		}

		private async Task<TReturn> SendRequestAsync<TReturn>(string relativeUrl, HttpMethod method, object? parameters = null)
		{
			using var request = RequestBuilder.BuildRequest(relativeUrl, method, parameters);
			using var response = await this.client.SendAsync(request);

			var responseText = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
				throw new ChallongeException(responseText, response.StatusCode);

			return JsonSerializer.Deserialize<TReturn>(responseText, JsonSerializerOptions)!;
		}

		public async Task<IEnumerable<Tournament>> GetTournamentsAsync(TournamentState? state = null, TournamentType? type = null,
			DateTime? createdAfter = null, DateTime? createdBefore = null, string? subdomain = null)
		{
			Dictionary<string, object?> parameters = new()
			{
				{ "state", state?.GetEnumMemberValue() },
				{ "type", type?.GetEnumMemberValue() },
				{ "created_after", createdAfter?.ToString("yyyy-MM-dd") },
				{ "created_before", createdBefore?.ToString("yyyy-MM-dd") },
				{ "subdomain", subdomain }
			};

			var wrappers = await SendRequestAsync<TournamentWrapper[]>(
				"tournaments.json",
				HttpMethod.Get,
				parameters
			);
			return wrappers.Select(w => w.Item);
		}

		public async Task<Tournament> CreateTournamentAsync(TournamentInfo tournamentInfo)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				"tournaments.json",
				HttpMethod.Post,
				tournamentInfo
			);

			return wrapper.Item;
		}

		public async Task<Tournament> GetTournamentByUrlAsync(string url)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{url}.json",
				HttpMethod.Get
			);

			return wrapper.Item;
		}

		public Task<Tournament> GetTournamentByIdAsync(long id)
		{
			return GetTournamentByUrlAsync(id.ToString());
		}

		public async Task<Tournament> UpdateTournamentAsync(string tournament, TournamentInfo tournamentInfo)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}.json",
				HttpMethod.Put,
				tournamentInfo
			);

			return wrapper.Item;
		}

		public Task<Tournament> UpdateTournamentAsync(Tournament tournament, TournamentInfo tournamentInfo)
		{
			return UpdateTournamentAsync(tournament.Id.ToString(), tournamentInfo);
		}

		public async Task DeleteTournamentAsync(string tournament)
		{
			await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}.json",
				HttpMethod.Delete
			);
		}

		public Task DeleteTournamentAsync(Tournament tournament)
		{
			return DeleteTournamentAsync(tournament.Id.ToString());
		}

		public async Task<Tournament> ProcessTournamentCheckInsAsync(string tournament)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}/process_check_ins.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public Task<Tournament> ProcessTournamentCheckInsAsync(Tournament tournament)
		{
			return ProcessTournamentCheckInsAsync(tournament.Id.ToString());
		}

		public async Task<Tournament> AbortTournamentCheckInAsync(string tournament)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}/abort_check_in.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public Task<Tournament> AbortTournamentCheckInAsync(Tournament tournament)
		{
			return AbortTournamentCheckInAsync(tournament.Id.ToString());
		}

		public async Task<Tournament> StartTournamentAsync(string tournament)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}/start.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public Task<Tournament> StartTournamentAsync(Tournament tournament)
		{
			return StartTournamentAsync(tournament.Id.ToString());
		}

		public async Task<Tournament> FinalizeTournamentAsync(string tournament)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}/finalize.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public Task<Tournament> FinalizeTournamentAsync(Tournament tournament)
		{
			return FinalizeTournamentAsync(tournament.Id.ToString());
		}

		public async Task<Tournament> ResetTournamentAsync(string tournament)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}/reset.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public Task<Tournament> ResetTournamentAsync(Tournament tournament)
		{
			return ResetTournamentAsync(tournament.Id.ToString());
		}

		public async Task<Tournament> OpenTournamentForPredictionsAsync(string tournament)
		{
			var wrapper = await SendRequestAsync<TournamentWrapper>(
				$"tournaments/{tournament}/open_for_predictions.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public Task<Tournament> OpenTournamentForPredictionsAsync(Tournament tournament)
		{
			return OpenTournamentForPredictionsAsync(tournament.Id.ToString());
		}

		public async Task<IEnumerable<Participant>> GetParticipantsAsync(string tournament)
		{
			var wrappers = await SendRequestAsync<IEnumerable<ParticipantWrapper>>(
				$"tournaments/{tournament}/participants.json",
				HttpMethod.Get
			);

			return wrappers.Select(w => w.Item);
		}

		public Task<IEnumerable<Participant>> GetParticipantsAsync(Tournament tournament)
		{
			return GetParticipantsAsync(tournament.Id.ToString());
		}

		public async Task<Participant> CreateParticipantAsync(string tournament, ParticipantInfo participantInfo)
		{
			var wrapper = await SendRequestAsync<ParticipantWrapper>(
				$"tournaments/{tournament}/participants.json",
				HttpMethod.Post,
				participantInfo
			);

			return wrapper.Item;
		}

		public Task<Participant> CreateParticipantAsync(Tournament tournament, ParticipantInfo participantInfo)
		{
			return CreateParticipantAsync(tournament.Id.ToString(), participantInfo);
		}

		public async Task<IEnumerable<Participant>> CreateParticipantsAsync(string tournament,
			IEnumerable<ParticipantInfo> participantInfos)
		{
			var wrappers = await SendRequestAsync<IEnumerable<ParticipantWrapper>>(
				$"tournaments/{tournament}/participants/bulk_add.json",
				HttpMethod.Post,
				participantInfos
			);

			return wrappers.Select(w => w.Item);
		}

		public Task<IEnumerable<Participant>> CreateParticipantsAsync(Tournament tournament, IEnumerable<ParticipantInfo> participantInfos)
		{
			return CreateParticipantsAsync(tournament.Id.ToString(), participantInfos);
		}

		public async Task<Participant> GetParticipantAsync(string tournament, long participantId)
		{
			var wrapper = await SendRequestAsync<ParticipantWrapper>(
				$"tournaments/{tournament}/participants/{participantId}.json",
				HttpMethod.Get
			);

			return wrapper.Item;
		}

		public Task<Participant> GetParticipantAsync(Tournament tournament, long participantId)
		{
			return GetParticipantAsync(tournament.Id.ToString(), participantId);
		}

		public async Task<Participant> UpdateParticipantAsync(Participant participant, ParticipantInfo participantInfo)
		{
			var wrapper = await SendRequestAsync<ParticipantWrapper>(
				$"tournaments/{participant.TournamentId}/participants/{participant.Id}.json",
				HttpMethod.Put,
				participantInfo
			);

			return wrapper.Item;
		}

		public async Task<Participant> CheckInParticipantAsync(Participant participant)
		{
			var wrapper = await SendRequestAsync<ParticipantWrapper>(
				$"tournaments/{participant.TournamentId}/participants/{participant.Id}/check_in.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public async Task<Participant> UndoCheckInParticipantAsync(Participant participant)
		{
			var wrapper = await SendRequestAsync<ParticipantWrapper>(
				$"tournaments/{participant.TournamentId}/participants/{participant.Id}/undo_check_in.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public async Task DeleteParticipantAsync(Participant participant)
		{
			await SendRequestAsync<ParticipantWrapper>(
				$"tournaments/{participant.TournamentId}/participants/{participant.Id}.json",
				HttpMethod.Delete
			);
		}

		public async Task ClearParticipantsAsync(string tournament)
		{
			await SendRequestAsync<ChallongeMessage>(
				$"tournaments/{tournament}/participants/clear.json",
				HttpMethod.Delete
			);
		}

		public Task ClearParticipantsAsync(Tournament tournament)
		{
			return ClearParticipantsAsync(tournament.Id.ToString());
		}

		public async Task<IEnumerable<Participant>> RandomizeParticipantsAsync(string tournament)
		{
			var wrappers = await SendRequestAsync<IEnumerable<ParticipantWrapper>>(
				$"tournaments/{tournament}/participants/randomize.json",
				HttpMethod.Post
			);

			return wrappers.Select(w => w.Item);
		}

		public Task<IEnumerable<Participant>> RandomizeParticipantsAsync(Tournament tournament)
		{
			return RandomizeParticipantsAsync(tournament.Id.ToString());
		}

		public async Task<IEnumerable<Match>> GetMatchesAsync(string tournament, MatchState matchState = MatchState.All, Participant? participant = null)
		{
			var parameters = new Dictionary<string, object?>()
			{
				{ "state", matchState.GetEnumMemberValue() },
				{ "participant_id", participant?.Id }
			};

			var wrappers = await SendRequestAsync<IEnumerable<MatchWrapper>>(
				$"tournaments/{tournament}/matches.json",
				HttpMethod.Get,
				parameters
			);

			return wrappers.Select(w => w.Item);
		}

		public Task<IEnumerable<Match>> GetMatchesAsync(Tournament tournament, MatchState matchState = MatchState.All, Participant? participant = null)
		{
			return GetMatchesAsync(tournament.Id.ToString(), matchState, participant);
		}

		public async Task<Match> GetMatchAsync(string tournament, long matchId)
		{
			var wrapper = await SendRequestAsync<MatchWrapper>(
				$"tournaments/{tournament}/matches/{matchId}.json",
				HttpMethod.Get
			);

			return wrapper.Item;
		}

		public Task<Match> GetMatchAsync(Tournament tournament, long matchId)
		{
			return GetMatchAsync(tournament.Id.ToString(), matchId);
		}

		public async Task<Match> UpdateMatchAsync(Match match, MatchInfo matchInfo)
		{
			var wrapper = await SendRequestAsync<MatchWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}.json",
				HttpMethod.Put,
				matchInfo
			);

			return wrapper.Item;
		}

		public async Task<Match> ReopenMatchAsync(Match match)
		{
			var wrapper = await SendRequestAsync<MatchWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/reopen.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public async Task<Match> MarkMatchAsUnderwayAsync(Match match)
		{
			var wrapper = await SendRequestAsync<MatchWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/mark_as_underway.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public async Task<Match> UnmarkMatchAsUnderwayAsync(Match match)
		{
			var wrapper = await SendRequestAsync<MatchWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/unmark_as_underway.json",
				HttpMethod.Post
			);

			return wrapper.Item;
		}

		public async Task<IEnumerable<MatchAttachment>> GetMatchAttachmentsAsync(Match match)
		{
			var wrappers = await SendRequestAsync<IEnumerable<MatchAttachmentWrapper>>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/attachments.json",
				HttpMethod.Get
			);

			return wrappers.Select(w => w.Item);
		}

		public async Task<MatchAttachment> CreateMatchAttachmentAsync(Match match, MatchAttachmentInfo matchAttachmentInfo)
		{
			var wrapper = await SendRequestAsync<MatchAttachmentWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/attachments.json",
				HttpMethod.Post,
				PrepareMatchAttachment(matchAttachmentInfo)
			);

			return wrapper.Item;
		}

		public async Task<MatchAttachment> GetMatchAttachmentAsync(Match match, long matchAttachmentId)
		{
			var wrapper = await SendRequestAsync<MatchAttachmentWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/attachments/{matchAttachmentId}.json",
				HttpMethod.Get
			);

			return wrapper.Item;
		}

		public async Task<MatchAttachment> UpdateMatchAttachmentAsync(Match match, MatchAttachment matchAttachment, MatchAttachmentInfo matchAttachmentInfo)
		{
			var wrapper = await SendRequestAsync<MatchAttachmentWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/attachments/{matchAttachment.Id}.json",
				HttpMethod.Put,
				PrepareMatchAttachment(matchAttachmentInfo)
			);

			return wrapper.Item;
		}

		public async Task DeleteMatchAttachmentAsync(Match match, MatchAttachment matchAttachment)
		{
			await SendRequestAsync<MatchAttachmentWrapper>(
				$"tournaments/{match.TournamentId}/matches/{match.Id}/attachments/{matchAttachment.Id}.json",
				HttpMethod.Delete
			);
		}

		private static MultipartFormDataContent PrepareMatchAttachment(MatchAttachmentInfo matchAttachmentInfo)
		{
			var container = new MultipartFormDataContent();
			if (matchAttachmentInfo.Url != null)
				container.Add(new StringContent(matchAttachmentInfo.Url), "match_attachment[url]");
			if (matchAttachmentInfo.Description != null)
				container.Add(new StringContent(matchAttachmentInfo.Description), "match_attachment[description]");
			if (matchAttachmentInfo.Asset != null)
				container.Add(new StreamContent(new MemoryStream(matchAttachmentInfo.Asset.Content)), "match_attachment[asset]", matchAttachmentInfo.Asset.FileName);
			return container;
		}
	}
}
