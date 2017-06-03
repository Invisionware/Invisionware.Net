// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Location.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Class Location.
	/// </summary>
	[DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}")]
	public class GeoLocation : IGeoLocation
	{
		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public double? Latitude { get; set; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double? Longitude { get; set; }
	}
}