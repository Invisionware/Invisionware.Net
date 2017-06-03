// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="AddressComponent.cs" company="Invisionware">
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
	/// Class AddressComponent.
	/// </summary>
	[DebuggerDisplay("ShortName = {ShortName}, LongName = {LongName}")]
	internal class AddressComponent
	{
		/// <summary>
		/// Gets or sets the long name.
		/// </summary>
		/// <value>The long name.</value>
		[JsonProperty("long_name")]
		public string LongName { get; set; }
		/// <summary>
		/// Gets or sets the short name.
		/// </summary>
		/// <value>The short name.</value>
		[JsonProperty("short_name")]
		public string ShortName { get; set; }
		/// <summary>
		/// Gets or sets the types.
		/// </summary>
		/// <value>The types.</value>
		[JsonProperty("types", ItemConverterType = typeof(StringEnumConverter))]
		public IList<GoogleAddressTypes> Types { get; set; }
	}
}