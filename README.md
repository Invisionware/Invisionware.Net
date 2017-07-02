# Invisionware Framework
Invisionware Frmaework is a collection of utilities classes, extension methods, and new functionality to simplify creatig application in .NET. Amost all of the libraries are support on Dektop and Mobile (including Xamarin) development environments to provide the maxinum value possible.

## NET
This portion of the Invisionware Framework is focused on enhancements to standard Net libraries within the .NET frmaework

[![NuGet](https://img.shields.io/nuget/v/Invisionware.Net.svg)](https://www.nuget.org/packages/Invisionware.Net)

Packages related to Invisionware Net
```powershell
Install-Package Invisionware.Net
```

Then just add the following using statement
```c#
using Invisionware.Net.WebUtils;
```

### Dictionary Extensions
The following outline the extension methods provided for the IDictionary<,> class

#### string ToQueryString(this IDictionary<string, string> data, bool urlEncodeParams = false, bool includeEmptyValues = true)
Provides a simple way to rename an existing key in a dictionary

```c#
var dict = new Dictionary<string, string>();

dict["q"] = "text value";
dict["sort"] = "asc";
dict["page"] = "1";

var queryString = dict.ToQueryString();

Console.WriteLine(queryString);
// q=text%20value&sort=asc&page=1
```

### Object Extensions
This following outline the extension methods provided for the base object class

#### string ToQueryString<T>(this T obj, QueryStringParamOptions options = null)
Provides a simple why to take an object and convert it to a query string using the properties as the key names

```c#
public class MyObject 
{
   public string Q {get; set;}
   public string Sort {get;set;}
   public int Page {get;set;}
}

var obj = new MyObject { Q = "text value", Sort = "asc", Page = 1 };

var queryString = obj.ToQueryString();
Console.WriteLine(queryString);
// q=text%20value&sort=asc&page=1
```

### Cookie Extensions
This following outline the extension methods provided for the Cookie class

#### CookieCollection GetCookieCollection(this HttpResponseMessage response)
Provides a simple way to access the cookie collection associated with the HttpResponseMessage

```c#
var cookieContainer = new CookieContainer();

var clientHandler = new HttpClientHandler
{
	AllowAutoRedirect = true,
	CookieContainer = cookieContainer,
	MaxAutomaticRedirections = 5,
	UseCookies = true,
};

var client = new HttpClient(_clientHandler);

var response = await client.GetAsync("http://google.com");

var cc = response.GetCookieCollection();
```

#### Cookie GetCookie(this HttpResponseMessage response, string cookieName)
Provides a simple way to access a single cookie from the associated with the HttpResponseMessage

```c#
var cookieContainer = new CookieContainer();

var clientHandler = new HttpClientHandler
{
	AllowAutoRedirect = true,
	CookieContainer = cookieContainer,
	MaxAutomaticRedirections = 5,
	UseCookies = true,
};

var client = new HttpClient(_clientHandler);

var response = await client.GetAsync("http://google.com");

var cookie = response.GetCookie("NID");

Console.WriteLine(cookie.Value);
```

### HttpClientExtensions
This following outline the extension methods provided for the httpClient class

#### Task<Stream> GetFileAsync(this HttpClient client, Uri requestUri)
Provides a simple way to retrieve a FileStream from an HttpClient in one single async call.

Note: Will throw an exception is Http status code is not a success

```c#
var client = new HttpClient();
var stream = await client.GetFileAsync(new Uri("http://google.com");
```

#### Task<string> GetFileAsBase64Async(this HttpClient client, Uri requestUri)
Provides a simple way to retrieve a file as a base64 string from an HttpClient in one single async call.

Note: Will throw an exception is Http status code is not a success
```c#
var client = new HttpClient();
var str = await client.GetFileAsBase64Async(new Uri("http://google.com");
```

### UrlBuilder
This is a complete class designed to make creating and managing Urls easier (including working with QueryStrings). This class behaves identically to the Uri class with the added ability to get/set all properties on the URL and access the QueryString like a Dictionary<string,string> object

```c#
var ub = new UrlBuilder("http://google.com");
ub.QueryString["q"] = "search text";

var str = ub.ToString();

Console.WriteLine(str);
/// http://google.com?q=search%20text
```

### WebUtility
A set of light weight utility functions that allow for easy Html/Url encode/decode without requiring the need for System.Web

#### string HtmlEncode(string value)
Converts a string to an HTML-encoded string.

#### void HtmlEncode(string value, TextWriter output)
Converts a string into an HTML-encoded string, and writes the output to a TextWriter object.

#### static string HtmlDecode(string value)
Converts a string that has been HTML-encoded into a decoded string.

#### static void HtmlDecode(string value, TextWriter output)
Converts a string that has been HTML-encoded into a decoded string, and writes the output to a TextWriter object.

#### static string UrlEncode(string text)
UrlEncodes a string without the requirement for System.Web

#### static string GetUrlEncodedKey(string urlEncoded, string key)
UrlDecodes a string without requiring System.Web

---

## GeoCoding
This library provides a provider based framework for working with GeoCoding libraries.  Currently the only implementation is for support for Google GeoCoding 

Note: This library currently REQUIRES the use of XLabs.IOC framework for registring and handling the DI resolution of interface to implementation

```c#
var container = new XLabs.Ioc.Autofac.AutofacContainer(new Autofac.ContainerBuilder().Build());
Resolver.SetResolver(container.GetResolver());

container.Register<IDependencyContainer>(t => container);

provider = new GoogleGeoCoderProvider();

var container = Resolver.Resolve<IDependencyContainer>();
container.Register<IGeoCoderProvider>(t => provider);

Invisionware.Net.GeoCoding.ModelMapper.Map();

provider.Initialize(coderProvider =>
{
	coderProvider.APIKey = "<<YOUR GOOGLE API KEY>>";
});

var request = new GeoSearchRequest
				{
					Distance = 50,
					Name = "Wells Fargo Arena",
					//AddressTypes = new List<AddressTypes> { AddressTypes.Stadium, AddressTypes.Establishment, AddressTypes.Store } <- not working right now
				};

var result = await provider.SearchAsync(request);

```
