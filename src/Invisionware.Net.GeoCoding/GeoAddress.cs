// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Address.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using XLabs.Ioc;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Class Address.
	/// </summary>
	public class GeoAddress : IGeoAddress
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GeoAddress"/> class.
		/// </summary>
		public GeoAddress()
		{
			Location = Resolver.Resolve<IGeoLocation>();
			Source = new ItemSource();
		}

		#region Implementation of IAddress
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the address.
		/// </summary>
		/// <value>The type of the address.</value>
		public AddressTypes AddressType { get; set; }
		/// <summary>
		/// Gets or sets the line1.
		/// </summary>
		/// <value>The line1.</value>
		public string Line1 { get; set; }
		/// <summary>
		/// Gets or sets the line2.
		/// </summary>
		/// <value>The line2.</value>
		public string Line2 { get; set; }
		/// <summary>
		/// Gets or sets the line3.
		/// </summary>
		/// <value>The line3.</value>
		public string Line3 { get; set; }
		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		public string City { get; set; }
		/// <summary>
		/// Gets or sets the region.
		/// </summary>
		/// <value>The region.</value>
		public string Region { get; set; }
		/// <summary>
		/// Gets or sets the postal code.
		/// </summary>
		/// <value>The postal code.</value>
		public string PostalCode { get; set; }
		/// <summary>
		/// Gets or sets the county.
		/// </summary>
		/// <value>The county.</value>
		public string County { get; set; }
		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>The country.</value>
		public string Country { get; set; }
		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		/// <value>The phone number.</value>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public IGeoLocation Location { get; set; }
		/// <summary>
		/// Gets or sets the web site.
		/// </summary>
		/// <value>The web site.</value>
		public string WebSite { get; set; }
		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		/// <value>The source.</value>
		public IGeoSource Source { get; set; }
		#endregion
	}
}