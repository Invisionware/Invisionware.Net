using FakeItEasy;
using FluentAssertions;
using Invisionware.Net.GeoCoding;
using NUnit.Framework;
using System;

namespace Invisionware.Net.GeoCoding.Tests
{
	[TestFixture(Category = "", Description = "Implements Unit Tests for GeoLocationExtensions")]
	public class GeoLocationExtensionsTests
	{
		[Test]
		public void DistanceBetween_Miles_Valid()
		{
			// Arrange
			IGeoLocation location1 = A.Fake<IGeoLocation>();
			IGeoLocation location2 = A.Fake<IGeoLocation>();

			location1.Latitude = 39.90120981809262;
			location1.Longitude = -75.17199754714966;

			location2.Latitude = 39.9128075;
			location2.Longitude = -75.1725466;

			// Act
			var result = location1.DistanceBetween(location2, GeoLocationDistanceUnits.Miles);

			// Assert
			result.Should().Be(0.80181203009432145);
		}

		[Test]
		public void DistanceBetween_Kilometers_Valid()
		{
			// Arrange
			IGeoLocation location1 = A.Fake<IGeoLocation>();
			IGeoLocation location2 = A.Fake<IGeoLocation>();

			location1.Latitude = 39.90120981809262;
			location1.Longitude = -75.17199754714966;

			location2.Latitude = 39.9128075;
			location2.Longitude = -75.1725466;

			// Act
			var result = location1.DistanceBetween(location2, GeoLocationDistanceUnits.Kilometers);

			// Assert
			result.Should().Be(1.2903913797601156);
		}

		[Test]
		public void DistanceBetween_Meters_Valid()
		{
			// Arrange
			IGeoLocation location1 = A.Fake<IGeoLocation>();
			IGeoLocation location2 = A.Fake<IGeoLocation>();

			location1.Latitude = 39.90120981809262;
			location1.Longitude = -75.17199754714966;

			location2.Latitude = 39.9128075;
			location2.Longitude = -75.1725466;

			// Act
			var result = location1.DistanceBetween(location2, GeoLocationDistanceUnits.Meters);

			// Assert
			result.Should().Be(1290.3913797601157967);
		}

		[Test]
		public void DistanceBetween_NauticalMiles_Valid()
		{
			// Arrange
			IGeoLocation location1 = A.Fake<IGeoLocation>();
			IGeoLocation location2 = A.Fake<IGeoLocation>();

			location1.Latitude = 39.90120981809262;
			location1.Longitude = -75.17199754714966;

			location2.Latitude = 39.9128075;
			location2.Longitude = -75.1725466;

			// Act
			var result = location1.DistanceBetween(location2, GeoLocationDistanceUnits.NauticalMiles);

			// Assert
			result.Should().Be(0.69662209391339824);
		}
	}
}
