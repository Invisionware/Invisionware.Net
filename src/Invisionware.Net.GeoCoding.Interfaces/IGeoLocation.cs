// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Interfaces
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-27-2015
// ***********************************************************************
// <copyright file="ILocation.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Interface ILocation
	/// </summary>
	public interface IGeoLocation
	{
		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		double? Latitude { get; set; }
		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		double? Longitude { get; set; }
	}
}