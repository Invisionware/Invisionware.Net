// ***********************************************************************
// Assembly         : Invisionware.Net.WebUitls
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="JSONHelpers.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Invisionware.Net.WebUtils
{
	/// <summary>
	/// Class JSONHelpers.
	/// </summary>
	internal static class JSONHelpers
	{
		/// <summary>
		/// Gets the property names.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>IEnumerable&lt;System.String&gt;.</returns>
		internal static IEnumerable<string> GetPropertyNames(string data)
		{
			return GetPropertyNames(ParseData(data));
		}

		/// <summary>
		/// Gets the property names.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>IEnumerable&lt;System.String&gt;.</returns>
		internal static IEnumerable<string> GetPropertyNames(JObject data)
		{
			var list = new List<string>();

			foreach (var k in data)
			{
				list.Add(k.Key);
			}

			return list;
		}

		/// <summary>
		/// Parses the data into a JSON object.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>JObject.</returns>
		internal static JObject ParseData(string data)
		{
			return JObject.Parse(data);
		}
	}
}
