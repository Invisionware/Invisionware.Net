// ***********************************************************************
// Assembly         : Invisionware.DataMigration.API.LightSpeed
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 11-27-2016
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 11-27-2016
// ***********************************************************************
// <copyright file="ClientFactory.cs" company="Invisionware">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using RestEase;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Invisionware.Net.Http;

namespace Invisionware.Net
{
	/// <summary>
	/// Class RestEase Client Factory.
	/// </summary>
	public class RestEaseClientFactory<T> : IRestEaseClientFactory<T>
	{
		/// <summary>
		/// Gets the options.
		/// </summary>
		/// <value>
		/// The options.
		/// </value>
		public RestEaseClientFactoryOptions Options { get; private set; }

		/// <summary>
		/// Gets the HTTP client.
		/// </summary>
		/// <value>The HTTP client.</value>
		public HttpClient HttpClient { get; private set; }

		/// <summary>
		/// Gets the API client.
		/// </summary>
		/// <value>The API client.</value>
		public RestClient ApiClient { get; private set; }

		/// <summary>
		/// Gets the account API.
		/// </summary>
		/// <value>The account API.</value>
		public T Api { get; private set; }

		/// <summary>  Occurs before the HttpClient sends any data</summary>
		public event HttpLoggingHandler.OnSendAsyncHandler OnSendAsyncBefore;

		/// <summary>Occurs after the httpclient sends its request</summary>
		public event HttpLoggingHandler.OnSendAsyncHandler OnSendAsyncAfter;

		/// <summary>
		/// Initializes the factory.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">token - token cannot be null</exception>
		public Task<bool> InitializeAsync(string url, RestEaseClientFactoryOptions options = null)
		{
			return InitializeAsync(url, new HttpClientHandler(), options);
		}

		/// <summary>
		/// Initializes the factory.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="authToken">The auth token.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">token - token cannot be null</exception>
		public Task<bool> InitializeAsync(string url, string authToken, RestEaseClientFactoryOptions options = null)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException(nameof(authToken), "token cannot be null");

#if DEBUG
			Serilog.Log.Debug($"Authentication Token: {authToken}");
#endif

			return InitializeAsync(url, new AuthenticatedHttpClientHandler(() => Task.FromResult(authToken))
			{
				AuthenticationScheme = "Bearer"
			}, options);
		}

		/// <summary>
		/// initialize the factory.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="clientHandler">The client handler.</param>
		/// <param name="options">The options.</param>
		/// <returns>
		/// Task&lt;System.Boolean&gt;.
		/// </returns>
		public Task<bool> InitializeAsync(string url, HttpClientHandler clientHandler, RestEaseClientFactoryOptions options = null)
		{
			Serilog.Log.Information("Client Factory Initialized");

			HttpClient httpClient;

			if (options?.EnableHttpLogging == true)
			{
				var loggingHandler = new HttpLoggingHandler(clientHandler);
				if (OnSendAsyncBefore != null) loggingHandler.OnSendAsyncBefore += OnSendAsyncBefore;
				if (OnSendAsyncAfter != null) loggingHandler.OnSendAsyncAfter += OnSendAsyncAfter;

				httpClient = new HttpClient(new HttpLoggingHandler(clientHandler))
				{
					BaseAddress = new Uri(url)
				};
			}
			else
			{
				httpClient = new HttpClient(clientHandler)
				{
					BaseAddress = new Uri(url)
				};
			}

			return InitializeAsync(httpClient, options);
		}

		/// <summary>
		/// Initializes the asynchronous.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public Task<bool> InitializeAsync(HttpClient httpClient, RestEaseClientFactoryOptions options = null)
		{
			Serilog.Log.Information("Client Factory Initialized");

			try
			{
				HttpClient = httpClient;
				Options = options;

				ApiClient = new RestClient(HttpClient)
				{
					ResponseDeserializer = Options?.ResponseDeserializer,
					RequestBodySerializer= Options?.JsonRequestBodySerializer,
					JsonSerializerSettings = Options?.JsonSerializerSettings
				};

				Api = ApiClient.For<T>();

				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
				Serilog.Log.Error(ex, "Failed to initialize API Client");
			}

			return Task.FromResult(false);
		}
	}

	public class RestEaseClientFactoryOptions
	{
		/// <summary>
		/// Gets or sets a value indicating whether [enable HTTP logging].
		/// </summary>
		/// <value>
		///   <c>true</c> if [enable HTTP logging]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableHttpLogging { get; set; } = false;

		public RestEaseHybridResponseDeserializer ResponseDeserializer { get; set; } = new RestEaseHybridResponseDeserializer();

		public JsonRequestBodySerializer JsonRequestBodySerializer { get; set; } = new JsonRequestBodySerializer()
		{
			JsonSerializerSettings =
				new JsonSerializerSettings
				{
					DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
					NullValueHandling = NullValueHandling.Ignore,
					Formatting = Formatting.Indented
				}
		};

		public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
			NullValueHandling = NullValueHandling.Ignore,
			Formatting = Formatting.Indented
		};
	}
}
