namespace Challonge.Api
{
	/// <summary>
	/// Holds the credentials used to access the Challonge API.
	/// </summary>
	public interface IChallongeCredentials
	{
		string Username { get; set; }
		string ApiKey { get; set; }
	}
}
