// ***********************************************************************
// Assembly         : Invisionware.Net.WebUitls
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="UrlBuilder.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Invisionware.Collections;

namespace Invisionware.Net.WebUtils
{
	/// <summary>
	/// Class UrlBuilder.
	/// </summary>
	public class UrlBuilder : UriBuilder
	{
		#region Private Variables
		/// <summary>
		/// The _query string
		/// </summary>
		private IDictionary<string,string> _queryString = null;
		#endregion Private Variables

		#region Constructors
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [URL encode parameters].
		/// </summary>
		/// <value><c>true</c> if [URL encode parameters]; otherwise, <c>false</c>.</value>
		public bool UrlEncodeParameters { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [include empty parameters].
		/// </summary>
		/// <value><c>true</c> if [include empty parameters]; otherwise, <c>false</c>.</value>
		public bool IncludeEmptyParameters { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [URL encode path].
		/// </summary>
		/// <value><c>true</c> if [URL encode path]; otherwise, <c>false</c>.</value>
		public bool UrlEncodePath { get; set; }

		/// <summary>
		/// Gets the query string.
		/// </summary>
		/// <value>The query string.</value>
		public IDictionary<string, string> QueryString
		{
			get
			{
				if (_queryString == null && !string.IsNullOrEmpty(base.Query))
				{
					PopulateQueryString();					
				} 
				else if (_queryString == null)
				{
					_queryString = new Dictionary<string, string>();
				}
				return _queryString;
			}
		}

		/// <summary>
		/// Gets or sets the name of the page.
		/// </summary>
		/// <value>The name of the page.</value>
		public string PageName
		{
			get
			{
				var path = base.Path;
				return path.Substring(path.LastIndexOf("/") + 1);
			}
			set
			{
				var path = base.Path;
				path = path.Substring(0, path.LastIndexOf("/"));
				base.Path = string.Concat(path, "/", value);
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.Uri" /> instance constructed by the specified <see cref="T:System.UriBuilder" /> instance.
		/// </summary>
		/// <value>The URI.</value>
		public new Uri Uri
		{
			get
			{
				ToString();

				return base.Uri;
			}
		}
		#endregion

		#region Constructor overloads

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlBuilder" /> class.
		/// </summary>
		public UrlBuilder()
			: base()
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified URI.
		/// </summary>
		/// <param name="uri">A URI string.</param>
		public UrlBuilder(string uri)
			: base(uri)
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;

			PopulateQueryString();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified <see cref="T:System.Uri" /> instance.
		/// </summary>
		/// <param name="uri">An instance of the <see cref="T:System.Uri" /> class.</param>
		public UrlBuilder(Uri uri)
			: base(uri)
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;

			PopulateQueryString();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlBuilder"/> class.
		/// </summary>
		/// <param name="ub">The ub.</param>
		public UrlBuilder(UrlBuilder ub) : this(ub.Uri)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme and host.
		/// </summary>
		/// <param name="schemeName">An Internet access protocol.</param>
		/// <param name="hostName">A DNS-style domain name or IP address.</param>
		public UrlBuilder(string schemeName, string hostName)
			: base(schemeName, hostName)
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, and port.
		/// </summary>
		/// <param name="scheme">An Internet access protocol.</param>
		/// <param name="host">A DNS-style domain name or IP address.</param>
		/// <param name="portNumber">An IP port number for the service.</param>
		public UrlBuilder(string scheme, string host, int portNumber)
			: base(scheme, host, portNumber)
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, and path.
		/// </summary>
		/// <param name="scheme">An Internet access protocol.</param>
		/// <param name="host">A DNS-style domain name or IP address.</param>
		/// <param name="port">An IP port number for the service.</param>
		/// <param name="pathValue">The path to the Internet resource.</param>
		public UrlBuilder(string scheme, string host, int port, string pathValue)
			: base(scheme, host, port, pathValue)
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, path and query string or fragment identifier.
		/// </summary>
		/// <param name="scheme">An Internet access protocol.</param>
		/// <param name="host">A DNS-style domain name or IP address.</param>
		/// <param name="port">An IP port number for the service.</param>
		/// <param name="path">The path to the Internet resource.</param>
		/// <param name="extraValue">A query string or fragment identifier.</param>
		public UrlBuilder(string scheme, string host, int port, string path, string extraValue)
			: base(scheme, host, port, path, extraValue)
		{
			UrlEncodeParameters = false;
			IncludeEmptyParameters = false;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <param name="urlEncodeParams">if set to <c>true</c> [URL encode parameters].</param>
		/// <param name="includeEmptyValues">if set to <c>true</c> [include empty values].</param>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public string ToString(bool urlEncodeParams = false, bool includeEmptyValues = true)
		{
			GetQueryString(urlEncodeParams, includeEmptyValues);

			var result = base.Uri.AbsoluteUri;

			if (!UrlEncodePath)
			{
				result = WebUtility.UrlDecode(result);
			}

			return result;
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		public new string ToString()
		{
			return ToString(UrlEncodeParameters, IncludeEmptyParameters);
		}
		#endregion

		#region Private methods

		/// <summary>
		/// Populates the query string.
		/// </summary>
		private void PopulateQueryString()
		{
			string query = base.Query;

			if (string.IsNullOrEmpty(query))
			{
				return;
			}

			if (_queryString == null)
			{
				_queryString = new Dictionary<string, string>();
			}

			_queryString.Clear();

			query = query.Substring(1); //remove the ?

			var pairs = query.Split(new[] { '&' });
			foreach (var s in pairs)
			{
				var pair = s.Split(new[] { '=' });

				_queryString[pair[0]] = (pair.Length > 1) ? pair[1] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the query string.
		/// </summary>
		/// <param name="urlEncodeParams">if set to <c>true</c> [URL encode parameters].</param>
		/// <param name="includeEmptyValues">if set to <c>true</c> [include empty values].</param>
		private void GetQueryString(bool urlEncodeParams = false, bool includeEmptyValues = true)
		{
			base.Query = _queryString.ToQueryString(urlEncodeParams, includeEmptyValues);
		}

		#endregion
	}
}