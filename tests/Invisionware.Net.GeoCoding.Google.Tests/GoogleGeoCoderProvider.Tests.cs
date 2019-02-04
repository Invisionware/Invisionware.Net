using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invisionware.IoC;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Invisionware.Net.GeoCoding.Google.Tests
{
	[TestFixture]
	public class GoogleGeoCoderProviderTests
	{
		private IGeoCoderProvider _provider;

		[SetUp]
		public Task InitializeAsync()
		{
			var config = new ConfigurationBuilder()
								.AddJsonFile("appsettings.json", true, true)
								.AddJsonFile("appsettings.dev.json", true, true) // This is used for local work so API key is not added to git
								.Build();

			_provider = new GoogleGeoCoderProvider();

			var container = Resolver.Resolve<IDependencyContainer>();
			container.Register<IGeoCoderProvider>(t => _provider);

			_provider.Initialize(coderProvider =>
			{
				coderProvider.APIKey = config["googleApiKey"];
			});

			return Task.FromResult(true);
		}

		[Test]
		[Category("Geo")]
		[Category("Geo.Search.Google")]
		[TestCaseSource(typeof(UnitTestData), nameof(UnitTestData.GeoAddresses))]
		public async Task GeoSearchTest(IGeoAddress geoAddress, bool shouldBeValid)
		{
			var request = new GeoSearchRequest
							{
								Distance = 1000,
								Name = geoAddress.Name,
								Address = geoAddress
								//AddressTypes = new List<AddressTypes> { AddressTypes.Stadium, AddressTypes.Establishment, AddressTypes.Store } <- not working right now
							};

			var result = await _provider.SearchAsync(request).ConfigureAwait(true);

			if (shouldBeValid)
			{
				result.Should().NotBeNull();

				result.Items.Should().NotBeEmpty();

				var addressFound = result.Items.First();

				if (!string.IsNullOrEmpty(geoAddress.Line1)) addressFound.Line1.Should().BeEquivalentTo(geoAddress.Line1);
				if (!string.IsNullOrEmpty(geoAddress.Line2)) addressFound.Line2.Should().BeEquivalentTo(geoAddress.Line2);
				if (!string.IsNullOrEmpty(geoAddress.Region)) addressFound.Region.Should().BeEquivalentTo(geoAddress.Region);
				if (!string.IsNullOrEmpty(geoAddress.Country)) addressFound.Country.Should().BeEquivalentTo(geoAddress.Country);

				if (geoAddress.Location != null)
				{
					addressFound.Location.Should().NotBeNull();	
					addressFound.Location.Latitude.Should().BeApproximately(geoAddress.Location.Latitude.Value, 1.0);
					addressFound.Location.Longitude.Should().BeApproximately(geoAddress.Location.Longitude.Value, 1.0);
				}

				if (geoAddress.Source != null)
				{
					addressFound.Source.Should().NotBeNull();
					//addressFound.Source.ObjectID.Should().Be(geoAddress.Source.ObjectID);
					//addressFound.Source.Provider.Should().Be(geoAddress.Source.Provider);
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
		[TestCaseSource(typeof(UnitTestData), nameof(UnitTestData.GeoAddresses))]
#pragma warning disable RECS0154 // Parameter is never used
		public async Task GeoFindByLocationTest(IGeoAddress geoAddress, bool shouldBeValid)
#pragma warning restore RECS0154 // Parameter is never used
		{
			var result = await _provider.FindAsync(geoAddress.Location);

			if (shouldBeValid)
			{
				result.Should().NotBeNull();

				result.Items.Should().NotBeEmpty();

				var addressFound = result.Items.First();

				addressFound.Line1.Should().BeEquivalentTo(geoAddress.Line1);
				addressFound.Region.Should().BeEquivalentTo(geoAddress.Region);
				addressFound.Country.Should().BeEquivalentTo(geoAddress.Country);
				addressFound.Location.Should().NotBeNull();
				addressFound.Location.Latitude.Should().BeApproximately(geoAddress.Location.Latitude.Value, 1.0);
				addressFound.Location.Longitude.Should().BeApproximately(geoAddress.Location.Longitude.Value, 1.0);
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
		[TestCaseSource(typeof(UnitTestData), nameof(UnitTestData.GeoAddresses))]
#pragma warning disable RECS0154 // Parameter is never used
		public async Task GeoFindByAddressTest(IGeoAddress geoAddress, bool shouldBeValid)
#pragma warning restore RECS0154 // Parameter is never used
		{
			var result = await _provider.FindAsync(geoAddress);

			if (shouldBeValid)
			{
				result.Should().NotBeNull();
				result.Items.Should().NotBeEmpty();

				var addressFound = result.Items.First();

				addressFound.Line1.Should().BeEquivalentTo(geoAddress.Line1);
				addressFound.Region.Should().BeEquivalentTo(geoAddress.Region);
				addressFound.Country.Should().BeEquivalentTo(geoAddress.Country);
				addressFound.Location.Should().NotBeNull();
				addressFound.Location.Latitude.Should().BeApproximately(geoAddress.Location.Latitude.Value, 1.0);
				addressFound.Location.Longitude.Should().BeApproximately(geoAddress.Location.Longitude.Value, 1.0);
			}
			else
			{
				//result.Should().BeNull();
				//result.Items.Should().BeEmpty();
			}
		}

	}
}
