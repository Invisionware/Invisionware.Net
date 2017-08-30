using FluentAssertions;
using Invisionware.Net.WebUtils;
using Invisionware.Net.WebUtils.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace Invisionware.Net.WebUitls.Tests
{
	[TestFixture]
	public class QueryStringSerializationTests
	{
		private QueryStringParamOptions _serializationOptionsAttributeFilter1 =
			new QueryStringParamOptions
			{
					IgnorePropertiesWithoutAttribute = true
				};

		private QueryStringParamOptions _serializationOptionsAttributeFilter2 =
			new QueryStringParamOptions
			{
				PropertyFilter = (p) => p.Name != "ParamList1" && p.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName != "ParamList1"
			};

		private QueryStringParamOptions _serializationOptionsUrlDisableEncode1 =
			new QueryStringParamOptions
			{
				UrlEncodeKeyName = false,
				UrlEncodeValue = false
			};

		private QueryStringParamOptions _serializationOptionsUrlQueryParamFunc1 =
			new QueryStringParamOptions
			{
				QueryParamJoinFunc = (paramName, formmattedParam) =>
										{
											if (paramName == "Param1") return formmattedParam + "Modfied";
											return formmattedParam;
										}
			};



		[SetUp]
		public void Initialize()
		{

		}

		[Test]
		public void TestSimpleClassDefaultOptions()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.ToQueryString();

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("Param1=Class1%20Value");
			result.Should().Contain("Param2=1");
			result.Should().Contain("ParamList1=listItem1%2ClistItem2%2ClistItem3");
			result.Should().NotContain("someParam1=Class1Value");
		}

		[Test]
		public void TestSimpleClassOnlyMarkedAttributes()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.ToQueryString(_serializationOptionsAttributeFilter1);

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("Param1=Class1%20Value");
			result.Should().Contain("Param2=1");
			result.Should().NotContain("ParamList1=listItem1%2ClistItem2%2ClistItem3");

		}

		[Test]
		public void TestSimpleClassIgnoreBasedOnAttributes()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.ToQueryString(_serializationOptionsAttributeFilter2);

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("Param1=Class1%20Value");
			result.Should().Contain("Param2=1");
			result.Should().NotContain("ParamList1=listItem1%2ClistItem2%2ClistItem3");
		}

		[Test]
		public void TestClassDoNotEncodListValues()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.ToQueryString(_serializationOptionsUrlDisableEncode1);

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("Param1=Class1 Value");
			result.Should().Contain("Param2=1");
			result.Should().Contain("ParamList1=listItem1,listItem2,listItem3");            
		}

		[Test]
		public void TestJsonPropertyName()
		{
			var obj = new DictionaryObjectTestClass2();
			var result = obj.ToQueryString();

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("someParam1Json=Class2Value");
			result.Should().Contain("someParam2=2");
			result.Should().Contain("someParamList1Xml=listItem1%2ClistItem2%2ClistItem3");
		}

		[Test]
		public void TestXmlElementPropertyName()
		{
			var obj = new DictionaryObjectTestClass2();
			var result = obj.ToQueryString();

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("someParam1Json=Class2Value");
			result.Should().NotContain("someParam1=Class2Value");
			result.Should().Contain("someParam2=2");
			result.Should().Contain("someParamList1Xml=listItem1%2ClistItem2%2ClistItem3");
		}

		[Test]
		public void TestMultipleAttributePropertyName()
		{
			var obj = new DictionaryObjectTestClass3();
			var result = obj.ToQueryString();

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("someParam1=ClassInterfaceValue1");
			result.Should().NotContain("someParam1Json=ClassInterfaceValue1");
			result.Should().Contain("Param2=3");
			result.Should().Contain("someParam3=Class3Param3%20Value");
			result.Should().NotContain("someParam3Json=Class3Param3%20Value");
		}

		[Test]
		public void TestInterface()
		{
			var obj = new DictionaryObjectTestClass3();
			var result = obj.ToQueryString(_serializationOptionsUrlDisableEncode1);

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("someParam1=ClassInterfaceValue1");
			result.Should().Contain("someParam2=3");
		}

		[Test]
		public void TestQueryParamFunc1()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.ToQueryString(_serializationOptionsUrlQueryParamFunc1);

			result.Should().NotBeNullOrEmpty("Query String creation failed");
			result.Should().Contain("Param1=Class1%20ValueModfied");
			result.Should().Contain("Param2=1");
			result.Should().Contain("ParamList1=listItem1%2ClistItem2%2ClistItem3");
			result.Should().NotContain("someParam1=Class1Value");		
		}
	}

	public class DictionaryObjectTestClass1
	{
		[DictionaryElement()]
		public string Param1 { get; set; } = "Class1 Value";
		[DictionaryElement()]
		public int Param2 { get; set; } = 1;
		public List<string> ParamList1 { get; set; } = new List<string> {"listItem1", "listItem2", "listItem3"};
	}

	public class DictionaryObjectTestClass2
	{
		[JsonProperty("someParam1Json")]
		public string Param1 { get; set; } = "Class2Value";

		[DictionaryElement("someParam2")]
		public int Param2 { get; set; } = 2;

		[XmlElementAttribute("someParamList1Xml")]
		public List<string> ParamList1 { get; set; } = new List<string> { "listItem1", "listItem2", "listItem3" };
	}

	public interface IDictionaryObjectTestInterface1
	{
		[DictionaryElement("someParam1")]
		string Param1 { get; set; }
		[DictionaryElement("someParam2")]
		int Param2 { get; set; }
	}

	public class DictionaryObjectTestClass3 : IDictionaryObjectTestInterface1
	{
		#region Implementation of IQueryStringInterface1

		public string Param1 { get; set; } = "ClassInterfaceValue1";
		public int Param2 { get; set; } = 3;

		[DictionaryElement("someParam3")]
		[JsonProperty("someParam3Json")]
		public string Param3 { get; set; } = "Class3Param3 Value";

		#endregion
	}
}
