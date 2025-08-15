using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Challonge.Api;
using Challonge.Exceptions;
using Challonge.Objects;
using Xunit;

namespace ChallongeTests
{
	public class ChallongeTests : IClassFixture<TestUtils>
	{
		internal const string TEST_TOURNAMENT_SUFFIX = "-_-_Challonge-DotNetTest-_-_";
		private static readonly ChallongeClient client = new(new HttpClient(), TestUtils.Default);

		[Fact]
		public async Task TestTournamentCreationDeletion()
		{
			var tournamentInfos = new TournamentInfo[]
			{
				new(){ Name = "Test1" + TEST_TOURNAMENT_SUFFIX },
				new(){ Name = "Test2" + TEST_TOURNAMENT_SUFFIX },
				new(){ Name = "Test3" + TEST_TOURNAMENT_SUFFIX },
			};

			var testTournaments = new List<Tournament>();

			foreach (var tournamentInfo in tournamentInfos)
			{
				testTournaments.Add(await client.CreateTournamentAsync(tournamentInfo));
			}

			var allTournaments = await client.GetTournamentsAsync();

			Assert.True(testTournaments.All(test => allTournaments.Select(t => t.Id).Contains(test.Id)));

			foreach (var t in testTournaments)
			{
				await client.DeleteTournamentAsync(t);
				await Assert.ThrowsAsync<ChallongeException>(() => client.GetTournamentByIdAsync(t.Id));
			}
		}

		[Fact]
		public async Task TestTournamentUpdates()
		{
			var tournament = await client.CreateTournamentAsync(new()
			{
				Name = "UpdateTest" + TEST_TOURNAMENT_SUFFIX,
			});
			var newDescription = "An updated description";

			tournament = await client.UpdateTournamentAsync(tournament, new()
			{
				AcceptAttachments = true,
				Description = newDescription,
				PredictionMethod = PredictionMethod.ExponentialScoring,
			});

			Assert.True(tournament.AcceptAttachments);
			Assert.Equal(newDescription, tournament.Description);
			Assert.Equal(PredictionMethod.ExponentialScoring, tournament.PredictionMethod);

			await client.CreateParticipantsAsync(tournament,
			[
				new() { Name = "player1" },
				new() { Name = "player2" },
			]);

			tournament = await client.OpenTournamentForPredictionsAsync(tournament);

			Assert.True(tournament.AcceptingPredictions);
			Assert.NotNull(tournament.PredictionsOpenedAt);
			Assert.Equal(TournamentState.AcceptingPredictions, tournament.State);

			tournament = await client.StartTournamentAsync(tournament);

			Assert.Equal(TournamentState.Underway, tournament.State);

			var match = (await client.GetMatchesAsync(tournament)).FirstOrDefault();
			await client.UpdateMatchAsync(match, new MatchInfo
			{
				Scores = [new(3, 0)],
				WinnerId = match.Player1Id,
			});

			tournament = await client.ResetTournamentAsync(tournament);

			Assert.Null(tournament.StartedAt);
			Assert.Null(tournament.PredictionsOpenedAt);
			Assert.False(tournament.AcceptingPredictions);
			Assert.Equal(TournamentState.Pending, tournament.State);
			Assert.Equal(0, tournament.ProgressMeter);
		}

		[Fact]
		public async Task TestFullTournamentRun()
		{
			var tournamentInfo = new TournamentInfo()
			{
				Name = "Full Tournament Test" + TEST_TOURNAMENT_SUFFIX
			};

			var tournament = await client.CreateTournamentAsync(tournamentInfo);
			var count = 5;

			for (var i = 1; i <= count; i++)
			{
				var name = $"player{i}";
				var participantInfo = new ParticipantInfo
				{
					Name = name,
					Seed = i
				};

				var participant = await client.CreateParticipantAsync(tournament, participantInfo);

				Assert.Equal(name, participant.Name);
				Assert.Equal(i, participant.Seed);
			}

			var participants = await client.GetParticipantsAsync(tournament);
			tournament = await client.GetTournamentByIdAsync(tournament.Id);

			Assert.Equal(count, participants.Count());
			Assert.Equal(count, tournament.ParticipantsCount);

			tournament = await client.StartTournamentAsync(tournament);

			Assert.NotNull(tournament.StartedAt);
			Assert.Equal(TournamentState.Underway, tournament.State);

			var matches = await client.GetMatchesAsync(tournament, MatchState.Open);

			while (matches.Any())
			{
				foreach (var match in matches)
				{
					var matchInfo = new MatchInfo
					{
						Scores = [new(3, 0)],
						WinnerId = match.Player1Id
					};

					var updated = await client.UpdateMatchAsync(match, matchInfo);

					Assert.Equal(matchInfo.WinnerId, updated.WinnerId);
				}
				matches = await client.GetMatchesAsync(tournament, MatchState.Open);
			}

			tournament = await client.FinalizeTournamentAsync(tournament);

			Assert.NotNull(tournament.CompletedAt);
			Assert.Equal(TournamentState.Complete, tournament.State);
			Assert.Equal(100, tournament.ProgressMeter);
		}

		[Fact]
		public async Task TestParticipants()
		{
			var tournamentInfo = new TournamentInfo
			{
				Name = "ParticipantsTest" + TEST_TOURNAMENT_SUFFIX,
				CheckInDuration = 30,
				StartAt = DateTime.Now.AddMinutes(1),
			};

			var tournament = await client.CreateTournamentAsync(tournamentInfo);
			var count = 5;

			// tests basic participant creation
			for (var i = 1; i <= count; i++)
			{
				var name = $"player{i}";

				var participantInfo = new ParticipantInfo
				{
					Name = name,
					Seed = i,
				};

				var p = await client.CreateParticipantAsync(tournament, participantInfo);

				Assert.Equal(name, p.Name);
				Assert.Equal(i, p.Seed);
			}

			var participants = await client.GetParticipantsAsync(tournament);

			Assert.Equal(count, participants.Count());

			var participant = participants.First();
			await client.DeleteParticipantAsync(participant);

			await Assert.ThrowsAsync<ChallongeException>(() => client.GetParticipantAsync(tournament, participant.Id));

			participants = await client.GetParticipantsAsync(tournament);

			Assert.Equal(count - 1, participants.Count());

			participant = participants.First();
			var newName = "UPDATED PLAYER";

			ParticipantInfo newPi = new()
			{
				Name = newName,
			};

			await client.UpdateParticipantAsync(participant, newPi);
			participant = await client.GetParticipantAsync(tournament, participant.Id);

			Assert.Equal(newName, participant.Name);

			// tests participants bulk add
			List<ParticipantInfo> participantInfos = [];
			for (var i = count + 1; i <= count + count; i++)
			{
				var name = $"player{i}";
				var participantInfo = new ParticipantInfo
				{
					Name = name,
					Seed = i - 1,
				};
				participantInfos.Add(participantInfo);
			}

			var result = (await client.CreateParticipantsAsync(tournament, participantInfos)).ToList();

			for (var i = 0; i < count; i++)
			{
				Assert.Equal(participantInfos[i].Name, result[i].Name);
				Assert.Equal(participantInfos[i].Seed, result[i].Seed);
			}

			await client.ClearParticipantsAsync(tournament);

			var noParticipants = await client.GetParticipantsAsync(tournament);

			Assert.Empty(noParticipants);
		}

		[Fact]
		public async Task TestParticipantCheckIns()
		{
			var tournament = await client.CreateTournamentAsync(new()
			{
				Name = "ParticipantCheckInsTest" + TEST_TOURNAMENT_SUFFIX,
				CheckInDuration = 60,
				StartAt = DateTime.Now.AddMinutes(1),
			});

			var player = await client.CreateParticipantAsync(tournament, new()
			{
				Name = "player1",
			});

			player = await client.CheckInParticipantAsync(player);

			Assert.True(player.CheckedIn);
			Assert.NotNull(player.CheckedInAt);
			Assert.True(player.CheckInOpen);

			player = await client.UndoCheckInParticipantAsync(player);

			Assert.False(player.CheckedIn);
			Assert.Null(player.CheckedInAt);
			Assert.True(player.CanCheckIn);
		}

		[Fact]
		public async Task TestTournamentCheckIns()
		{
			var tournament = await client.CreateTournamentAsync(new()
			{
				Name = "TournamentCheckInsTest" + TEST_TOURNAMENT_SUFFIX,
				CheckInDuration = 60,
				StartAt = DateTime.Now.AddMinutes(1),
			});

			Assert.NotNull(tournament.StartedCheckingInAt);
			Assert.Equal(TournamentState.CheckingIn, tournament.State);

			var participants = await client.CreateParticipantsAsync(tournament,
			[
				new() { Name = "player1" },
				new() { Name = "player2" },
			]);

			foreach (var p in participants)
			{
				await client.CheckInParticipantAsync(p);
			}

			tournament = await client.ProcessTournamentCheckInsAsync(tournament);

			Assert.Equal(TournamentState.CheckedIn, tournament.State);

			tournament = await client.AbortTournamentCheckInAsync(tournament);

			Assert.Null(tournament.StartedCheckingInAt);
			Assert.Equal(TournamentState.Pending, tournament.State);

			foreach (var p in await client.GetParticipantsAsync(tournament))
			{
				Assert.False(p.CheckedIn);
				Assert.Null(p.CheckedInAt);
			}
		}

		[Fact]
		public async Task TestTournamentAttributes()
		{
			var checkInDuration = 30;
			var description = "A test tournament";
			var name = "TestAttributes" + TEST_TOURNAMENT_SUFFIX;
			var signupCap = 100;
			var startAt = DateTime.Now.AddDays(1).Date;
			var tournamentInfo = new TournamentInfo
			{
				AcceptAttachments = true,
				CheckInDuration = checkInDuration,
				Description = description,
				GrandFinalsModifier = GrandFinalsModifier.Skip,
				HideForum = false,
				HoldThirdPlaceMatch = false,
				Name = name,
				NotifyUsersWhenMatchesOpen = true,
				NotifyUsersWhenTheTournamentEnds = true,
				OpenSignup = true,
				Private = false,
				RankedBy = RankingMethod.MatchWins,
				SequentialPairings = false,
				ShowRounds = true,
				SignupCap = signupCap,
				TournamentType = TournamentType.DoubleElimination,
				StartAt = startAt,
			};
			var tournament = await client.CreateTournamentAsync(tournamentInfo);

			Assert.True(tournament.AcceptAttachments);
			Assert.Equal(checkInDuration, tournament.CheckInDuration);
			Assert.True(tournament.CreatedByApi);
			Assert.Equal(description, tournament.Description);
			Assert.Equal(GrandFinalsModifier.Skip, tournament.GrandFinalsModifier);
			Assert.False(tournament.HideForum);
			Assert.False(tournament.HoldThirdPlaceMatch);
			Assert.Equal(name, tournament.Name);
			Assert.True(tournament.NotifyUsersWhenMatchesOpen);
			Assert.True(tournament.NotifyUsersWhenTheTournamentEnds);
			Assert.True(tournament.OpenSignup);
			Assert.False(tournament.Private);
			Assert.Equal(RankingMethod.MatchWins, tournament.RankedBy);
			Assert.False(tournament.SequentialPairings);
			Assert.True(tournament.ShowRounds);
			Assert.Equal(signupCap, tournament.SignupCap);
			Assert.Equal(TournamentType.DoubleElimination, tournament.TournamentType);
			Assert.Equal(startAt, tournament.StartAt);
		}

		[Fact]
		public async Task TestMatches()
		{
			var tournament = await client.CreateTournamentAsync(new()
			{
				Name = "MatchTest" + TEST_TOURNAMENT_SUFFIX,
				AcceptAttachments = true,
			});

			await client.CreateParticipantsAsync(tournament,
			[
				new() { Name = "player1" },
				new() { Name = "player2" },
			]);

			tournament = await client.StartTournamentAsync(tournament);
			var matches = await client.GetMatchesAsync(tournament);

			Assert.Single(matches);

			var match = matches.First();
			var scores = new Score[] { new(3, 0), new(3, 0) };
			match = await client.UpdateMatchAsync(match, new MatchInfo
			{
				Scores = scores,
				WinnerId = match.Player1Id,
			});

			Assert.True(scores.SequenceEqual(match.Scores));
			Assert.Equal(match.Player1Id, match.WinnerId);
			Assert.Equal(MatchState.Complete, match.State);
			Assert.NotNull(match.CompletedAt);

			match = await client.ReopenMatchAsync(match);

			Assert.Equal(MatchState.Open, match.State);
			Assert.Null(match.CompletedAt);
			Assert.Null(match.UnderwayAt);

			match = await client.MarkMatchAsUnderwayAsync(match);

			Assert.NotNull(match.UnderwayAt);

			match = await client.UnmarkMatchAsUnderwayAsync(match);

			Assert.Null(match.UnderwayAt);
		}

		[Fact]
		public async Task TestMatchAttachments()
		{
			var tournament = await client.CreateTournamentAsync(new TournamentInfo()
			{
				Name = "MatchAttachmentTest" + TEST_TOURNAMENT_SUFFIX,
				AcceptAttachments = true,
			});

			await client.CreateParticipantsAsync(tournament, new ParticipantInfo[]
			{
				new() { Name = "player1" },
				new() { Name = "player2" },
			});

			tournament = await client.StartTournamentAsync(tournament);

			var matches = (await client.GetMatchesAsync(tournament)).First();

			var description = "An attachment test";
			var fileName = "attachmenttest.jpg";
			var matchAttachment = await client.CreateMatchAttachmentAsync(matches, new()
			{
				Asset = new MatchAttachmentAsset(TestImageGenerator.GenerateTestPngBytes(), fileName),
				Description = description
			});

			matches = await client.GetMatchAsync(tournament, matches.Id);

			Assert.Equal(description, matchAttachment.Description);
			Assert.Equal(fileName, matchAttachment.OriginalFileName);
			Assert.Equal(1, matches.AttachmentCount);

			description = "A new description";

			matchAttachment = await client.UpdateMatchAttachmentAsync(matches, matchAttachment, new MatchAttachmentInfo
			{
				Description = description
			});

			Assert.Equal(description, matchAttachment.Description);

			await client.DeleteMatchAttachmentAsync(matches, matchAttachment);

			Assert.Empty(await client.GetMatchAttachmentsAsync(matches));
			await Assert.ThrowsAsync<ChallongeException>(() => client.GetMatchAttachmentAsync(matches, matchAttachment.Id));
		}
	}
}
