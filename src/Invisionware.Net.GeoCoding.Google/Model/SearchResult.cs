// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="SearchResult.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class SearchResult.
	/// </summary>
	[DebuggerDisplay("ID = {ID}, Name = {Name}, PlaceID = {PlaceID}, Scope = {Scope}, Vicinity = {Vicinity}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SearchResult1
	{
		/// <summary>
		/// Gets or sets the address components.
		/// </summary>
		/// <value>The address components.</value>
		[JsonProperty("address_components")]
		public IList<AddressComponent> AddressComponents { get; set; }

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
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty("name")]
		public string Name { get; set; }

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
		/// Gets or sets the alt i ds.
		/// </summary>
		/// <value>The alt i ds.</value>
		[JsonProperty("alt_ids")]
		public IList<AlternateID> AltIDs { get; set; }

		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>The reference.</value>
		[JsonProperty("reference")]
		public string Reference { get; set; }

		/// <summary>
		/// Gets or sets the types.
		/// </summary>
		/// <value>The types.</value>
		[JsonProperty("types")]
		public IList<string> Types { get; set; }

		/// <summary>
		/// Gets or sets the vicinity.
		/// </summary>
		/// <value>The vicinity.</value>
		[JsonProperty("vicinity")]
		public string Vicinity { get; set; }
	}
}