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

using Invisionware.Net.Http;
using Newtonsoft.Json;
using RestEase;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Invisionware.Net
{
    /// <summary>
    /// Class ClientFactory.
    /// </summary>
    public class ClientFactory<T> : IClientFactory<T>
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
        /// initialize as an asynchronous operation.
        /// </summary>
        /// <param name="dataSource">The settings.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> InitializeAsync(string token, string url)
        {
            Serilog.Log.Information("Client Factory Initialized");
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token), "Data Source cannot be null");

            try
            {
#if DEBUG
                Serilog.Log.Debug($"Authentication Token: {token}");
#endif

                HttpClient =
                    new HttpClient(
                        new AuthenticatedHttpClientHandler(() => Task.FromResult(token))
                        {
                            AuthenticationScheme = "Bearer"
                        })
                    {
                        BaseAddress = new Uri(url)
                    };

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

                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Failed to initialize LightSpeed API Client");
            }

            return false;
        }
    }
}
