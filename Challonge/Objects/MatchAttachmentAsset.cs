namespace Challonge.Objects
{
	public class MatchAttachmentAsset(byte[] content, string fileName)
	{
		public string FileName { get; set; } = fileName;
		public byte[] Content { get; set; } = content;
	}
}
