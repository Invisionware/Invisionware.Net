// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Interfaces
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="IGeoSearchRequest.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Interface IGeoSearchRequest
	/// </summary>
	public interface IGeoSearchRequest
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		IGeoAddress Address { get; set; }
		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>The distance.</value>
		int? Distance { get; set; }

		/// <summary>
		/// Gets or sets the address types to search for.
		/// </summary>
		/// <value>The address types.</value>
		IList<AddressTypes> AddressTypes { get; set; }
	}
}