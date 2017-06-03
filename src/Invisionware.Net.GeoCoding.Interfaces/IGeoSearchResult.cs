// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Interfaces
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 06-28-2015
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="IGeoSearchResult.cs" company="Personal">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Interface IGeoSearchResult
	/// </summary>
	public interface IGeoSearchResult
	{
		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		IList<IGeoAddress> Items { get; set; }
		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		/// <value>The session.</value>
		string Session { get; set; }
	}
}