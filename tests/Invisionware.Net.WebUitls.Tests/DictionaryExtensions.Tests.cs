using System.Collections.Generic;
using Invisionware.Collections;
using NUnit.Framework;

namespace Invisionware.Net.WebUitls.Tests
{
	[TestFixture]
	public class DictionaryExtensionsTests
	{
		private readonly IDictionary<string, string> _queryString = new Dictionary<string, string>
		{
			{"param1", "value1"},
			{"param2", "1"},
			{"param3", "some value"},
			{"param4", "value > value1"},
			{"param5", ""},
		};
			
		[SetUp]
		public void Initialize()
		{
			
		}

		[Test]
		public void ToQueryStringTest()
		{
			var result = _queryString.ToQueryString();

			Assert.AreEqual("param1=value1&param2=1&param3=some value&param4=value > value1&param5", result);			
		}

		[Test]
		public void ToQueryStringUrlEncodeParamsTest()
		{
			var result = _queryString.ToQueryString(true);

			Assert.AreEqual("param1=value1&param2=1&param3=some+value&param4=value+%3E+value1&param5", result);
		}

		[Test]
		public void ToQueryStringIncludeEmptyTest()
		{
			var result = _queryString.ToQueryString(false, false);

			Assert.AreEqual("param1=value1&param2=1&param3=some value&param4=value > value1", result);
		}

		[Test]
		public void ToQueryStringUrlEncodeAndIncludeEmptyTest()
		{
			var result = _queryString.ToQueryString(true, false);

			Assert.AreEqual("param1=value1&param2=1&param3=some+value&param4=value+%3E+value1", result);
		}
	}
}
