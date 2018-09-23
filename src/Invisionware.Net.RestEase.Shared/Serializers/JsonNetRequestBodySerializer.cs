using System.Net.Http;
using Newtonsoft.Json;
using RestEase;

namespace Invisionware.Net
{
    internal sealed class JsonNetRequestBodySerializer : RequestBodySerializer
    {
		public JsonSerializerSettings JsonSerializerSettings { get; set; }

		public override HttpContent SerializeBody<T>(T body, RequestBodySerializerInfo info)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(body, default(T)))
                return null;

            var content = new StringContent(JsonSerializerSettings != null ? JsonConvert.SerializeObject(body, JsonSerializerSettings) : JsonConvert.SerializeObject(body));

            // Set the default Content-Type header to application/json
            content.Headers.ContentType.MediaType = "application/json";

            return content;
        }
    }
}
