// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Photo.cs" company="Invisionware">
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
	/// Class Photo.
	/// </summary>
	[DebuggerDisplay("PhotoReference = {PhotoReference}, Height = {Height}, Width = {Width}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class Photo
	{
		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		[JsonProperty("height")]
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the HTML attributes.
		/// </summary>
		/// <value>The HTML attributes.</value>
		[JsonProperty("html_attributions")]
		public IList<string> HtmlAttributes { get; set; }

		/// <summary>
		/// Gets or sets the photo reference.
		/// </summary>
		/// <value>The photo reference.</value>
		[JsonProperty("photo_reference")]
		public string PhotoReference { get; set; }

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		[JsonProperty("width")]
		public int Width { get; set; }
	}
}