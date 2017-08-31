// ***********************************************************************
// Assembly         : Invisionware.Net.WebUitls
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Cookies.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Invisionware.Net
{
	/// <summary>
	/// Class Cookies.
	/// </summary>
	public static class Cookies
	{
		/// <summary>
		/// Gets the cookie collection.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns>CookieCollection.</returns>
		public static CookieCollection GetCookieCollection(this HttpResponseMessage response)
		{
			try
			{
				var cookieHeader = response.Headers.GetValues("Set-Cookie").ToList();
				var cookieCollection = ParseCookieHeader(cookieHeader, response.RequestMessage.RequestUri.AbsoluteUri);

				return cookieCollection;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the cookie.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <param name="cookieName">Name of the cookie.</param>
		/// <returns>Cookie.</returns>
		public static Cookie GetCookie(this HttpResponseMessage response, string cookieName)
		{
			var cc = response.GetCookieCollection();

			try
			{
				return cc[cookieName];
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the cookies.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns>CookieContainer.</returns>
		public static CookieContainer GetCookieContainer(this HttpResponseMessage response)
		{
			var pageUri = response.RequestMessage.RequestUri;

			var cookieContainer = new CookieContainer();
			IEnumerable<string> cookies;
			if (!response.Headers.TryGetValues("set-cookie", out cookies)) return cookieContainer;

			foreach (var c in cookies)
			{
				cookieContainer.SetCookies(pageUri, c);
			}

			return cookieContainer;
		}

		/// <summary>
		/// Parses the cookie header.
		/// </summary>
		/// <param name="al">The al.</param>
		/// <param name="strHost">The string host.</param>
		/// <returns>CookieCollection.</returns>
		private static CookieCollection ParseCookieHeader(IList<string> al, string strHost)
		{
			var cc = new CookieCollection();

			var alcount = al.Count;

			for (var i = 0; i < alcount; i++)
			{
				var strEachCook = al[i];
				var strEachCookParts = strEachCook.Split(';');
				var intEachCookPartsCount = strEachCookParts.Length;
				var cookTemp = new Cookie();

				for (var j = 0; j < intEachCookPartsCount; j++)
				{
					if (j == 0)
					{
						var strCNameAndCValue = strEachCookParts[j];
						if (strCNameAndCValue != string.Empty)
						{
							var firstEqual = strCNameAndCValue.IndexOf("=");
							var firstName = strCNameAndCValue.Substring(0, firstEqual);
							var allValue = strCNameAndCValue.Substring(firstEqual + 1, strCNameAndCValue.Length - (firstEqual + 1));
							cookTemp.Name = firstName;
							cookTemp.Value = allValue;
						}
						continue;
					}
					string strPNameAndPValue;
					string[] nameValuePairTemp;
					if (strEachCookParts[j].IndexOf("path", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						strPNameAndPValue = strEachCookParts[j];
						if (strPNameAndPValue != string.Empty)
						{
							nameValuePairTemp = strPNameAndPValue.Split('=');
							cookTemp.Path = nameValuePairTemp[1] != string.Empty ? nameValuePairTemp[1] : "/";
						}
						continue;
					}

					if (strEachCookParts[j].IndexOf("domain", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						strPNameAndPValue = strEachCookParts[j];
						if (strPNameAndPValue != string.Empty)
						{
							nameValuePairTemp = strPNameAndPValue.Split('=');

							cookTemp.Domain = nameValuePairTemp[1] != string.Empty ? nameValuePairTemp[1] : strHost;
						}
					}
				}

				if (cookTemp.Path == string.Empty)
				{
					cookTemp.Path = "/";
				}
				if (cookTemp.Domain == string.Empty)
				{
					cookTemp.Domain = strHost;
				}
				cc.Add(cookTemp);
			}
			return cc;
		}
	}
}