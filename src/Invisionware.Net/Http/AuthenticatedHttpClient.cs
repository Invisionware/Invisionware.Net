using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Invisionware.Net
{
    /// <summary>
    /// Class AuthenticatedHttpClientHandler.
    /// </summary>
    /// <seealso cref="System.Net.Http.HttpClientHandler" />
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        /// <summary>
        /// The get token function
        /// </summary>
        private readonly Func<Task<string>> _getToken;

        /// <summary>
        /// Gets or sets the authentication scheme.
        /// </summary>
        /// <value>The authentication scheme.</value>
        public string AuthenticationScheme { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedHttpClientHandler"/> class.
        /// </summary>
        /// <param name="getToken">The get token.</param>
        /// <exception cref="ArgumentNullException"><paramref name="getToken" /> is <see langword="null" />.</exception>
        public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
        {
            if (getToken == null) throw new ArgumentNullException(nameof(getToken));
            _getToken = getToken;
        }

        /// <summary>
        /// send as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _getToken().ConfigureAwait(false);

            // See if the request has an authorize header
            var auth = request.Headers.Authorization;

            if (!string.IsNullOrEmpty(AuthenticationScheme))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, token);
            }
            else if (auth != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }
            else
            {
                request.Headers.Add("Authorization", token);
            }

            if (request.Headers.Authorization != null)
            {
                Serilog.Log.Debug($"HTTP Authentication Header: [{request.Headers.Authorization.Scheme} {request.Headers.Authorization.Parameter}]");
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
