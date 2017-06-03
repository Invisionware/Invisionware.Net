using Invisionware.Net.WebUtils;
using NUnit.Framework;

namespace Invisionware.Net.WebUitls.Tests
{
	[TestFixture]
	public class UrlBuilderTests
	{
		private string _url1 = "http://someplace.com";
		private string _url2 = "http://someplace.com/path/";
		private string _url3 = "http://someplace.com/path/page.cool";
		private string _url4 = "http://someplace.com/path/page.cool?param1=value1&param2=3&param3=1 > some&param4";

		[SetUp]
		public void Initialize()
		{
			
		}

		[Test]
		public void UrlParseTest1()
		{
			var ub = new UrlBuilder(_url1);

			Assert.AreEqual("someplace.com", ub.Host);
		}

		[Test]
		public void UrlParseTest2()
		{
			var ub = new UrlBuilder(_url2);

			Assert.AreEqual("someplace.com", ub.Host);
			Assert.AreEqual("/path/", ub.Path);
		}

		[Test]
		public void UrlParseTest3()
		{
			var ub = new UrlBuilder(_url3);

			Assert.AreEqual("someplace.com", ub.Host);
			Assert.AreEqual("/path/page.cool", ub.Path);
			Assert.AreEqual("page.cool", ub.PageName);
		}

		[Test]
		public void UrlParseTest4()
		{
			var ub = new UrlBuilder(_url4);

			Assert.AreEqual("someplace.com", ub.Host);
			Assert.AreEqual("/path/page.cool", ub.Path);
			Assert.AreEqual("page.cool", ub.PageName);
			Assert.IsTrue(ub.QueryString["param1"] == "value1");
			Assert.IsTrue(ub.QueryString.ContainsKey("param4"));
		}

		[Test]
		public void UrlQueryStringTest1()
		{
			var ub = new UrlBuilder(_url4);

			Assert.IsTrue(ub.QueryString["param1"] == "value1");

			ub.QueryString["param1"] = "value2";

			Assert.IsTrue(ub.QueryString["param1"] == "value2");
		}
	}
}
