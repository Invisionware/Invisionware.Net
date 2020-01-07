using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Invisionware.Net.Http
{
	/// <summary>
	/// Class HttpLoggingHandler.
	/// </summary>
	/// <seealso cref="System.Net.Http.DelegatingHandler" />
	public class HttpLoggingHandler : HttpEnhancedMessageHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpLoggingHandler"/> class.
		/// </summary>
		/// <param name="innerHandler">The inner handler.</param>
		public HttpLoggingHandler(HttpMessageHandler innerHandler = null)
			: base(innerHandler ?? new HttpClientHandler())
		{ }

		/// <summary>
		/// send as an asynchronous operation.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			await Task.Delay(1).ConfigureAwait(false);

			var id = Guid.NewGuid().ToString();
			var msg = $"[{id} - Request]";

			Log.Debug($"{msg}========Start==========");

			try
			{
				var req = request;

				Log.Verbose($"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
				Log.Debug($"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");

				foreach (var header in req.Headers)
				{
					Log.Debug($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
				}

				if (req.Content != null)
				{
					foreach (var header in req.Content.Headers)
					{
						Log.Debug($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
					}

					if (req.Content is StringContent || req.Headers.IsTextBasedContentType() || req.Content.Headers.IsTextBasedContentType())
					{
						var result = await req.Content.ReadAsStringAsync().ConfigureAwait(false);

						Log.Debug($"{msg} Content:");
#if DEBUG
						Log.Debug($"{msg} {string.Join("", result)}");
#else
					Log.Debug($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
#endif
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{msg} Exception during logging 'presend' details");
			}

			Log.Debug($"{msg} Sending Reqest - Start");

			var start = DateTime.Now;

			var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

			var end = DateTime.Now;

			Log.Debug($"{msg} Sending Reqest - End");

			Log.Debug($"{msg} Duration: {end - start}");
			Log.Debug($"{msg}==========End==========");

			msg = $"[{id} - Response]";
			Log.Debug($"{msg}=========Start=========");

			try
			{
				var resp = response;

				if (!resp.IsSuccessStatusCode)
				{
					Log.Error($"{msg} {resp.RequestMessage.RequestUri.AbsoluteUri} ({resp.RequestMessage.RequestUri.Scheme.ToUpper()}/{resp.Version}) {(int)resp.StatusCode} {resp.ReasonPhrase}");
				}
				else
				{
					Log.Verbose($"{msg} {resp.RequestMessage.RequestUri.AbsoluteUri} ({resp.RequestMessage.RequestUri.Scheme.ToUpper()}/{resp.Version}) {(int)resp.StatusCode} {resp.ReasonPhrase}");
				}

				foreach (var header in resp.Headers)
				{
					Log.Debug($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
				}

				if (resp.Content != null)
				{
					foreach (var header in resp.Content.Headers)
					{
						Log.Debug($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
					}

					if (resp.Content is StringContent || resp.Headers.IsTextBasedContentType() || resp.Content.Headers.IsTextBasedContentType())
					{
						try
						{
							start = DateTime.Now;
							var result = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
							end = DateTime.Now;

							Log.Debug($"{msg} Content:");
#if DEBUG
							Log.Debug($"{msg} {string.Join("", result)}");
#else
					Log.Debug($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
#endif
							Log.Debug($"{msg} Duration: {end - start}");
						}
						catch (Exception ex)
						{
							Log.Debug(ex, "Failed to retrieve content");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{msg} Exception during logging 'postsend' details");
			}

			Log.Debug($"{msg}==========End==========");

			return response;
		}
	}

	public static class HttpHeadersExtensions
	{
		/// <summary>
		/// The types
		/// </summary>
		private static readonly string[] _types = { "html", "text", "xml", "json", "txt" };

		/// <summary>
		/// Determines whether [is text based content type] [the specified headers].
		/// </summary>
		/// <param name="headers">The headers.</param>
		/// <returns><c>true</c> if [is text based content type] [the specified headers]; otherwise, <c>false</c>.</returns>
		public static bool IsTextBasedContentType(this HttpHeaders headers)
		{
			if (headers == null) return false;
			if (!headers.TryGetValues("Content-Type", out IEnumerable<string> values))
			{
				return false;
			}

			var header = string.Join(" ", values).ToLowerInvariant();

			return _types.Any(t => header.Contains(t));
		}

		public static bool IsTextBasedContentType(this HttpRequestHeaders headers)
		{
			return IsTextBasedContentType(headers as HttpHeaders);
		}

		public static bool IsTextBasedContentType(this HttpResponseHeaders headers)
		{
			return IsTextBasedContentType(headers as HttpHeaders);
		}
	}
}
