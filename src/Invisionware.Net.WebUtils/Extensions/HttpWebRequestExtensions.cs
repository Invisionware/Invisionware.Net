namespace Invisionware.Net.WebUtils.Extensions
{
	public static class HttpWebRequestExtensions
	{
		/// <summary>
		/// Parses the Query String from the request object and returns it as a strongly typed object
		/// </summary>
		/// <typeparam name="T">The object type to serialize the query string to</typeparam>
		/// <param name="request">A valid HttpWebRequest object</param>
		/// <param name="jsonSerializer">Option json serializer object (useful to when using custom configurations)</param>
		/// <returns>A valid instance of T</returns>
		public static T FromQueryString<T>(this System.Net.HttpWebRequest request, Newtonsoft.Json.JsonSerializer jsonSerializer = null)
		{
			var result = new UrlBuilder(request.RequestUri).QueryStringAsObject<T>(jsonSerializer);

			return result;
		}
	}
}
