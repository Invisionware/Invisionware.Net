using System.Linq;
using Invisionware.Serialization.JsonNet;

namespace Invisionware.Net.WebUtils.Extensions
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Creates a QueryString from the specified object
		/// Note: QueryStringParamAttribute, JsonPropertyAttribute, and XmlElementAttribute (in that order) are all supproted for configuing
		/// the name of the parameter
		/// </summary>
		/// <typeparam name="T">The object type to serialize to a query string</typeparam>
		/// <param name="obj">Instance of the object to serialize.</param>
		/// <param name="options">The options that control how the query string is generate.</param>
		/// <returns>A valid formated query string.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string ToQueryString<T>(this T obj, QueryStringParamOptions options = null)
		{
			if (options == null) options = new QueryStringParamOptions();

			var properties = obj.SerializeToDictionary(options);
			//var properties = Newtonsoft.Json.Linq.JObject.FromObject(obj, options.JsonSerializer).ToObject<System.Collections.Generic.Dictionary<string, object>>();

			return string.Join("&", properties
				.Select(x => options.QueryParamJoinFunc(x.Key, x.Value != null ? string.Concat(x.Key, options.NameValueSepartor, x.Value) : x.Key)));

		}

		public static T FromQueryString<T>(System.Net.HttpWebRequest request, Newtonsoft.Json.JsonSerializer jsonSerializer = null)
		{
			var dict = new UrlBuilder(request.RequestUri).QueryString;

			var jObject = Newtonsoft.Json.Linq.JObject.FromObject(dict, jsonSerializer);
			var result = jObject.ToObject<T>();

			return result;
		}
	}
}
