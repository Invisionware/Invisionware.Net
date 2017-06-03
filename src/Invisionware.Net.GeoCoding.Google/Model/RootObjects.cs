// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="RootObjects.cs" company="Invisionware">
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
	/// Class GooglePlacesDetailsRoot.
	/// </summary>
	[DebuggerDisplay("Status = {Status}")]
	internal class GooglePlacesDetailsRoot
	{
		/// <summary>
		/// Gets or sets the HTML attributes.
		/// </summary>
		/// <value>The HTML attributes.</value>
		[JsonProperty("html_attributions")]
		public IList<object> HtmlAttributes { get; set; }
		/// <summary>
		/// Gets or sets the result.
		/// </summary>
		/// <value>The result.</value>
		[JsonProperty("result")]
		public Place Item { get; set; }
		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty("status")]
		public GoogleStatusCodeTypes Status { get; set; }
	}

	/// <summary>
	/// Class GoogleGeoSearchRoot.
	/// </summary>
	[DebuggerDisplay("Status = {Status}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class GoogleGeoSearchRoot
	{
		/// <summary>
		/// Gets or sets the HTML attributes.
		/// </summary>
		/// <value>The HTML attributes.</value>
		[JsonProperty("html_attributions")]
		public IList<object> HtmlAttributes { get; set; }

		/// <summary>
		/// Gets or sets the next page token.
		/// </summary>
		/// <value>The next page token.</value>
		[JsonProperty("next_page_token")]
		public string NextPageToken { get; set; }

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		[JsonProperty("results")]
		public IList<Place> Items { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty("status")]
		public GoogleStatusCodeTypes Status { get; set; }
	}
}
