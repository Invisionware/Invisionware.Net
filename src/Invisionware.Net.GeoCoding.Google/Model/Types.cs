// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Types.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Enum GoogleStatusCodeTypes
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GoogleStatusCodeTypes
	{
		/// <summary>
		/// indicates that no errors occurred and at least one address was returned
		/// </summary>
		[EnumMember(Value = "OK")]
		Ok,
		/// <summary>
		/// indicates that the reverse geocoding was successful but returned no results. This may occur if the geocoder
		/// was passed a latlng in a remote location.
		/// </summary>
		[EnumMember(Value = "ZERO_RESULTS")]
		ZeroResults,
		/// <summary>
		/// indicates that the reverse geocoding was not found
		/// </summary>
		[EnumMember(Value = "NOT_FOUND")]
		NotFound,
		/// <summary>
		/// indicates that you are over your quota.
		/// </summary>
		[EnumMember(Value = "OVER_QUERY_LIMIT")]				
		OverQueryLimit,
		/// <summary>
		/// indicates that the request was denied. Possibly because the request includes a result_type or location_type
		/// parameter but does not include an API key or client ID.
		/// </summary>
		[EnumMember(Value = "REQUEST_DENIED")]
		RequestDenied,
		/// <summary>
		/// generally indicates one of the following:
		/// The query (address, components or latlng) is missing
		/// An invalid result_type or location_type was given.
		/// </summary>
		[EnumMember(Value = "INVALID_REQUEST")]
		InvalidRequest,
		/// <summary>
		/// indicates that the request could not be processed due to a server error. The request may succeed if
		/// you try again.
		/// </summary>
		[EnumMember(Value = "UNKNOWN_ERROR")]
		UnknownError
	}

	/// <summary>
	/// Enum RankByTypes
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	internal enum RankByTypes
	{
		/// <summary>
		/// The prominence
		/// </summary>
		[EnumMember(Value = "prominence")]
		Prominence,
		/// <summary>
		/// The distance
		/// </summary>
		[EnumMember(Value = "distance")]
		Distance
	}

	#region Legacy
	/// <summary>
	/// These types are value for searching and adding places
	/// https://developers.google.com/places/supported_types
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	internal enum PlaceTypes
	{
		/// <summary>
		/// The accounting
		/// </summary>
		[EnumMember(Value = "accounting")]
		Accounting,
		/// <summary>
		/// The airport
		/// </summary>
		[EnumMember(Value = "airport")]
		Airport,
		/// <summary>
		/// The amusement park
		/// </summary>
		[EnumMember(Value = "amusement_park")]
		AmusementPark,
		/// <summary>
		/// The aquarium
		/// </summary>
		[EnumMember(Value = "aquarium")]
		Aquarium,
		/// <summary>
		/// The art gallery
		/// </summary>
		[EnumMember(Value = "art_gallery")]
		ArtGallery,
		/// <summary>
		/// The atm
		/// </summary>
		[EnumMember(Value = "atm")]
		Atm,
		/// <summary>
		/// The bakery
		/// </summary>
		[EnumMember(Value = "bakery")]
		Bakery,
		/// <summary>
		/// The bank
		/// </summary>
		[EnumMember(Value = "bank")]
		Bank,
		/// <summary>
		/// The bar
		/// </summary>
		[EnumMember(Value = "bar")]
		Bar,
		/// <summary>
		/// The beauty salon
		/// </summary>
		[EnumMember(Value = "beauty_salon")]
		BeautySalon,
		/// <summary>
		/// The bicycle store
		/// </summary>
		[EnumMember(Value = "bicycle_store")]
		BicycleStore,
		/// <summary>
		/// The book store
		/// </summary>
		[EnumMember(Value = "book_store")]
		BookStore,
		/// <summary>
		/// The bowling alley
		/// </summary>
		[EnumMember(Value = "bowling_alley")]
		BowlingAlley,
		/// <summary>
		/// The bus station
		/// </summary>
		[EnumMember(Value = "bus_station")]
		BusStation,
		/// <summary>
		/// The cafe
		/// </summary>
		[EnumMember(Value = "cafe")]
		Cafe,
		/// <summary>
		/// The campground
		/// </summary>
		[EnumMember(Value = "campground")]
		Campground,
		/// <summary>
		/// The car dealer
		/// </summary>
		[EnumMember(Value = "car_dealer")]
		CarDealer,
		/// <summary>
		/// The car rental
		/// </summary>
		[EnumMember(Value = "car_rental")]
		CarRental,
		/// <summary>
		/// The car repair
		/// </summary>
		[EnumMember(Value = "car_repair")]
		CarRepair,
		/// <summary>
		/// The car wash
		/// </summary>
		[EnumMember(Value = "car_wash")]
		CarWash,
		/// <summary>
		/// The casino
		/// </summary>
		[EnumMember(Value = "casino")]
		Casino,
		/// <summary>
		/// The cemetery
		/// </summary>
		[EnumMember(Value = "cemetery")]
		Cemetery,
		/// <summary>
		/// The church
		/// </summary>
		[EnumMember(Value = "church")]
		Church,
		/// <summary>
		/// The city hall
		/// </summary>
		[EnumMember(Value = "city_hall")]
		CityHall,
		/// <summary>
		/// The clothing store
		/// </summary>
		[EnumMember(Value = "clothing_store")]
		ClothingStore,
		/// <summary>
		/// The convenience store
		/// </summary>
		[EnumMember(Value = "convenience_store")]
		ConvenienceStore,
		/// <summary>
		/// The courthouse
		/// </summary>
		[EnumMember(Value = "courthouse")]
		Courthouse,
		/// <summary>
		/// The dentist
		/// </summary>
		[EnumMember(Value = "dentist")]
		Dentist,
		/// <summary>
		/// The department store
		/// </summary>
		[EnumMember(Value = "department_store")]
		DepartmentStore,
		/// <summary>
		/// The doctor
		/// </summary>
		[EnumMember(Value = "doctor")]
		Doctor,
		/// <summary>
		/// The electrician
		/// </summary>
		[EnumMember(Value = "electrician")]
		Electrician,
		/// <summary>
		/// The electronics store
		/// </summary>
		[EnumMember(Value = "electronics_store")]
		ElectronicsStore,
		/// <summary>
		/// The embassy
		/// </summary>
		[EnumMember(Value = "embassy")]
		Embassy,
		/// <summary>
		/// The establishment
		/// </summary>
		[EnumMember(Value = "establishment")]
		Establishment,
		/// <summary>
		/// The finance
		/// </summary>
		[EnumMember(Value = "finance")]
		Finance,
		/// <summary>
		/// The fire station
		/// </summary>
		[EnumMember(Value = "fire_station")]
		FireStation,
		/// <summary>
		/// The florist
		/// </summary>
		[EnumMember(Value = "florist")]
		Florist,
		/// <summary>
		/// The food
		/// </summary>
		[EnumMember(Value = "food")]
		Food,
		/// <summary>
		/// The funeral home
		/// </summary>
		[EnumMember(Value = "funeral_home")]
		FuneralHome,
		/// <summary>
		/// The furniture store
		/// </summary>
		[EnumMember(Value = "furniture_store")]
		FurnitureStore,
		/// <summary>
		/// The gas station
		/// </summary>
		[EnumMember(Value = "gas_station")]
		GasStation,
		/// <summary>
		/// The general contractor
		/// </summary>
		[EnumMember(Value = "general_contractor")]
		GeneralContractor,
		/// <summary>
		/// The grocery or supermarket
		/// </summary>
		[EnumMember(Value = "grocery_or_supermarket")]
		GroceryOrSupermarket,
		/// <summary>
		/// The gym
		/// </summary>
		[EnumMember(Value = "gym")]
		Gym,
		/// <summary>
		/// The hair care
		/// </summary>
		[EnumMember(Value = "hair_care")]
		HairCare,
		/// <summary>
		/// The hardware store
		/// </summary>
		[EnumMember(Value = "hardware_store")]
		HardwareStore,
		/// <summary>
		/// The health
		/// </summary>
		[EnumMember(Value = "health")]
		Health,
		/// <summary>
		/// The hindu temple
		/// </summary>
		[EnumMember(Value = "hindu_temple")]
		HinduTemple,
		/// <summary>
		/// The home goods store
		/// </summary>
		[EnumMember(Value = "home_goods_store")]
		HomeGoodsStore,
		/// <summary>
		/// The hospital
		/// </summary>
		[EnumMember(Value = "hospital")]
		Hospital,
		/// <summary>
		/// The insurance agency
		/// </summary>
		[EnumMember(Value = "insurance_agency")]
		InsuranceAgency,
		/// <summary>
		/// The jewelry store
		/// </summary>
		[EnumMember(Value = "jewelry_store")]
		JewelryStore,
		/// <summary>
		/// The laundry
		/// </summary>
		[EnumMember(Value = "laundry")]
		Laundry,
		/// <summary>
		/// The lawyer
		/// </summary>
		[EnumMember(Value = "lawyer")]
		Lawyer,
		/// <summary>
		/// The library
		/// </summary>
		[EnumMember(Value = "library")]
		Library,
		/// <summary>
		/// The liquor store
		/// </summary>
		[EnumMember(Value = "liquor_store")]
		LiquorStore,
		/// <summary>
		/// The local government office
		/// </summary>
		[EnumMember(Value = "local_government_office")]
		LocalGovernmentOffice,
		/// <summary>
		/// The locksmith
		/// </summary>
		[EnumMember(Value = "locksmith")]
		Locksmith,
		/// <summary>
		/// The lodging
		/// </summary>
		[EnumMember(Value = "lodging")]
		Lodging,
		/// <summary>
		/// The meal delivery
		/// </summary>
		[EnumMember(Value = "meal_delivery")]
		MealDelivery,
		/// <summary>
		/// The meal takeaway
		/// </summary>
		[EnumMember(Value = "meal_takeaway")]
		MealTakeaway,
		/// <summary>
		/// The mosque
		/// </summary>
		[EnumMember(Value = "mosque")]
		Mosque,
		/// <summary>
		/// The movie rental
		/// </summary>
		[EnumMember(Value = "movie_rental")]
		MovieRental,
		/// <summary>
		/// The movie theater
		/// </summary>
		[EnumMember(Value = "movie_theater")]
		MovieTheater,
		/// <summary>
		/// The moving company
		/// </summary>
		[EnumMember(Value = "moving_company")]
		MovingCompany,
		/// <summary>
		/// The museum
		/// </summary>
		[EnumMember(Value = "museum")]
		Museum,
		/// <summary>
		/// The night club
		/// </summary>
		[EnumMember(Value = "night_club")]
		NightClub,
		/// <summary>
		/// The painter
		/// </summary>
		[EnumMember(Value = "painter")]
		Painter,
		/// <summary>
		/// The park
		/// </summary>
		[EnumMember(Value = "park")]
		Park,
		/// <summary>
		/// The parking
		/// </summary>
		[EnumMember(Value = "parking")]
		Parking,
		/// <summary>
		/// The pet store
		/// </summary>
		[EnumMember(Value = "pet_store")]
		PetStore,
		/// <summary>
		/// The pharmacy
		/// </summary>
		[EnumMember(Value = "pharmacy")]
		Pharmacy,
		/// <summary>
		/// The physiotherapist
		/// </summary>
		[EnumMember(Value = "physiotherapist")]
		Physiotherapist,
		/// <summary>
		/// The place of worship
		/// </summary>
		[EnumMember(Value = "place_of_worship")]
		PlaceOfWorship,
		/// <summary>
		/// The plumber
		/// </summary>
		[EnumMember(Value = "plumber")]
		Plumber,
		/// <summary>
		/// The police
		/// </summary>
		[EnumMember(Value = "police")]
		Police,
		/// <summary>
		/// The post office
		/// </summary>
		[EnumMember(Value = "post_office")]
		PostOffice,
		/// <summary>
		/// The real estate agency
		/// </summary>
		[EnumMember(Value = "real_estate_agency")]
		RealEstateAgency,
		/// <summary>
		/// The restaurant
		/// </summary>
		[EnumMember(Value = "restaurant")]
		Restaurant,
		/// <summary>
		/// The roofing contractor
		/// </summary>
		[EnumMember(Value = "roofing_contractor")]
		RoofingContractor,
		/// <summary>
		/// The rv park
		/// </summary>
		[EnumMember(Value = "rv_park")]
		RvPark,
		/// <summary>
		/// The school
		/// </summary>
		[EnumMember(Value = "school")]
		School,
		/// <summary>
		/// The shoe store
		/// </summary>
		[EnumMember(Value = "shoe_store")]
		ShoeStore,
		/// <summary>
		/// The shopping mall
		/// </summary>
		[EnumMember(Value = "shopping_mall")]
		ShoppingMall,
		/// <summary>
		/// The spa
		/// </summary>
		[EnumMember(Value = "spa")]
		Spa,
		/// <summary>
		/// The stadium
		/// </summary>
		[EnumMember(Value = "stadium")]
		Stadium,
		/// <summary>
		/// The storage
		/// </summary>
		[EnumMember(Value = "storage")]
		Storage,
		/// <summary>
		/// The store
		/// </summary>
		[EnumMember(Value = "store")]
		Store,
		/// <summary>
		/// The subway station
		/// </summary>
		[EnumMember(Value = "subway_station")]
		SubwayStation,
		/// <summary>
		/// The synagogue
		/// </summary>
		[EnumMember(Value = "synagogue")]
		Synagogue,
		/// <summary>
		/// The taxi stand
		/// </summary>
		[EnumMember(Value = "taxi_stand")]
		TaxiStand,
		/// <summary>
		/// The train station
		/// </summary>
		[EnumMember(Value = "train_station")]
		TrainStation,
		/// <summary>
		/// The light rail station
		/// </summary>
		[EnumMember(Value = "light_rail_station")]
		LightRailStation,
		/// <summary>
		/// The travel agency
		/// </summary>
		[EnumMember(Value = "travel_agency")]
		TravelAgency,
		/// <summary>
		/// The university
		/// </summary>
		[EnumMember(Value = "university")]
		University,
		/// <summary>
		/// The veterinary care
		/// </summary>
		[EnumMember(Value = "veterinary_care")]
		VeterinaryCare,
		/// <summary>
		/// The zoo
		/// </summary>
		[EnumMember(Value = "zoo")]
		Zoo
	}
	#endregion Legacy

	/// <summary>
	/// Address Types represents ALL possible address types.  Note, this list contains values that are ONLY RETURNED by google
	/// and cannot be used for searching or adding new addresses
	/// https://developers.google.com/places/supported_types
	/// https://developers.google.com/maps/documentation/geocoding/
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GoogleAddressTypes
	{
		/// <summary>
		/// indicates a precise street address.
		/// </summary>
		[EnumMember(Value = "street_address")]
		StreetAddress,
		/// <summary>
		/// indicates a named route (such as "US 101").
		/// </summary>
		[EnumMember(Value = "route")]
		Route,
		/// <summary>
		/// indicates a major intersection, usually of two major roads.
		/// </summary>
		[EnumMember(Value = "intersection")]
		Intersection,
		/// <summary>
		/// indicates a political entity. Usually, this type indicates a polygon of some civil administration.
		/// </summary>
		[EnumMember(Value = "political")]
		Political,
		/// <summary>
		/// indicates the national political entity, and is typically the highest order type returned by the Geocoder.
		/// </summary>
		[EnumMember(Value = "country")]
		Country,
		/// <summary>
		/// indicates a first-order civil entity below the country level. Within the United States, these administrative
		/// levels are states. Not all nations exhibit these administrative levels.
		/// </summary>
		[EnumMember(Value = "administrative_area_level_1")]
		AdministrativeAreaLevel1,
		/// <summary>
		/// indicates a second-order civil entity below the country level. Within the United States, these administrative
		/// levels are counties. Not all nations exhibit these administrative levels.
		/// </summary>
		[EnumMember(Value = "administrative_area_level_2")]
		AdministrativeAreaLevel2,
		/// <summary>
		/// indicates a third-order civil entity below the country level. This type indicates a minor civil division.
		/// Not all nations exhibit these administrative levels.
		/// </summary>
		[EnumMember(Value = "administrative_area_level_3")]
		AdministrativeAreaLevel3,
		/// <summary>
		/// indicates a fourth-order civil entity below the country level. This type indicates a minor civil division.
		/// Not all nations exhibit these administrative levels.
		/// </summary>
		[EnumMember(Value = "administrative_area_level_4")]
		AdministrativeAreaLevel4,
		/// <summary>
		/// indicates a fifth-order civil entity below the country level. This type indicates a minor civil division.
		/// Not all nations exhibit these administrative levels.
		/// </summary>
		[EnumMember(Value = "administrative_area_level_5")]
		AdministrativeAreaLevel5,
		/// <summary>
		/// indicates a commonly-used alternative name for the entity.
		/// </summary>
		[EnumMember(Value = "colloquial_area")]
		ColloquialArea,
		/// <summary>
		/// indicates an incorporated city or town political entity.
		/// </summary>
		[EnumMember(Value = "locality")]
		Locality,
		/// <summary>
		/// indicates a specific type of Japanese locality, to facilitate distinction between multiple locality components
		/// within a Japanese address.
		/// </summary>
		[EnumMember(Value = "ward")]
		Ward,
		/// <summary>
		/// indicates a first-order civil entity below a locality. For some locations may receive one of the additional
		/// types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers
		/// indicate a smaller geographic area.
		/// </summary>
		[EnumMember(Value = "sublocality")]
		Sublocality,
		/// <summary>
		/// indicates a named neighborhood
		/// </summary>
		[EnumMember(Value = "neighborhood")]
		Neighborhood,
		/// <summary>
		/// indicates a named location, usually a building or collection of buildings with a common name
		/// </summary>
		[EnumMember(Value = "premise")]
		Premise,
		/// <summary>
		/// indicates a first-order entity below a named location, usually a singular building within a collection of
		/// buildings with a common name
		/// </summary>
		[EnumMember(Value = "subpremise")]
		Subpremise,
		/// <summary>
		/// indicates a postal code as used to address postal mail within the country.
		/// </summary>
		[EnumMember(Value = "postal_code")]
		PostalCode,
		/// <summary>
		/// indicates a prominent natural feature.
		/// </summary>
		[EnumMember(Value = "natural_feature")]
		NaturalFeature,
		/// <summary>
		/// indicates an airport.
		/// </summary>
		[EnumMember(Value = "airport")]
		Airport,
		/// <summary>
		/// indicates a named park.
		/// </summary>
		[EnumMember(Value = "park")]
		Park,
		/// <summary>
		/// indicates a named point of interest. Typically, these "POI"s are prominent local entities that don't easily
		/// fit in another category, such as "Empire State Building" or "Statue of Liberty."
		/// </summary>
		[EnumMember(Value = "point_of_interest")]
		PointOfInterest,
		/// <summary>
		/// indicates the floor of a building address.
		/// </summary>
		[EnumMember(Value = "floor")]
		Floor,
		/// <summary>
		/// typically indicates a place that has not yet been categorized.
		/// </summary>
		[EnumMember(Value = "establishment")]
		Establishment,
		/// <summary>
		/// indicates a parking lot or parking structure.
		/// </summary>
		[EnumMember(Value = "parking")]
		Parking,
		/// <summary>
		/// indicates a specific postal box.
		/// </summary>
		[EnumMember(Value = "post_box")]
		PostBox,
		/// <summary>
		/// indicates a grouping of geographic areas, such as locality and sublocality, used for mailing addresses
		/// in some countries.
		/// </summary>
		[EnumMember(Value = "postal_town")]
		PostalTown,
		/// <summary>
		/// indicates the room of a building address.
		/// </summary>
		[EnumMember(Value = "room")]
		Room,
		/// <summary>
		/// indicates the precise street number
		/// </summary>
		[EnumMember(Value = "street_number")]
		StreetNumber,
		/// <summary>
		/// indicate the location of a bus, train or public transit stop.
		/// </summary>
		[EnumMember(Value = "bus_station")]
		BusStation,
		/// <summary>
		/// indicate the location of a bus, train or public transit stop.
		/// </summary>
		[EnumMember(Value = "train_station")]
		TrainStation,
		/// <summary>
		/// indicate the location of a bus, train or public transit stop.
		/// </summary>
		[EnumMember(Value = "transit_station")]
		TransitStation,
		/// <summary>
		/// The geocode
		/// </summary>
		[EnumMember(Value = "geocode")]
		Geocode,
		/// <summary>
		/// The postal code prefix
		/// </summary>
		[EnumMember(Value = "postal_code_prefix")]
		PostalCodePrefix,
		/// <summary>
		/// The postal code suffix
		/// </summary>
		[EnumMember(Value = "postal_code_suffix")]
		PostalCodeSuffix,
		/// <summary>
		/// The sublocality level5
		/// </summary>
		[EnumMember(Value = "sublocality_level_5")]
		SublocalityLevel5,
		/// <summary>
		/// The sublocality level4
		/// </summary>
		[EnumMember(Value = "sublocality_level_4")]
		SublocalityLevel4,
		/// <summary>
		/// The sublocality level3
		/// </summary>
		[EnumMember(Value = "sublocality_level_3")]
		SublocalityLevel3,
		/// <summary>
		/// The sublocality level2
		/// </summary>
		[EnumMember(Value = "sublocality_level_2")]
		SublocalityLevel2,
		/// <summary>
		/// The sublocality level1
		/// </summary>
		[EnumMember(Value = "sublocality_level_1")]
		SublocalityLevel1,
		/// <summary>
		/// The accounting
		/// </summary>
		[EnumMember(Value = "accounting")]
		Accounting,
		/// <summary>
		/// The amusement park
		/// </summary>
		[EnumMember(Value = "amusement_park")]
		AmusementPark,
		/// <summary>
		/// The aquarium
		/// </summary>
		[EnumMember(Value = "aquarium")]
		Aquarium,
		/// <summary>
		/// The art gallery
		/// </summary>
		[EnumMember(Value = "art_gallery")]
		ArtGallery,
		/// <summary>
		/// The atm
		/// </summary>
		[EnumMember(Value = "atm")]
		Atm,
		/// <summary>
		/// The bakery
		/// </summary>
		[EnumMember(Value = "bakery")]
		Bakery,
		/// <summary>
		/// The bank
		/// </summary>
		[EnumMember(Value = "bank")]
		Bank,
		/// <summary>
		/// The bar
		/// </summary>
		[EnumMember(Value = "bar")]
		Bar,
		/// <summary>
		/// The beauty salon
		/// </summary>
		[EnumMember(Value = "beauty_salon")]
		BeautySalon,
		/// <summary>
		/// The bicycle store
		/// </summary>
		[EnumMember(Value = "bicycle_store")]
		BicycleStore,
		/// <summary>
		/// The book store
		/// </summary>
		[EnumMember(Value = "book_store")]
		BookStore,
		/// <summary>
		/// The bowling alley
		/// </summary>
		[EnumMember(Value = "bowling_alley")]
		BowlingAlley,
		/// <summary>
		/// The cafe
		/// </summary>
		[EnumMember(Value = "cafe")]
		Cafe,
		/// <summary>
		/// The campground
		/// </summary>
		[EnumMember(Value = "campground")]
		Campground,
		/// <summary>
		/// The car dealer
		/// </summary>
		[EnumMember(Value = "car_dealer")]
		CarDealer,
		/// <summary>
		/// The car rental
		/// </summary>
		[EnumMember(Value = "car_rental")]
		CarRental,
		/// <summary>
		/// The car repair
		/// </summary>
		[EnumMember(Value = "car_repair")]
		CarRepair,
		/// <summary>
		/// The car wash
		/// </summary>
		[EnumMember(Value = "car_wash")]
		CarWash,
		/// <summary>
		/// The casino
		/// </summary>
		[EnumMember(Value = "casino")]
		Casino,
		/// <summary>
		/// The cemetery
		/// </summary>
		[EnumMember(Value = "cemetery")]
		Cemetery,
		/// <summary>
		/// The church
		/// </summary>
		[EnumMember(Value = "church")]
		Church,
		/// <summary>
		/// The city hall
		/// </summary>
		[EnumMember(Value = "city_hall")]
		CityHall,
		/// <summary>
		/// The clothing store
		/// </summary>
		[EnumMember(Value = "clothing_store")]
		ClothingStore,
		/// <summary>
		/// The convenience store
		/// </summary>
		[EnumMember(Value = "convenience_store")]
		ConvenienceStore,
		/// <summary>
		/// The courthouse
		/// </summary>
		[EnumMember(Value = "courthouse")]
		Courthouse,
		/// <summary>
		/// The dentist
		/// </summary>
		[EnumMember(Value = "dentist")]
		Dentist,
		/// <summary>
		/// The department store
		/// </summary>
		[EnumMember(Value = "department_store")]
		DepartmentStore,
		/// <summary>
		/// The doctor
		/// </summary>
		[EnumMember(Value = "doctor")]
		Doctor,
		/// <summary>
		/// The electrician
		/// </summary>
		[EnumMember(Value = "electrician")]
		Electrician,
		/// <summary>
		/// The electronics store
		/// </summary>
		[EnumMember(Value = "electronics_store")]
		ElectronicsStore,
		/// <summary>
		/// The embassy
		/// </summary>
		[EnumMember(Value = "embassy")]
		Embassy,
		/// <summary>
		/// The finance
		/// </summary>
		[EnumMember(Value = "finance")]
		Finance,
		/// <summary>
		/// The fire station
		/// </summary>
		[EnumMember(Value = "fire_station")]
		FireStation,
		/// <summary>
		/// The florist
		/// </summary>
		[EnumMember(Value = "florist")]
		Florist,
		/// <summary>
		/// The food
		/// </summary>
		[EnumMember(Value = "food")]
		Food,
		/// <summary>
		/// The funeral home
		/// </summary>
		[EnumMember(Value = "funeral_home")]
		FuneralHome,
		/// <summary>
		/// The furniture store
		/// </summary>
		[EnumMember(Value = "furniture_store")]
		FurnitureStore,
		/// <summary>
		/// The gas station
		/// </summary>
		[EnumMember(Value = "gas_station")]
		GasStation,
		/// <summary>
		/// The general contractor
		/// </summary>
		[EnumMember(Value = "general_contractor")]
		GeneralContractor,
		/// <summary>
		/// The grocery or supermarket
		/// </summary>
		[EnumMember(Value = "grocery_or_supermarket")]
		GroceryOrSupermarket,
		/// <summary>
		/// The gym
		/// </summary>
		[EnumMember(Value = "gym")]
		Gym,
		/// <summary>
		/// The hair care
		/// </summary>
		[EnumMember(Value = "hair_care")]
		HairCare,
		/// <summary>
		/// The hardware store
		/// </summary>
		[EnumMember(Value = "hardware_store")]
		HardwareStore,
		/// <summary>
		/// The health
		/// </summary>
		[EnumMember(Value = "health")]
		Health,
		/// <summary>
		/// The hindu temple
		/// </summary>
		[EnumMember(Value = "hindu_temple")]
		HinduTemple,
		/// <summary>
		/// The home goods store
		/// </summary>
		[EnumMember(Value = "home_goods_store")]
		HomeGoodsStore,
		/// <summary>
		/// The hospital
		/// </summary>
		[EnumMember(Value = "hospital")]
		Hospital,
		/// <summary>
		/// The insurance agency
		/// </summary>
		[EnumMember(Value = "insurance_agency")]
		InsuranceAgency,
		/// <summary>
		/// The jewelry store
		/// </summary>
		[EnumMember(Value = "jewelry_store")]
		JewelryStore,
		/// <summary>
		/// The laundry
		/// </summary>
		[EnumMember(Value = "laundry")]
		Laundry,
		/// <summary>
		/// The lawyer
		/// </summary>
		[EnumMember(Value = "lawyer")]
		Lawyer,
		/// <summary>
		/// The library
		/// </summary>
		[EnumMember(Value = "library")]
		Library,
		/// <summary>
		/// The light rail station
		/// </summary>
		[EnumMember(Value = "light_rail_station")]
		LightRailStation,
		/// <summary>
		/// The liquor store
		/// </summary>
		[EnumMember(Value = "liquor_store")]
		LiquorStore,
		/// <summary>
		/// The local government office
		/// </summary>
		[EnumMember(Value = "local_government_office")]
		LocalGovernmentOffice,
		/// <summary>
		/// The locksmith
		/// </summary>
		[EnumMember(Value = "locksmith")]
		Locksmith,
		/// <summary>
		/// The lodging
		/// </summary>
		[EnumMember(Value = "lodging")]
		Lodging,
		/// <summary>
		/// The meal delivery
		/// </summary>
		[EnumMember(Value = "meal_delivery")]
		MealDelivery,
		/// <summary>
		/// The meal takeaway
		/// </summary>
		[EnumMember(Value = "meal_takeaway")]
		MealTakeaway,
		/// <summary>
		/// The mosque
		/// </summary>
		[EnumMember(Value = "mosque")]
		Mosque,
		/// <summary>
		/// The movie rental
		/// </summary>
		[EnumMember(Value = "movie_rental")]
		MovieRental,
		/// <summary>
		/// The movie theater
		/// </summary>
		[EnumMember(Value = "movie_theater")]
		MovieTheater,
		/// <summary>
		/// The moving company
		/// </summary>
		[EnumMember(Value = "moving_company")]
		MovingCompany,
		/// <summary>
		/// The museum
		/// </summary>
		[EnumMember(Value = "museum")]
		Museum,
		/// <summary>
		/// The night club
		/// </summary>
		[EnumMember(Value = "night_club")]
		NightClub,
		/// <summary>
		/// The painter
		/// </summary>
		[EnumMember(Value = "painter")]
		Painter,
		/// <summary>
		/// The pet store
		/// </summary>
		[EnumMember(Value = "pet_store")]
		PetStore,
		/// <summary>
		/// The pharmacy
		/// </summary>
		[EnumMember(Value = "pharmacy")]
		Pharmacy,
		/// <summary>
		/// The physiotherapist
		/// </summary>
		[EnumMember(Value = "physiotherapist")]
		Physiotherapist,
		/// <summary>
		/// The place of worship
		/// </summary>
		[EnumMember(Value = "place_of_worship")]
		PlaceOfWorship,
		/// <summary>
		/// The plumber
		/// </summary>
		[EnumMember(Value = "plumber")]
		Plumber,
		/// <summary>
		/// The police
		/// </summary>
		[EnumMember(Value = "police")]
		Police,
		/// <summary>
		/// The post office
		/// </summary>
		[EnumMember(Value = "post_office")]
		PostOffice,
		/// <summary>
		/// The real estate agency
		/// </summary>
		[EnumMember(Value = "real_estate_agency")]
		RealEstateAgency,
		/// <summary>
		/// The restaurant
		/// </summary>
		[EnumMember(Value = "restaurant")]
		Restaurant,
		/// <summary>
		/// The roofing contractor
		/// </summary>
		[EnumMember(Value = "roofing_contractor")]
		RoofingContractor,
		/// <summary>
		/// The rv park
		/// </summary>
		[EnumMember(Value = "rv_park")]
		RvPark,
		/// <summary>
		/// The school
		/// </summary>
		[EnumMember(Value = "school")]
		School,
		/// <summary>
		/// The shoe store
		/// </summary>
		[EnumMember(Value = "shoe_store")]
		ShoeStore,
		/// <summary>
		/// The shopping mall
		/// </summary>
		[EnumMember(Value = "shopping_mall")]
		ShoppingMall,
		/// <summary>
		/// The spa
		/// </summary>
		[EnumMember(Value = "spa")]
		Spa,
		/// <summary>
		/// The stadium
		/// </summary>
		[EnumMember(Value = "stadium")]
		Stadium,
		/// <summary>
		/// The storage
		/// </summary>
		[EnumMember(Value = "storage")]
		Storage,
		/// <summary>
		/// The store
		/// </summary>
		[EnumMember(Value = "store")]
		Store,
		/// <summary>
		/// The subway station
		/// </summary>
		[EnumMember(Value = "subway_station")]
		SubwayStation,
		/// <summary>
		/// The synagogue
		/// </summary>
		[EnumMember(Value = "synagogue")]
		Synagogue,
		/// <summary>
		/// The taxi stand
		/// </summary>
		[EnumMember(Value = "taxi_stand")]
		TaxiStand,
		/// <summary>
		/// The travel agency
		/// </summary>
		[EnumMember(Value = "travel_agency")]
		TravelAgency,
		/// <summary>
		/// The university
		/// </summary>
		[EnumMember(Value = "university")]
		University,
		/// <summary>
		/// The veterinary care
		/// </summary>
		[EnumMember(Value = "veterinary_care")]
		VeterinaryCare,
		/// <summary>
		/// The zoo
		/// </summary>
		[EnumMember(Value = "zoo")]
		Zoo
	}
}