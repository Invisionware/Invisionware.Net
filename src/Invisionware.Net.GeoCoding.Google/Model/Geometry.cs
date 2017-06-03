// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Geometry.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using System.ComponentModel;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class Geometry.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class Geometry
	{
		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		[JsonProperty("location")]
		public GoogleLocation Location { get; set; }
		/// <summary>
		/// Gets or sets the bounds.
		/// </summary>
		/// <value>The bounds.</value>
		[JsonProperty("bounds")]
		public Bounds Bounds { get; set; }
		/// <summary>
		/// Gets or sets the type of the location.
		/// </summary>
		/// <value>The type of the location.</value>
		[JsonProperty("location_type")]
		public string LocationType { get; set; }
		/// <summary>
		/// Gets or sets the view port.
		/// </summary>
		/// <value>The view port.</value>
		[JsonProperty("viewport")]
		public Bounds ViewPort { get; set; }

	}
}