// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Aspect.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics;
using Newtonsoft.Json;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class Aspect.
	/// </summary>
	[DebuggerDisplay("Type = {Type}, Rating = {Rating}")]
	internal class Aspect
	{
		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		[JsonProperty("rating")]
		public int Rating { get; set; }
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}