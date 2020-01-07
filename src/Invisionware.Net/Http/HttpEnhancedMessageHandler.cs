using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Invisionware.Net.Http
{
	public class HttpEnhancedMessageHandler : DelegatingHandler
	{
		public delegate void OnSendAsyncHandler(object sender, HttpLoggingnHandlerEventArgs args);

		public event OnSendAsyncHandler OnSendAsyncBefore;
		public event OnSendAsyncHandler OnSendAsyncAfter;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpLoggingHandler"/> class.
		/// </summary>
		/// <param name="innerHandler">The inner handler.</param>
		public HttpEnhancedMessageHandler(HttpMessageHandler innerHandler = null)
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
			OnSendAsyncBefore?.Invoke(this, new HttpLoggingnHandlerEventArgs(request));

			var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

			OnSendAsyncAfter?.Invoke(this, new HttpLoggingnHandlerEventArgs(request, response));

			return response;
		}
	}

	public class HttpLoggingnHandlerEventArgs : EventArgs
	{
		public HttpLoggingnHandlerEventArgs(HttpRequestMessage request)
		{
			Request = request;
		}

		public HttpLoggingnHandlerEventArgs(HttpRequestMessage request, HttpResponseMessage response)
		{
			Request = request;
			Response = response;
		}

		public HttpRequestMessage Request { get; private set; }
		public HttpResponseMessage Response { get; private set; }
	}
}
