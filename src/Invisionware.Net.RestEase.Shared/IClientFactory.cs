using System.Net.Http;
using System.Threading.Tasks;
using RestEase;

namespace Invisionware.Net
{
    public interface IClientFactory<T>
    {
        /// <summary>
        /// Gets the API.
        /// </summary>
        /// <value>
        /// The API.
        /// </value>
        T Api { get; }
        /// <summary>
        /// Gets the API client.
        /// </summary>
        /// <value>
        /// The API client.
        /// </value>
        RestClient ApiClient { get; }
        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <value>
        /// The HTTP client.
        /// </value>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="token">The token.</param>
        /// <param name="enableHttpLogging">if set to <c>true</c> [enable HTTP logging].</param>
        /// <returns></returns>
        Task<bool> InitializeAsync(string url, string token, bool enableHttpLogging = false);

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="clientHandler">The client handler.</param>
        /// <param name="enableHttpLogging">if set to <c>true</c> [enable HTTP logging].</param>
        /// <returns></returns>
        Task<bool> InitializeAsync(string url, HttpClientHandler clientHandler, bool enableHttpLogging = false);
    }
}