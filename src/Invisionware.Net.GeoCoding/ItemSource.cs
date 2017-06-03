// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="ItemSource.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Class ItemSource.
	/// </summary>
	public class ItemSource : IGeoSource
	{
		/// <summary>
		/// Gets or sets the provider.
		/// </summary>
		/// <value>The provider.</value>
		public string Provider { get; set; }
		/// <summary>
		/// Gets or sets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		public string ObjectID { get; set; }
		/// <summary>
		/// Gets or sets the retrieved on.
		/// </summary>
		/// <value>The retrieved on.</value>
		public DateTime RetrievedOn { get; set; }
	}
}