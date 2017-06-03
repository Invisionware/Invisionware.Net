// ***********************************************************************
// Assembly         : PortableGeoCoding.Interfaces
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-27-2015
// ***********************************************************************
// <copyright file="IGeoCoderProvider.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;

namespace Invisionware.Net.GeoCoding
{
	/// <summary>
	/// Interface IGeoCoderProvider
	/// </summary>
	public interface IGeoCoderProvider
	{
		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		/// <value>The API key.</value>
		string APIKey { get; set; }

		/// <summary>
		/// Initializes the specified initialize function.
		/// </summary>
		/// <param name="initFunc">The initialize function.</param>
		void Initialize(Action<IGeoCoderProvider> initFunc = null);

		/// <summary>
		/// Searches the specified request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		Task<IGeoSearchResult> SearchAsync(IGeoSearchRequest request);

		/// <summary>
		/// Gets the address by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task&lt;IAddress&gt;.</returns>
		Task<IGeoAddress> GetAddressByIdAsync(string id);

		/// <summary>
		/// Finds the specified location.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		Task<IGeoSearchResult> FindAsync(IGeoLocation location);

		/// <summary>
		/// Finds the specified address.
		/// </summary>
		/// <param name="geoAddress">The address.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		Task<IGeoSearchResult> FindAsync(IGeoAddress geoAddress);
	}
}