using Invisionware.Net.GeoCoding.Google.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Net.GeoCoding.Google
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2229:Add a constructor wit the following signature 'protected GoogleGeoProviderException(SerializtionInfo info, StreamContext contenxt)", Justification = "<Pending>")]
	[Serializable]
	public class GoogleGeoProviderException : GeoException
	{
		public GoogleStatusCodeTypes StatusCode { get; private set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
		public string StatusMessage { get; private set; }

		public GoogleGeoProviderException(GoogleStatusCodeTypes statusCode, string statusMessage) : base()
		{
			StatusCode = statusCode;
			StatusMessage = statusMessage;
		}

		public GoogleGeoProviderException(GoogleStatusCodeTypes statusCode, string statusMessage, string message) : base(message)
		{
			StatusCode = statusCode;
			StatusMessage = statusMessage;
		}

		public GoogleGeoProviderException(GoogleStatusCodeTypes statusCode, string statusMessage, string message, Exception innerException) : base(message, innerException)
		{
			StatusCode = statusCode;
			StatusMessage = statusMessage;
		}

		public GoogleGeoProviderException()
		{
		}

		public GoogleGeoProviderException(string message) : base(message)
		{
		}

		public GoogleGeoProviderException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
