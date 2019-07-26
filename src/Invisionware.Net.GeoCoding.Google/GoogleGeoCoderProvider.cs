// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="GoogleGeoCoderProvider.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Invisionware.Net.GeoCoding.Google.Model;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invisionware.Net.GeoCoding.Google
{
	/// <summary>
	/// Class GoogleGeoCoderProvider.
	/// </summary>
	public class GoogleGeoCoderProvider : IGeoCoderProvider
	{
		/// <summary>
		/// The _wrapper
		/// </summary>
		private GooglePlacesWrapper _wrapper;

		#region Implementation of IGeoCoderProvider
		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		/// <value>The API key.</value>
		public string APIKey { get; set; }

		/// <summary>
		/// Initializes the specified initialize function.
		/// </summary>
		/// <param name="initFunc">The initialize function.</param>
		/// <exception cref="System.Exception">APIKey must be defined</exception>
		public void Initialize(Action<IGeoCoderProvider> initFunc = null)
		{
			initFunc?.Invoke(this);

			if (string.IsNullOrEmpty(APIKey))
			{
				throw new Exception("APIKey must be defined");
			}

			_wrapper = new GooglePlacesWrapper(APIKey);
		}

		/// <summary>
		/// Searches the specified request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		public async Task<IGeoSearchResult> SearchAsync(IGeoSearchRequest request)
		{
			Log.Debug("Search Request: {@request}", request);

			IGeoSearchResult searchResult = null;

			if (request.Address != null)
			{
				if (!string.IsNullOrEmpty(request.Address.Source?.ObjectID))
				{
					var address = await _wrapper.GetAddressByIdAsync(request.Address.Source.ObjectID).ConfigureAwait(false);

					if (address != null)
					{
						searchResult = new GeoSearchResult();

						searchResult.Items.Add(address);

						return searchResult;
					}
				}

				// Do we have a Lat/Long?  If so, lets use that
				if (request.Address.Location?.Latitude != null)
				{
					Log.Debug("Executing FindLocation");
					searchResult = await _wrapper.FindLocationAsync(request.Address.Location).ConfigureAwait(false);

					if (searchResult != null)
					{
						return searchResult;
					}
				}

				// Do we have enough of an address to try to use that -- this includs a full line 1 or a postcode that includes the "extra" details in the US?
				if (!string.IsNullOrEmpty(request.Address.Line1) || (!string.IsNullOrEmpty(request.Address.PostalCode) && request.Address.PostalCode.Length > 7))
				{
					Log.Debug("Executing FindAddress");

					if (searchResult == null || searchResult.Items.All(x => x == null))
					{
						searchResult = await _wrapper.FindAddressAsync(request.Address).ConfigureAwait(false);

						return searchResult;
					}
				}
			}

			// If all else fails, lets try a general search with as much as possible
			var searchRequest = new GooglePlacesSearchRequest()
			{
				Keyword =
					string.Format("{0} {1}", request.Name,
						request.Address != null
							? string.Format("{0} {1} {2} {3}", request.Address.City, request.Address.Region, request.Address.Country,
								request.Address.PostalCode)
							: null).Trim(),
				Lattitude =
					request.Address != null && request.Address.Location != null && request.Address.Location.Latitude.HasValue
						? request.Address.Location.Latitude
						: null,
				Longitude =
					request.Address != null && request.Address.Location != null && request.Address.Location.Longitude.HasValue
						? request.Address.Location.Longitude
						: null,
				Types = request.AddressTypes.Select(x => (GoogleAddressTypes) Enum.Parse(typeof (GoogleAddressTypes), x.ToString())).ToList(),
				Distance = request.Distance
			};

			if (!string.IsNullOrEmpty(searchRequest.Keyword))
			{
				Log.Debug("Executing General Search: {@searchRequest}", searchRequest);

				searchResult = await _wrapper.SearchAsync(searchRequest).ConfigureAwait(false);
			}

			return searchResult;
		}

		/// <summary>
		/// Gets the address by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Task&lt;IAddress&gt;.</returns>
		public Task<IGeoAddress> GetAddressByIdAsync(string id)
		{
			return _wrapper.GetAddressByIdAsync(id);
		}

		/// <summary>
		/// Finds the specified location.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		public Task<IGeoSearchResult> FindAsync(IGeoLocation location)
		{
			return _wrapper.FindLocationAsync(location);
		}

		/// <summary>
		/// Finds the specified address.
		/// </summary>
		/// <param name="geoAddress">The address.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		public Task<IGeoSearchResult> FindAsync(IGeoAddress geoAddress)
		{
			return _wrapper.FindAddressAsync(geoAddress);
		}

		#endregion
	}
}