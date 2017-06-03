using Invisionware.Serialization;
using System.Linq;

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

			return string.Join("&", properties
				.Select(x => options.QueryParmJoinFunc(x.Key, x.Value != null ? string.Concat(x.Key, options.NameValueSeperator, x.Value) : x.Key)));

		}

	}
}
