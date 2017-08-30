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
    /// <seealso cref="RestEase.IResponseDeserializer" />
    public class RestEaseHybridResponseDeserializer : ResponseDeserializer
    {
        /// <summary>
        /// Deserializes the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        /// <typeparam name="T"></typeparam>
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
            switch (response.Content.Headers.ContentType.MediaType)
            {
                case "application/json":
                    return this.DeserializeJson<T>(content);
                case "application/xml":
                    return this.DeserializeXml<T>(content);
            }

            throw new ArgumentException("Response was not JSON or XML");
        }
    }
}
