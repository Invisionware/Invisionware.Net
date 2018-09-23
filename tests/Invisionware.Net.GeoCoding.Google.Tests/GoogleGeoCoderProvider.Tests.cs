using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invisionware.IoC;
using System.Configuration;

namespace Invisionware.Net.GeoCoding.Google.Tests
{
	[TestFixture]
	public class GoogleGeoCoderProviderTests
	{
		private IGeoCoderProvider _provider;

		static GoogleGeoCoderProviderTests()
		{
			var container = new Invisionware.IoC.Autofac.AutofacContainer(new Autofac.ContainerBuilder().Build());
			Resolver.SetResolver(container.GetResolver());

			container.Register<IDependencyContainer>(t => container);
		}

		[SetUp]
		public Task InitializeAsync()
		{

			_provider = new GoogleGeoCoderProvider();

			var container = Resolver.Resolve<IDependencyContainer>();
			container.Register<IGeoCoderProvider>(t => _provider);

			Invisionware.Net.GeoCoding.ModelMapper.Map();

			_provider.Initialize(coderProvider =>
			{
				coderProvider.APIKey = ConfigurationManager.AppSettings["GoogleApiKey"];
			});

			return Task.FromResult(true);
		}

		[Test]
		[Category("Geo")]
		[Category("Geo.Search.Google")]
		[TestCaseSource(nameof(AddressSearchTestCaseItems))]
		public async Task GeoSearchTest(string name, int distance, IGeoAddress geoAddress, IGeoAddress geoAddressValidResult, bool shouldBeValid)
		{
			var request = new GeoSearchRequest
							{
								Distance = distance,
								Name = name,
								Address = geoAddress
								//AddressTypes = new List<AddressTypes> { AddressTypes.Stadium, AddressTypes.Establishment, AddressTypes.Store } <- not working right now
							};

			var result = await _provider.SearchAsync(request).ConfigureAwait(true);

			if (shouldBeValid)
			{
				result.Should().NotBeNull();

				result.Items.Should().NotBeEmpty();

				var addressFound = result.Items.First();

				if (!string.IsNullOrEmpty(geoAddressValidResult.Line1)) addressFound.Line1.Should().BeEquivalentTo(geoAddressValidResult.Line1);
				if (!string.IsNullOrEmpty(geoAddressValidResult.Line2)) addressFound.Line2.Should().BeEquivalentTo(geoAddressValidResult.Line2);
				if (!string.IsNullOrEmpty(geoAddressValidResult.Region)) addressFound.Region.Should().BeEquivalentTo(geoAddressValidResult.Region);
				if (!string.IsNullOrEmpty(geoAddressValidResult.Country)) addressFound.Country.Should().BeEquivalentTo(geoAddressValidResult.Country);

				if (geoAddressValidResult.Location != null)
				{
					addressFound.Location.Should().NotBeNull();	
					addressFound.Location.Latitude.Should().BeApproximately(geoAddressValidResult.Location.Latitude, 1.0);
					addressFound.Location.Longitude.Should().BeApproximately(geoAddressValidResult.Location.Longitude, 1.0);
				}

				if (geoAddressValidResult.Source != null)
				{
					addressFound.Source.Should().NotBeNull();
					//addressFound.Source.ObjectID.Should().Be(geoAddressValidResult.Source.ObjectID);
					//addressFound.Source.Provider.Should().Be(geoAddressValidResult.Source.Provider);
				}
			}
			else
			{
				//result.Should().BeNull();
				//result.Items.Should().BeEmpty();
			}
		}

		[Test]
		[Category("Geo")]
		[Category("Geo.FindByLocation.Google")]
		[TestCaseSource(nameof(AddressFindTestCaseItems))]
#pragma warning disable RECS0154 // Parameter is never used
		public async Task GeoFindByLocationTest(string name, IGeoAddress geoAddress, IGeoAddress geoAddressValidResult, bool shouldBeValid)
#pragma warning restore RECS0154 // Parameter is never used
		{
			var result = await _provider.FindAsync(geoAddress.Location);

			if (shouldBeValid)
			{
				result.Should().NotBeNull();

				result.Items.Should().NotBeEmpty();

				var addressFound = result.Items.First();

				addressFound.Line1.Should().BeEquivalentTo(geoAddressValidResult.Line1);
				addressFound.Region.Should().BeEquivalentTo(geoAddressValidResult.Region);
				addressFound.Country.Should().BeEquivalentTo(geoAddressValidResult.Country);
				addressFound.Location.Should().NotBeNull();
				addressFound.Location.Latitude.Should().BeApproximately(geoAddressValidResult.Location.Latitude, 1.0);
				addressFound.Location.Longitude.Should().BeApproximately(geoAddressValidResult.Location.Longitude, 1.0);
			}
			else
			{
				if (result == null)
					result.Should().BeNull();
				else
					result.Items.Should().BeEmpty();
			}
		}

		[Test]
		[Category("Geo")]
		[Category("Geo.FindByAddress.Google")]
		[TestCaseSource(nameof(AddressFindTestCaseItems))]
#pragma warning disable RECS0154 // Parameter is never used
		public async Task GeoFindByAddressTest(string name, IGeoAddress geoAddress, IGeoAddress geoAddressValidResult, bool shouldBeValid)
#pragma warning restore RECS0154 // Parameter is never used
		{
			var result = await _provider.FindAsync(geoAddress);

			if (shouldBeValid)
			{
				result.Should().NotBeNull();
				result.Items.Should().NotBeEmpty();

				var addressFound = result.Items.First();

				addressFound.Line1.Should().BeEquivalentTo(geoAddressValidResult.Line1);
				addressFound.Region.Should().BeEquivalentTo(geoAddressValidResult.Region);
				addressFound.Country.Should().BeEquivalentTo(geoAddressValidResult.Country);
				addressFound.Location.Should().NotBeNull();
				addressFound.Location.Latitude.Should().BeApproximately(geoAddressValidResult.Location.Latitude, 1.0);
				addressFound.Location.Longitude.Should().BeApproximately(geoAddressValidResult.Location.Longitude, 1.0);
			}
			else
			{
				//result.Should().BeNull();
				//result.Items.Should().BeEmpty();
			}
		}

		public static IEnumerable<TestCaseData> AddressSearchTestCaseItems()
		{
			// Valid Cases
			yield return new TestCaseData("Wells Fargo Center", 1000,
							new GeoAddress { Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "PA", Country = "US", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.172633, Latitude = 39.901171 } },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1718743, Latitude = 39.9011799 }, Source = new ItemSource { ObjectID = "ChIJR7HjD-zFxokRLbpFP7Ixn1I", Provider = "google"} },
							true).SetName("AddressSearchValid1");
			yield return new TestCaseData("Wells Fargo Center", 1000,
							new GeoAddress { Line1 = "3601 South Broad Street", Line2 = "", City = "", Region = "PA", Country = "US", PostalCode = "19148", Location = null },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1720529, Latitude = 39.9011809 }, Source = new ItemSource { ObjectID = "ChIJ_WUdHezFxokRjF9cmJPUWrQ", Provider = "google" } },
							true).SetName("AddressSearchValid2");
			yield return new TestCaseData("Wells Fargo Center", 1000,
							new GeoAddress { Line1 = "", Line2 = "", City = "Philadephia", Region = "PA", Country = "US", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.172633, Latitude = 39.901171 } },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1718743, Latitude = 39.9011799 }, Source = new ItemSource { ObjectID = "ChIJR7HjD-zFxokRLbpFP7Ixn1I", Provider = "google" } },
							true).SetName("AddressSearchValid3");
			yield return new TestCaseData("Wells Fargo Center", 1000,
							new GeoAddress { Line1 = "", Line2 = "", City = "", Region = "", Country = "", PostalCode = "", Location = new GeoLocation { Longitude = -75.172633, Latitude = 39.901171 } },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1718743, Latitude = 39.9011799 }, Source = new ItemSource { ObjectID = "ChIJR7HjD-zFxokRLbpFP7Ixn1I", Provider = "google" } },
							true).SetName("AddressSearchValid4");

			// Invalid Cases
			yield return new TestCaseData("Wells Fargo Center", 1000,
							new GeoAddress { Line1 = "3A6G01 South Broad Street", Line2 = "", City = "Philadepia", Region = "NJ", Country = "US", PostalCode = "19148", Location = new GeoLocation { Longitude = -975.172633, Latitude = 1039.901171 } },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1718743, Latitude = 39.9011799 }, Source = new ItemSource { ObjectID = "ChIJR7HjD-zFxokRLbpFP7Ixn1I", Provider = "google" } },
							false).SetName("AddressSearchInvalid1");
		}

		public static IEnumerable<TestCaseData> AddressFindTestCaseItems()
		{
			// Valid Cases
			yield return new TestCaseData("Wells Fargo Center", 
							new GeoAddress { Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "PA", Country = "US", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.172633, Latitude = 39.901171 } },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1718743, Latitude = 39.9011799 }, Source = new ItemSource { ObjectID = "ChIJR7HjD-zFxokRLbpFP7Ixn1I", Provider = "google" } },
							true).SetName("AddressFindValid1");

			// Invalid Cases
			yield return new TestCaseData("Wells Fargo Center", 
							new GeoAddress { Line1 = "3A6G01 South Broad Street", Line2 = "", City = "Philadepia", Region = "NJ", Country = "US", PostalCode = "19148", Location = new GeoLocation { Longitude = -975.172633, Latitude = 1039.901171 } },
							new GeoAddress { Name = "3601 S Broad St", Line1 = "3601 South Broad Street", Line2 = "", City = "Philadephia", Region = "Pennsylvania", Country = "United States", PostalCode = "19148", Location = new GeoLocation { Longitude = -75.1718743, Latitude = 39.9011799 }, Source = new ItemSource { ObjectID = "ChIJR7HjD-zFxokRLbpFP7Ixn1I", Provider = "google" } },
							false).SetName("AddressFindInvalid1");

		}
	}
}
