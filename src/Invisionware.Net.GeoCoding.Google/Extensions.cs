// ***********************************************************************
// Assembly         : ClassLibrary2
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Extensions.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Invisionware.Net.GeoCoding.Google.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invisionware.Net.GeoCoding.Google
{
	/// <summary>
	/// Class Extensions.
	/// </summary>
	internal static class Extensions
	{
		/// <summary>
		/// To the address.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>IAddress.</returns>
		public static IGeoAddress ToAddress(this Place source)
		{
			if (source == null) return null;

			var address = new GeoAddress
			{
				Name = source.Name,
				Line1 = string.Format("{0} {1}",
					source.AddressComponents?.Where(x => x.Types.Any(x1 => x1 == GoogleAddressTypes.StreetNumber))
						.Select(x => x.LongName)
						.FirstOrDefault(),
					source.AddressComponents?.Where(x => x.Types.Any(x1 => x1 == GoogleAddressTypes.Route))
						.Select(x => x.LongName)
						.FirstOrDefault()),
				City =
					source.AddressComponents?.Where(x => x.Types.Any(x1 => x1 == GoogleAddressTypes.Locality))
						.Select(x => x.LongName)
						.FirstOrDefault(),
				Region = source.AddressComponents?.Where(x => x.Types.Any(x1 => x1 == GoogleAddressTypes.AdministrativeAreaLevel1))
					.Select(x => x.LongName)
					.FirstOrDefault(),
				Country =
					source.AddressComponents?.Where(x => x.Types.Any(x1 => x1 == GoogleAddressTypes.Country))
						.Select(x => x.LongName)
						.FirstOrDefault(),
				PostalCode =
					source.AddressComponents?.Where(x => x.Types.Any(x1 => x1 == GoogleAddressTypes.PostalCode))
						.Select(x => x.LongName)
						.FirstOrDefault(),
				PhoneNumber = source.FormattedPhoneNumber,
				Location =
				{
					Latitude = source.Geometry?.Location.Latitude,
					Longitude = source.Geometry?.Location.Longitude
				},
				Source =
				{
					Provider = "google",
					ObjectID = source.PlaceID,
					RetrievedOn = DateTime.Now
				}
			};

			return address;
		}

		/// <summary>
		/// To the addresses.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>IList&lt;IAddress&gt;.</returns>
		static public IList<IGeoAddress> ToAddresses(this IList<Place> source)
		{
			if (source == null) return null;

			return source.Where(item => item != null).Select(item => item.ToAddress()).ToList();
		}
	}
}
