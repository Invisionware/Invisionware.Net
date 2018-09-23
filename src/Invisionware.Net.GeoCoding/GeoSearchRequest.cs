// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="GeoSearchRequest.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Invisionware.IoC;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Class GeoSearchRequest.
	/// </summary>
	public class GeoSearchRequest : IGeoSearchRequest
	{
		public GeoSearchRequest()
		{
			Address = Resolver.Resolve<IGeoAddress>();
			AddressTypes = new List<AddressTypes>();
		}

		#region Implementation of IGeoSearchRequest
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		public IGeoAddress Address { get; set; }
		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>The distance.</value>
		public int? Distance { get; set; }

		public IList<AddressTypes> AddressTypes { get; set; } 
		#endregion
	}
}