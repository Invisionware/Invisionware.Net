using Invisionware.IoC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Net.GeoCoding.Google.Tests
{
	public class UnitTestData
	{
		public static UnitTestData TestData => new UnitTestData();

		static UnitTestData()
		{
			var container = new Invisionware.IoC.Autofac.AutofacContainer(new Autofac.ContainerBuilder().Build());
			Resolver.SetResolver(container.GetResolver());

			container.Register<IDependencyContainer>(t => container);

			Invisionware.Net.GeoCoding.ModelMapper.Map();
		}

		public IGeoAddress ValidAddress => new GeoAddress
		{
			Name = "Wells Fargo Center",
			Line1 = "3601 South Broad Street",
			Line2 = "",
			City = "Philadephia",
			Region = "Pennsylvania",
			Country = "United States",
			PostalCode = "19148",
			Location = new GeoLocation
			{
				Latitude = 39.901662502007774,
				Longitude = -75.17271637916565
			}
		};

		public IGeoAddress InvalidAddress => new GeoAddress
		{
			Name = "Some WellsFargo Location",
			Line1 = "3A6G01 South Broad Street",
			Line2 = "",
			City = "Philadepia",
			Region = "NJ",
			Country = "US",
			PostalCode = "19148",
			Location = new GeoLocation
			{
				Longitude = -975.172633,
				Latitude = 1039.901171
			}
		};

		public static IEnumerable<TestCaseData> GeoAddresses()
		{
			yield return new TestCaseData(TestData.ValidAddress, true).SetCategory("GeoCoding::Address");
			yield return new TestCaseData(TestData.InvalidAddress, false).SetCategory("GeoCoding::Address");
		}
	}
}
