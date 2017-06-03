// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="OpeningHours.cs" company="Invisionware">
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
	/// Class OpeningHours.
	/// </summary>
	[DebuggerDisplay("OpenNow = {OpenNow}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class OpeningHours
	{
		/// <summary>
		/// Gets or sets a value indicating whether [open now].
		/// </summary>
		/// <value><c>true</c> if [open now]; otherwise, <c>false</c>.</value>
		[JsonProperty("open_now")]
		public bool OpenNow { get; set; }
	}
}