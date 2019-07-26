using FluentAssertions;
using Invisionware.Net.Http;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Invisionware.Net.Tests.Http
{
	[TestFixture(Category = "Http", Description = "Implements Unit Tests for HttpLoggingHandler")]
	public class HttpLoggingHandlerTests
	{
		[Test]
		public async Task VerifyBeforeAfterEventHandlers()
		{
			// Arrange
			var httpLoggingHandler = new HttpLoggingHandler();
			var httpClient = new HttpClient(httpLoggingHandler);
			var url = "https://google.com";

			bool beforeEventTriggers = false;
			bool aferEventTriggers = false;

			httpLoggingHandler.OnSendAsyncBefore += (obj, args) => { args.Request.RequestUri.Should().Be(url); beforeEventTriggers = true; };
			httpLoggingHandler.OnSendAsyncAfter += (obj, args) => { args.Response.Should().NotBeNull(); args.Response.Content.Should().NotBeNull(); aferEventTriggers = true; };

			// Act
			await httpClient.GetAsync(url);

			beforeEventTriggers.Should().BeTrue();
			aferEventTriggers.Should().BeTrue();
		}
	}
}
