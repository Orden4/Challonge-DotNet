using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Challonge.Exceptions
{
	internal class ErrorResponse
	{
		[JsonPropertyName("errors")]
		public ICollection<string> Errors { get; set; }

		public string Message
		{
			get
			{
				var i = 1;
				var errorCount = Errors.Count;
				StringBuilder builder = new($"Challonge responded with the following errors:{Environment.NewLine}");
				foreach (var error in Errors)
				{
					builder.Append($"{i}. {error}{(i == errorCount ? "" : Environment.NewLine)}");
					i++;
				}
				return builder.ToString();
			}
		}
	}
}
