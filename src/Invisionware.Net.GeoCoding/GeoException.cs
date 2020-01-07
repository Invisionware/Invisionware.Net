using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Net.GeoCoding
{
	[Serializable]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2229:Add a constructor wit the following signature 'protected GeoException(SerializtionInfo info, StreamContext contenxt)", Justification = "<Pending>")]
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
