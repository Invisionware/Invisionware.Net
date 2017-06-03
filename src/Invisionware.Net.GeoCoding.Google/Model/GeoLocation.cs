// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
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

using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class Location.
	/// </summary>
	[DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class GoogleLocation : IGeoLocation
	{
		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		[JsonProperty("lat")]
		public double? Latitude { get; set; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		[JsonProperty("lng")]
		public double? Longitude { get; set; }
	}
}