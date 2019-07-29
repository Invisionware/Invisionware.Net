// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Interfaces
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="IAddress.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Interface IAddress
	/// </summary>
	public interface IGeoAddress
	{
		/// <summary>
		/// Gets or sets the name.
		/// Note: This may be blank depending on the type of address
		/// </summary>
		/// <value>The name.</value>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the address.
		/// </summary>
		/// <value>The type of the address.</value>
		IList<AddressTypes> AddressType { get; set; }

		/// <summary>
		/// Gets or sets the line1.
		/// </summary>
		/// <value>The line1.</value>
		string Line1 { get; set; }
		/// <summary>
		/// Gets or sets the line2.
		/// </summary>
		/// <value>The line2.</value>
		string Line2 { get; set; }
		/// <summary>
		/// Gets or sets the line3.
		/// </summary>
		/// <value>The line3.</value>
		string Line3 { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		string City { get; set; }
		/// <summary>
		/// Gets or sets the region.
		/// </summary>
		/// <value>The region.</value>
		string Region { get; set; }
		/// <summary>
		/// Gets or sets the postal code.
		/// </summary>
		/// <value>The postal code.</value>
		string PostalCode { get;set; }
		/// <summary>
		/// Gets or sets the county.
		/// </summary>
		/// <value>The county.</value>
		string County { get; set; }
		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>The country.</value>
		string Country { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		/// <value>The phone number.</value>
		string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		IGeoLocation Location { get; set; }

		/// <summary>
		/// Gets or sets the web site.
		/// </summary>
		/// <value>The web site.</value>
		string WebSite { get; set; }

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		/// <value>The source.</value>
		IGeoSource Source { get; set; }
	}
}