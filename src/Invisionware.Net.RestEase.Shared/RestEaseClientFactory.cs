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
    public class RestEaseClientFactory<T> : IClientFactory<T>
    {
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

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="token">The token.</param>
        /// <param name="enableHttpLogging">if set to <c>true</c> [enable HTTP logging].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">token - token cannot be null</exception>
        public Task<bool> InitializeAsync(string url, string token, bool enableHttpLogging = false)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token), "token cannot be null");

#if DEBUG
            Serilog.Log.Debug($"Authentication Token: {token}");
#endif

            return InitializeAsync(url, new AuthenticatedHttpClientHandler(() => Task.FromResult(token))
            {
                AuthenticationScheme = "Bearer"
            });
        }

        /// <summary>
        /// initialize as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="clientHandler">The client handler.</param>
        /// <param name="enableHttpLogging">if set to <c>true</c> [enable HTTP logging].</param>
        /// <returns>
        /// Task&lt;System.Boolean&gt;.
        /// </returns>
        public Task<bool> InitializeAsync(string url, HttpClientHandler clientHandler, bool enableHttpLogging = false)
        {
            Serilog.Log.Information("Client Factory Initialized");

            try
            {
                if (enableHttpLogging)
                {
                    HttpClient = new HttpClient(new HttpLoggingHandler(clientHandler))
                    {
                        BaseAddress = new Uri(url)
                    };
                }
                else
                {
                    HttpClient = new HttpClient(clientHandler)
                    {
                        BaseAddress = new Uri(url)
                    };
                }

                ApiClient = new RestClient(HttpClient)
                {
                    ResponseDeserializer = new RestEaseHybridResponseDeserializer(),
                    RequestBodySerializer = new JsonRequestBodySerializer()
                    {
                        JsonSerializerSettings =
                            new JsonSerializerSettings
                            {
                                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                                NullValueHandling = NullValueHandling.Ignore,
                                Formatting = Formatting.Indented
                            }
                    },
                    JsonSerializerSettings = new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.Indented
                    }
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
}
