using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Net.GeoCoding
{
	[Serializable]
	public class GeoException : Exception
	{
		public GeoException()
		{
		}

		public GeoException(string message) : base(message)
		{
		}

		public GeoException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
