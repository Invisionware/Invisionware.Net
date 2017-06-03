// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Bounds.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class Bounds.
	/// </summary>
	internal class Bounds
	{
		/// <summary>
		/// Gets or sets the north east.
		/// </summary>
		/// <value>The north east.</value>
		[JsonProperty("northeast")]
		public GoogleLocation NorthEast { get; set; }
		/// <summary>
		/// Gets or sets the south west.
		/// </summary>
		/// <value>The south west.</value>
		[JsonProperty("southwest")]
		public GoogleLocation SouthWest { get; set; }
	}
}