using System;
using System.Net.Http;
using Challonge.Api;
using Microsoft.Extensions.Configuration;

namespace ChallongeTests
{
	public sealed class TestUtils : IDisposable
	{
		public static ChallongeCredentials Default { get; set; } = GetCredentialsFromAppConfig();

		private static ChallongeCredentials GetCredentialsFromAppConfig()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false)
				.AddJsonFile("appsettings.Debug.json", optional: true)
				.Build();
			var appSettings = configuration.Get<AppSettings>();
			return new ChallongeCredentials(appSettings.Username, appSettings.ApiKey);
		}

		public void Dispose()
		{
			var client = new ChallongeClient(new HttpClient(), Default);
			foreach (var tournaments in client.GetTournamentsAsync().GetAwaiter().GetResult())
			{
				if (tournaments.Name.EndsWith(ChallongeTests.TEST_TOURNAMENT_SUFFIX))
				{
					client.DeleteTournamentAsync(tournaments).GetAwaiter().GetResult();
				}
			}
		}
	}
}
