// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Place.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class Place.
	/// </summary>
	[DebuggerDisplay("ID = {ID}, Name = {Name}, FormattedAddress = {FormattedAddress}, PlaceID = {PlaceID}")]
	internal class Place
	{
		/// <summary>
		/// Gets or sets the address components.
		/// </summary>
		/// <value>The address components.</value>
		[JsonProperty("address_components")]
		public IList<AddressComponent> AddressComponents { get; set; }
		/// <summary>
		/// Gets or sets the formatted address.
		/// </summary>
		/// <value>The formatted address.</value>
		[JsonProperty("formatted_address")]
		public string FormattedAddress { get; set; }
		/// <summary>
		/// Gets or sets the formatted phone number.
		/// </summary>
		/// <value>The formatted phone number.</value>
		[JsonProperty("formatted_phone_number")]
		public string FormattedPhoneNumber { get; set; }
		/// <summary>
		/// Gets or sets the geometry.
		/// </summary>
		/// <value>The geometry.</value>
		[JsonProperty("geometry")]
		public Geometry Geometry { get; set; }
		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		[JsonProperty("icon")]
		public string Icon { get; set; }
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("id")]
		public string ID { get; set; }
		/// <summary>
		/// Gets or sets the international phone number.
		/// </summary>
		/// <value>The international phone number.</value>
		[JsonProperty("international_phone_number")]
		public string InternationalPhoneNumber { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty("name")]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the place identifier.
		/// </summary>
		/// <value>The place identifier.</value>
		[JsonProperty("place_id")]
		public string PlaceID { get; set; }
		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		[JsonProperty("scope")]
		public string Scope { get; set; }
		/// <summary>
		/// Gets or sets the alt ids.
		/// </summary>
		/// <value>The alt ids.</value>
		[JsonProperty("alt_ids")]
		public IList<AlternateID> AltIds { get; set; }
		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		[JsonProperty("rating")]
		public double Rating { get; set; }
		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>The reference.</value>
		[JsonProperty("reference")]
		public string Reference { get; set; }
		/// <summary>
		/// Gets or sets the reviews.
		/// </summary>
		/// <value>The reviews.</value>
		[JsonProperty("reviews")]
		public IList<Review> Reviews { get; set; }
		/// <summary>
		/// Gets or sets the types.
		/// </summary>
		/// <value>The types.</value>
		[JsonProperty("types", ItemConverterType = typeof(StringEnumConverter))]
		public IList<GoogleAddressTypes> Types { get; set; }
		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		[JsonProperty("url")]
		public string Url { get; set; }
		/// <summary>
		/// Gets or sets the vicinity.
		/// </summary>
		/// <value>The vicinity.</value>
		[JsonProperty("vicinity")]
		public string Vicinity { get; set; }
		/// <summary>
		/// Gets or sets the website.
		/// </summary>
		/// <value>The website.</value>
		[JsonProperty("website")]
		public string Website { get; set; }

		/// <summary>
		/// Gets or sets the opening hours.
		/// </summary>
		/// <value>The opening hours.</value>
		[JsonProperty("opening_hours")]
		public OpeningHours OpeningHours { get; set; }

		/// <summary>
		/// Gets or sets the photos.
		/// </summary>
		/// <value>The photos.</value>
		[JsonProperty("photos")]
		public IList<Photo> Photos { get; set; }
	}
}