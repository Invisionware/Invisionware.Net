using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RestEase;

namespace Invisionware.Net.RestEase.Tests
{
	[TestFixture]
	public class ClientFactory
	{
		[SetUp]
		public void Initialize()
		{
		}

		[Test]
		public async Task ClientFactorTest()
		{
			var factory = new RestEaseClientFactory<ITestAPI>();

			await factory.InitializeAsync("https://someurl.com");

			factory.Api.Should().NotBeNull();
		}
	}

	public interface ITestAPI
	{
		[Get("/name")]
		Task<string> GetNameAsync();
	}
}
