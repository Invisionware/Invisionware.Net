using System.Net.Http;
using RestEase;

namespace Invisionware.Net
{
    internal sealed class JsonNetRequestBodySerializer : RequestBodySerializer
    {
        public override HttpContent SerializeBody<T>(T body, RequestBodySerializerInfo info)
        {
            if (body == null)
                return null;

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body));

            // Set the default Content-Type header to application/json
            content.Headers.ContentType.MediaType = "application/json";

            return content;
        }
    }
}
