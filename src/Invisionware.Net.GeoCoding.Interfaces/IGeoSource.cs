// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Interfaces
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="IGeoSource.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Interface IGeoSource
	/// </summary>
	public interface IGeoSource
	{
		/// <summary>
		/// Gets or sets the provider.
		/// </summary>
		/// <value>The provider.</value>
		string Provider { get; set; }
		/// <summary>
		/// Gets or sets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		string ObjectID { get; set; }
		/// <summary>
		/// Gets or sets the retrieved on.
		/// </summary>
		/// <value>The retrieved on.</value>
		DateTime RetrievedOn { get; set; }
	}
}