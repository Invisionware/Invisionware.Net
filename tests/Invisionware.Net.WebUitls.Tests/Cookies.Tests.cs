using System.Net;
using System.Net.Http;
using Invisionware.Net.WebUtils;
using NUnit.Framework;

namespace Invisionware.Net.WebUitls.Tests
{
	[TestFixture]
	public class CookiesTests
	{
		private HttpClient _client;
		private HttpClientHandler _clientHandler;
		private CookieContainer _cookieContainer;

		[SetUp]
		public void Initialize()
		{
			_cookieContainer = new CookieContainer();

			_clientHandler = new HttpClientHandler
			{
				AllowAutoRedirect = true,
				CookieContainer = _cookieContainer,
				MaxAutomaticRedirections = 5,
				UseCookies = true,
			};

			_client = new HttpClient(_clientHandler);
		}

		[Test]
		public void GetCookieCollectionTest()
		{
			var task = _client.GetAsync("http://google.com");

			task.Wait(1000);

			var cookies = task.Result.GetCookieCollection();

			Assert.IsNotNull(cookies);
			Assert.IsTrue(cookies.Count > 0);
		}

		[Test]
		public void GetCookieTest()
		{
			var task = _client.GetAsync("http://google.com");

			task.Wait(1000);

			var cookie = task.Result.GetCookie("NID");

			Assert.IsNotNull(cookie);
			Assert.IsTrue(!string.IsNullOrEmpty(cookie.Value));
		}
	}
}
