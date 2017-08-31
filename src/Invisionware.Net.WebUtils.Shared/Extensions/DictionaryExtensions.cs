// ***********************************************************************
// Assembly         : Invisionware.Net.WebUitls
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="DictionaryExtensions.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Invisionware.Collections
{
	/// <summary>
	/// Class Extensions.
	/// </summary>
	public static class DictionaryExtensions
	{

		/// <summary>
		/// To the query string.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="urlEncodeParams">if set to <c>true</c> [URL encode parameters].</param>
		/// <param name="includeEmptyValues">if set to <c>true</c> [include empty values].</param>
		/// <returns>System.String.</returns>
		public static string ToQueryString(this IDictionary<string, string> data, bool urlEncodeParams = false, bool includeEmptyValues = true)
		{
			if (data == null || data.Count == 0) return string.Empty;

			//var keys = new string[count];
			//var values = new string[count];
			var pairs = new List<string>();

			//data.Keys.CopyTo(keys, 0);
			//data.Values.CopyTo(values, 0);
			
			//for (var i = 0; i < count; i++)
			//{
			//	pairs[i] = string.Concat(keys[i], "=", urlEncodeParams ? WebUtility.UrlEncode(values[i]) : values[i]);
			//}

			foreach (var qp in data)
			{
				if (string.IsNullOrEmpty(qp.Value))
				{
					if (includeEmptyValues) pairs.Add(qp.Key);
				}
				else
				{
					pairs.Add(string.Concat(qp.Key, "=", urlEncodeParams ? System.Net.WebUtility.UrlEncode(qp.Value) : qp.Value));
				}
			}

			var result = string.Join("&", pairs);

			return result;
		}
	}
}