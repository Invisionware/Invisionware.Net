// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-27-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Google.Geo.Wrapper.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Invisionware.Net.GeoCoding.Google.Model;
using Invisionware.Net.WebUtils;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XLabs.Ioc;

namespace Invisionware.Net.GeoCoding.Google
{
	/// <summary>
	/// Class GooglePlacesWrapper.
	/// </summary>
	internal class GooglePlacesWrapper
	{
		/// <summary>
		/// The _api key
		/// </summary>
		private readonly string _apiKey;
		/// <summary>
		/// The search source URL
		/// </summary>
		private string SearchSourceUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json";
		/// <summary>
		/// The details source URL
		/// </summary>
		private string DetailsSourceUrl = "https://maps.googleapis.com/maps/api/place/details/json";
		/// <summary>
		/// The geo code source URL
		/// </summary>
		private string GeoCodeSourceUrl = "https://maps.googleapis.com/maps/api/geocode/json";

		/// <summary>
		/// Initializes a new instance of the <see cref="GooglePlacesWrapper"/> class.
		/// </summary>
		/// <param name="apiKey">The API key.</param>
		public GooglePlacesWrapper(string apiKey)
		{
			_apiKey = apiKey;
		}

		/// <summary>
		/// Searches the specified request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		public Task<IGeoSearchResult> SearchAsync(GooglePlacesSearchRequest request)
		{
			var ub = new UrlBuilder(SearchSourceUrl);

			ub.QueryString["key"] = _apiKey;
			ub.QueryString["query"] = request.Keyword;

			if (request.Lattitude.HasValue && request.Longitude.HasValue)
			{
				ub.QueryString["location"] = string.Format("{0},{1}", request.Lattitude, request.Longitude);
				ub.QueryString["radius"] = request.Distance.HasValue ? request.Distance.ToString() : "10000";
			}
			else
			{
				ub.QueryString["radius"] = request.Distance.ToString();
			}

			ub.QueryString["language"] = request.Language;
			ub.QueryString["name"] = request.Name;
			ub.QueryString["pagetoken"] = request.PageToken;
			ub.QueryString["maxprice"] = request.MaxPrice.ToString();
			ub.QueryString["minprice"] = request.MinPrice.ToString();
			ub.QueryString["opennow"] = request.OpenNow.ToString();
			//ub.QueryString["rankby"] = request.RankBy.ToString();
			ub.QueryString["types"] = request.Types.Any() ? request.Types.Select(x => x.ToString()).Aggregate((s, s1) => s + "|" + s1) : null;
			ub.QueryString["zagatselected"] = (request.ZagatSelected).ToString();
			ub.QueryString["pagetoken"] = request.Session;

			return ExecuteQueryAsync(ub);
		}

		/// <summary>
		/// Finds the address.
		/// </summary>
		/// <param name="geoAddress">The address.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		public Task<IGeoSearchResult> FindAddressAsync(IGeoAddress geoAddress)
		{
			var ub = new UrlBuilder(GeoCodeSourceUrl);

			var parts = new[]
			{
				geoAddress.Line1,
				geoAddress.Line2,
				geoAddress.Line3,
				geoAddress.City,
				geoAddress.Region,
				geoAddress.Country,
				geoAddress.PostalCode
			}.Where(x => !string.IsNullOrEmpty(x)).ToArray();

			ub.QueryString["key"] = _apiKey;
			ub.QueryString["address"] = string.Join(",", parts);

			return ExecuteQueryAsync(ub);
		}

		/// <summary>
		/// Finds the location.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		public Task<IGeoSearchResult> FindLocationAsync(IGeoLocation location)
		{
			var ub = new UrlBuilder(GeoCodeSourceUrl);

			ub.QueryString["key"] = _apiKey;
			ub.QueryString["latlng"] = string.Format("{0},{1}", location.Latitude, location.Longitude);

			return ExecuteQueryAsync(ub);
		}

		/// <summary>
		/// Gets the address by identifier.
		/// </summary>
		/// <param name="googleID">The google identifier.</param>
		/// <returns>Task&lt;IAddress&gt;.</returns>
		public async Task<IGeoAddress> GetAddressByIdAsync(string googleID)
		{
			var ub = new UrlBuilder(DetailsSourceUrl);

			ub.QueryString["key"] = _apiKey;
			ub.QueryString["placeid"] = googleID;

			Log.Debug("GetAddressByID: {0}", ub.Uri.ToString());

			var httpClient = new HttpClient();
			var httpGetResponse = await httpClient.GetAsync(ub.Uri).ConfigureAwait(false);

			if (httpGetResponse == null || !httpGetResponse.IsSuccessStatusCode)
			{
				Log.Error("Failed to execute query for address search. HTTP Result: {0}", httpGetResponse?.StatusCode);

				return null;
			}

			var httpMsgResponse = await httpGetResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

			try
			{
				var result = JsonConvert.DeserializeObject<GooglePlacesDetailsRoot>(httpMsgResponse, new JsonSerializerSettings { ContractResolver = new ContractResolverDelegate(type => Resolver.IsRegistered(type), type => Resolver.Resolve(type)) });

				if (result.Status != GoogleStatusCodeTypes.Ok)
				{
					//TODO Should we throw an exception here?
					Log.Error("Failed to execute query for address search. {@results}", result);

					return null;
				}

				return result.Item.ToAddress();
			}
			catch (JsonSerializationException jex)
			{
				Log.Error(jex, "Failed to deserialize result from Google for google place id: {0}", googleID);
			}

			return null;
		}

		/// <summary>
		/// Executes the query.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="autoLoadDetails">if set to <c>true</c> [automatic load details].</param>
		/// <returns>Task&lt;IList&lt;IAddress&gt;&gt;.</returns>
		private async Task<IGeoSearchResult> ExecuteQueryAsync(UrlBuilder url, bool autoLoadDetails = true)
		{
			var httpClient = new HttpClient();

			Log.Debug("ExecuteQuery: {0}", url.Uri.ToString());

			var httpGetResponse = await httpClient.GetAsync(url.Uri).ConfigureAwait(false);

			if (httpGetResponse == null || !httpGetResponse.IsSuccessStatusCode)
			{
				Log.Error("Failed to execute query for address search. HTTP Result: {0}", httpGetResponse?.StatusCode);

				return null;
			}

			try
			{
				var httpMsgResponse = await httpGetResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

				Log.Debug("ExecuteQuery Result: {0}", httpMsgResponse);

				var results = JsonConvert.DeserializeObject<GoogleGeoSearchRoot>(httpMsgResponse, new JsonSerializerSettings { ContractResolver = new ContractResolverDelegate(type => Resolver.IsRegistered(type), type => Resolver.Resolve(type)) });

				if (results.Status != GoogleStatusCodeTypes.Ok)
				{
					//TODO Should we thrown an exception here?
					Log.Error("Failed to execute query for address search. {@results}", results);

					return null;
				}

				if (!autoLoadDetails)
				{
					var result = new GeoSearchResult
									{
										Items =
											results.Items.Where(x => x.Types.Contains(GoogleAddressTypes.StreetAddress))
													.Select(x => x.ToAddress())
													.ToList(),
										Session = results.NextPageToken
									};

					return result;
				}

				var tmpList =
					results.Items.Where(x =>
											!x.Types.Intersect(
												new[]
													{
														//GoogleAddressTypes.StreetAddress,
														GoogleAddressTypes.Route,
														GoogleAddressTypes.Intersection,
														GoogleAddressTypes.Political,
														GoogleAddressTypes.Country,
														GoogleAddressTypes.AdministrativeAreaLevel1,
														GoogleAddressTypes.AdministrativeAreaLevel2,
														GoogleAddressTypes.AdministrativeAreaLevel3,
														GoogleAddressTypes.AdministrativeAreaLevel4,
														GoogleAddressTypes.AdministrativeAreaLevel5,
														//GoogleAddressTypes.Establishment,
														GoogleAddressTypes.ColloquialArea,
														GoogleAddressTypes.Locality,
														GoogleAddressTypes.Ward,
														GoogleAddressTypes.Sublocality,
														GoogleAddressTypes.Neighborhood,
														GoogleAddressTypes.PostalCode,
														GoogleAddressTypes.NaturalFeature,
														GoogleAddressTypes.PostalTown,
														GoogleAddressTypes.Geocode,
														GoogleAddressTypes.PostalCodePrefix,
														GoogleAddressTypes.PostalCodeSuffix,
														GoogleAddressTypes.SublocalityLevel5,
														GoogleAddressTypes.SublocalityLevel4,
														GoogleAddressTypes.SublocalityLevel3,
														GoogleAddressTypes.SublocalityLevel2,
														GoogleAddressTypes.SublocalityLevel1,
														GoogleAddressTypes.Accounting,
														GoogleAddressTypes.Atm
													}).Any());

				var taskList = tmpList.Select(result => GetAddressByIdAsync(result.PlaceID).ContinueWith(ctxAddress =>
																											{
																												return ctxAddress.Result;
																											})).ToList();

				var taskResult = Task.WhenAll(taskList).ContinueWith(ctxTaskList =>
																		{
																			var result = new GeoSearchResult
																							{
																								Items = ctxTaskList.Result.Where(x => x != null).ToList(),
																								Session = results.NextPageToken
																							};

																			return (IGeoSearchResult) result;
																		});

				return await taskResult.ConfigureAwait(false);
			}
			catch (Newtonsoft.Json.JsonSerializationException ex)
			{
				Log.Error(ex, "Failed to desearlize json from google");
			}

			return null;
		}
	}

	/// <summary>
	/// Class GooglePlacesSearchRequest.
	/// </summary>
	internal class GooglePlacesSearchRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GooglePlacesSearchRequest"/> class.
		/// </summary>
		/// <param name="useDefaultPlaces">if set to <c>true</c> [use default places].</param>
		public GooglePlacesSearchRequest(bool useDefaultPlaces = false)
		{
			Language = "en";
			Types = new List<GoogleAddressTypes>();

			if (useDefaultPlaces)
			{
				//Types.Add(GoogleAddressTypes.movie_theater);
				Types.Add(GoogleAddressTypes.NightClub);
				Types.Add(GoogleAddressTypes.Park);
				//Types.Add(GoogleAddressTypes.school);
				Types.Add(GoogleAddressTypes.Stadium);
				//Types.Add(GoogleAddressTypes.university);
				//Types.Add(GoogleAddressTypes.zoo);
			}
		}

		/// <summary>
		/// Gets or sets the keyword.
		/// </summary>
		/// <value>The keyword.</value>
		public string Keyword { get; set; }
		/// <summary>
		/// Gets or sets the lattitude.
		/// </summary>
		/// <value>The lattitude.</value>
		public double? Lattitude { get; set; }
		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double? Longitude { get; set; }
		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>The distance.</value>
		public int? Distance { get; set; }

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <value>The language.</value>
		public string Language { get; private set; }
		/// <summary>
		/// Gets or sets the minimum price.
		/// </summary>
		/// <value>The minimum price.</value>
		public double? MinPrice { get; set; }
		/// <summary>
		/// Gets or sets the maximum price.
		/// </summary>
		/// <value>The maximum price.</value>
		public double? MaxPrice { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [open now].
		/// </summary>
		/// <value><c>null</c> if [open now] contains no value, <c>true</c> if [open now]; otherwise, <c>false</c>.</value>
		public bool? OpenNow { get; set; }
		//public RankByTypes? RankBy { get; set; }
		/// <summary>
		/// Gets or sets the types.
		/// </summary>
		/// <value>The types.</value>
		public ICollection<GoogleAddressTypes> Types { get; set; }
		/// <summary>
		/// Gets or sets the page token.
		/// </summary>
		/// <value>The page token.</value>
		public string PageToken { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [zagat selected].
		/// </summary>
		/// <value><c>null</c> if [zagat selected] contains no value, <c>true</c> if [zagat selected]; otherwise, <c>false</c>.</value>
		public bool? ZagatSelected { get; set; }
		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		/// <value>The session.</value>
		public string Session { get; set; }
	}

	#region Internal Enums


	#endregion Internal Enums
}