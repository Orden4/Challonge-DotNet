using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Web;
using Challonge.Api;
using Challonge.JsonConverters;
using Challonge.Objects;

namespace Challonge.Helpers
{
	internal static class RequestBuilder
	{
		internal static HttpRequestMessage BuildRequest(string url, HttpMethod method, object? parameters)
		{
			return method.Method switch
			{
				"GET" => BuildGetRequest(url, parameters),
				"POST" => BuildPostRequest(url, parameters),
				"PUT" => BuildPutRequest(url, parameters),
				"DELETE" => BuildDeleteRequest(url, parameters),
				_ => throw new NotImplementedException("This HTTP method is not supported.")
			};
		}

		private static HttpRequestMessage BuildGetRequest(string url, object? parameters)
		{
			return new(HttpMethod.Get, url + BuildQueryString(parameters));
		}

		private static HttpRequestMessage BuildPostRequest(string url, object? parameters)
		{
			return new(HttpMethod.Post, url)
			{
				Content = BuildHttpContent(parameters),
			};
		}

		private static HttpRequestMessage BuildPutRequest(string url, object? parameters)
		{
			return new(HttpMethod.Put, url)
			{
				Content = BuildHttpContent(parameters),
			};
		}

		private static HttpRequestMessage BuildDeleteRequest(string url, object? parameters)
		{
			return new(HttpMethod.Delete, url)
			{
				Content = BuildHttpContent(parameters),
			};
		}

		private static string BuildQueryString(object? parameters)
		{
			if (parameters == null)
				return string.Empty;

			if (parameters is ChallongeObjectInfo)
			{
				var str = JsonSerializer.Serialize(parameters, ChallongeClient.JsonSerializerOptions);
				parameters = JsonSerializer.Deserialize<Dictionary<string, object?>>(str, ChallongeClient.JsonSerializerOptions);
			}

			var query = HttpUtility.ParseQueryString(string.Empty);
			if (parameters is IDictionary<string, object?> dict)
			{
				foreach (var (key, value) in dict)
				{
					if (value != null)
						query.Add(key, value.ToString());
				}
			}

			if (query.Count == 0)
				return string.Empty;

			return $"?{query}";
		}

		private static HttpContent? BuildHttpContent(object? parameters)
		{
			if (parameters == null)
				return null;
			if (parameters is HttpContent content)
				return content;

			var key = default(string?);
			var container = new MultipartFormDataContent();

			if (parameters is ChallongeObjectInfo info)
			{
				var infoType = parameters.GetType();
				key = infoType.GetCustomAttribute<WrapperAttribute>()!.Name;
			}
			else if (parameters is IEnumerable)
			{
				var enumerableType = parameters.GetType();
				var infoType = enumerableType.GetGenericArguments().ElementAtOrDefault(0) ?? enumerableType.GetElementType()!;
				key = $"{infoType.GetCustomAttribute<WrapperAttribute>()!.Name}s";
			}
			else
			{
				return JsonContent.Create(parameters, options: ChallongeClient.JsonSerializerOptions);
			}

			var dict = new Dictionary<string, object>
			{
				{ key, parameters }
			};
			return JsonContent.Create(dict, options: ChallongeClient.JsonSerializerOptions);
		}
	}
}
