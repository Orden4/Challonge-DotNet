namespace Challonge.Api
{
	/// <inheritdoc cref="IChallongeCredentials"/>
	public class ChallongeCredentials(string username, string apiKey) : IChallongeCredentials
	{
		public string Username { get; set; } = username;
		public string ApiKey { get; set; } = apiKey;
	}
}
