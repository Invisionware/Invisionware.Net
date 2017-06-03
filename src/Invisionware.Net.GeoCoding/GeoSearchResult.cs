// ***********************************************************************
// Assembly         : ClassLibrary1
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="GeoSearchResult.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Class GeoSearchResult.
	/// </summary>
	public class GeoSearchResult : IGeoSearchResult
	{
		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>The results.</value>
		public IList<IGeoAddress> Items { get; set; }
		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		/// <value>The session.</value>
		public string Session { get; set; }
	}
}
