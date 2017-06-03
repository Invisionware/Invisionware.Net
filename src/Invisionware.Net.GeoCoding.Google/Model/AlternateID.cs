// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="AlternateID.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class AlternateID.
	/// </summary>
	[DebuggerDisplay("ID = {ID}, Scope = {Scope}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class AlternateID
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("place_id")]
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		[JsonProperty("scope")]
		public string Scope { get; set; }
	}
}