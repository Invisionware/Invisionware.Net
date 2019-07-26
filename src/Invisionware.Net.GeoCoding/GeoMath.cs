using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Net.GeoCoding
{
	public static class GeoMath
	{
		/// <summary>
		/// This function converts decimal degrees to radians
		/// </summary>
		/// <param name="deg">The degrees.</param>
		/// <returns>The equalivvant in radians as a System.Double.</returns>
		public static double Deg2Rad(double deg)
		{
			return (deg * Math.PI / 180.0);
		}

		/// <summary>
		/// This function converts radians to decimal degrees
		/// </summary>
		/// <param name="rad">The value in radians.</param>
		/// <returns>The equalivant in degrees as a System.Double.</returns>
		public static double Rad2Deg(double rad)
		{
			return (rad / Math.PI * 180.0);
		}
	}
}
