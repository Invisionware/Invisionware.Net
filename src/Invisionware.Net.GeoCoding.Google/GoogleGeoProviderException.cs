using Invisionware.Net.GeoCoding.Google.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Net.GeoCoding.Google
{
	public class GoogleGeoProviderException : GeoException
	{
		public GoogleStatusCodeTypes StatusCode { get; private set; }
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
	}
}
