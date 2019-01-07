using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using RestEase;

namespace Invisionware.Net
{
    internal sealed class XmlRequestBodySerializer : RequestBodySerializer
    {
        public override HttpContent SerializeBody<T>(T body, RequestBodySerializerInfo info)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(body, default(T)))
                return null;

            // Consider caching generated XmlSerializers
            var serializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, body);
                var content = new StringContent(stringWriter.ToString());
                // Set the default Content-Type header to application/xml
                content.Headers.ContentType.MediaType = "application/xml";
                return content;
            }
        }
    }
}
