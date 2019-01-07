using System;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using Newtonsoft.Json;
using RestEase;

namespace Invisionware.Net
{
	/// <summary>
	/// Class RestEaseHybridResponseDeserializer.
	/// </summary>
	/// <seealso cref="RestEase.ResponseDeserializer" />
	public class RestEaseHybridResponseDeserializer : ResponseDeserializer
	{
		/// <summary>
		/// Gets or sets a value indicating whether [automatic detect for unknown content types].
		/// </summary>
		/// <value><c>true</c> if [automatic detect for unknown content types]; otherwise, <c>false</c>.</value>
		public bool AutoDetectForUnknownContentTypes { get; set; } = false;

		/// <summary>
		/// Deserializes the XML.
		/// </summary>
		/// <typeparam name="T">Type of object to deserialize into</typeparam>
		/// <param name="content">The content.</param>
		/// <returns>T.</returns>
		private T DeserializeXml<T>(string content)
		{
			// Consider caching generated XmlSerializers
			var serializer = new XmlSerializer(typeof(T));

			using (var stringReader = new StringReader(content))
			{
				return (T)serializer.Deserialize(stringReader);
			}
		}

		/// <summary>
		/// Deserializes the json.
		/// </summary>
		/// <typeparam name="T">Type of object to deserialize into</typeparam>
		/// <param name="content">The content.</param>
		/// <returns>T.</returns>
		private T DeserializeJson<T>(string content)
		{
			return JsonConvert.DeserializeObject<T>(content);
		}

		/// <summary>
		/// Read the response string from the response, deserialize, and return a deserialized object
		/// </summary>
		/// <typeparam name="T">Type of object to deserialize into</typeparam>
		/// <param name="content">String content read from the response</param>
		/// <param name="response">HttpResponseMessage. Consider calling response.Content.ReadAsStringAsync() to retrieve a string</param>
		/// <returns>Deserialized response</returns>
		/// <exception cref="System.ArgumentException">Response was not JSON or XML</exception>
		public T Deserialize<T>(string content, HttpResponseMessage response)
		{
			return this.Deserialize<T>(content, response, new ResponseDeserializerInfo());
		}

		/// <summary>
		/// Read the response string from the response, deserialize, and return a deserialized object
		/// </summary>
		/// <typeparam name="T">Type of object to deserialize into</typeparam>
		/// <param name="content">String content read from the response</param>
		/// <param name="response">HttpResponseMessage. Consider calling response.Content.ReadAsStringAsync() to retrieve a string</param>
		/// <param name="info">The information.</param>
		/// <returns>Deserialized response</returns>
		/// <exception cref="ArgumentException">Response was not JSON or XML</exception>
		public override T Deserialize<T>(string content, HttpResponseMessage response, ResponseDeserializerInfo info)
		{
			switch (response.Content.Headers.ContentType.MediaType)
			{
				case "text/json":
				case "application/json":
					return this.DeserializeJson<T>(content);
				case "text/xml":
				case "application/xml":
					return this.DeserializeXml<T>(content);
				default:
					if (AutoDetectForUnknownContentTypes)
					{
						Serilog.Log.Verbose("Attempting to autodetect content type");
						try
						{
							return this.DeserializeJson<T>(content);
						}
						catch
						{
							Serilog.Log.Verbose("Failedt to deserialize as json content");
						}

						try
						{
							return this.DeserializeXml<T>(content);
						}
						catch
						{
							Serilog.Log.Verbose("Failedt to deserialize as xml content");
						}
					}
					//return base.Deserialize<T>(content, response, new ResponseDeserializerInfo());
					break;
			}

			throw new ArgumentException($"Response was not JSON or XML (Content Type: {response.Content.Headers.ContentType.MediaType})");
		}
	}
}
