using System;

namespace Invisionware.Net.GeoCoding
{
	public static class GeoLocationExtensions
	{
		/// <summary>
		/// This routine calculates the distance between two points (given the 
		/// latitude/longitude of those points). It is being used to calculate 
		/// the distance between two locations using GeoDataSource(TM) products
		/// 
		/// Definitions:                                                         
		///   South latitudes are negative, east longitudes are positive         
		///                                                                      
		/// Passed to function:                                                  
		///   lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)
		///   lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)
		///   unit = the unit you desire for results                             
		///          where: 'M' is statute miles (default)                       
		///                 'K' is kilometers                                    
		///                 'N' is nautical miles                                
		///                                                                      
		/// Worldwide cities and other features databases with latitude longitude
		/// are available at https://www.geodatasource.com                       
		/// </summary>
		/// <param name="location">The location.</param>
		/// <param name="units">The units.</param>
		/// <returns>System.Nullable&lt;System.Double&gt;.</returns>
		public static double? DistanceBetween(this IGeoLocation location1, IGeoLocation location2, GeoLocationDistanceUnits units = GeoLocationDistanceUnits.Miles)
		{
			if (location1 == null || location2 == null) return null;
			if (location1.Longitude == null || location2.Longitude == null) return null;
			if (location1.Latitude == null || location2.Latitude == null) return null;
			if (location1.Latitude == location2.Latitude && location1.Longitude == location2.Longitude) return 0;

			double lon1 = location1.Longitude.Value;
			double lat1 = location1.Latitude.Value;

			double lon2 = location2.Longitude.Value;
			double lat2 = location2.Latitude.Value;

			double theta = lon1 - lon2;
			double dist = Math.Sin(GeoMath.Deg2Rad(lat1)) * Math.Sin(GeoMath.Deg2Rad(lat2)) + Math.Cos(GeoMath.Deg2Rad(lat1)) * Math.Cos(GeoMath.Deg2Rad(lat2)) * Math.Cos(GeoMath.Deg2Rad(theta));
			dist = Math.Acos(dist);
			dist = GeoMath.Rad2Deg(dist);
			dist = dist * 60 * 1.1515;

			switch (units)
			{
				case GeoLocationDistanceUnits.Kilometers:
					dist = dist * 1.609344;
					break;
				case GeoLocationDistanceUnits.NauticalMiles:
					dist = dist / 1.151;
					break;
				case GeoLocationDistanceUnits.Meters:
					dist = dist * 1609.344;
					break;
				case GeoLocationDistanceUnits.Miles:
				default:
					break;
			}

			return dist;
		}
	}

	public enum GeoLocationDistanceUnits
	{
		Miles,
		NauticalMiles,
		Meters,
		Kilometers
	}

}
