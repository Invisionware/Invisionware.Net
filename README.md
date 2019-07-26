# Invisionware Framework

Invisionware Frmaework is a collection of utilities classes, extension methods, and new functionality to simplify creatig application in .NET. Amost all of the libraries are support on Dektop and Mobile (including Xamarin) development environments to provide the maxinum value possible.

## NET
This portion of the Invisionware Framework provides extensions and wrappers around a number of aspects related to Network and Internet type operations 

### GeoCoding
This package provides an easy to use wrapper around a number of GeoCoding services as well as utility methods for working with Geo Coordinates.  Currently support exists for Google

###### Usage

```
// Setup IoC
var container = new Invisionware.Ioc.Autofac.AutofacContainer(new Autofac.ContainerBuilder().Build());
Resolver.SetResolver(container.GetResolver());

container.Register<IDependencyContainer>(t => container);

// Setup for IoC mappings
Invisionware.Net.GeoCoding.ModelMapper.Map();

// Create the provider 
_provider = new GoogleGeoCoderProvider();
container.Register<IGeoCoderProvider>(t => _provider);

_provider.Initialize(coderProvider =>
{
	coderProvider.APIKey = "<Your Key>";
});

// Make the request
var request = new GeoSearchRequest
		{
			Distance = distance,
			Name = name,
			Address = geoAddress
		};

var result = await _provider.SearchAsync(request);

```

#### Utilities

**Geo Location Distance Calculations**
```
GeoLocation loc1 = new GeoLocation();
GeoLocation loc2 = new GeoLocation();

var distanceInMiles = loc1.DistanceBetween(loc2);
```

### RestEase
This package offers a number of extensions and utility methods for the [RestEase](https://github.com/canton7/RestEase) library

#### Factories
These "factories" make it easier to create a RestEase client with support for things link OAuth, logging, custom HttpClient, etc.

##### RestEastClientFractory
This class provides a wrapper for accessing RestEase interfaces and simplifies the following types of requirements
- Enable Logging for requests/responses using Serilog
- Customizing the JsonSerializer and JsonBodySerializer Settings objects
- Defining a custom Response Deserializer (like the HybridResponseDeserializer in this library)

###### Usage
```
```

#### Serializers
These serializers extend the RestEase client to handle a number of different types of request/response format

##### HybridResponseDeserializer
This class projects a custom response deserializer that looks at the content type of the response and selects either the standard Xml Deserializer or the Json Deserializer

###### Usage
```
var client = RestEase.RestClient("http://someurl.com")
		{
			ResponseDeserializer = new RestEaseHybridResponseDeserializer()
		}.For<ICustomApi>();
```

##### JsonNetRequestBodySerializer

###### Usage
```
var client = RestEase.RestClient("http://someurl.com")
		{
			RequestBodySerializer = new JsonRequestBodySerializer()
		}.For<ICustomApi>();
```

##### XmlRequestBodySerializer

###### Usage
```
var client = RestEase.RestClient("http://someurl.com")
		{
			RequestBodySerializer = new XmlRequestBodySerializer()
		}.For<ICustomApi>();
```

