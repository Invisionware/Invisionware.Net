// ***********************************************************************
// Assembly         : Invisionware.Net.WebUitls
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="WebUtility.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Globalization;
using System.IO;
using Invisionware.Net.WebUtils.Entities;

namespace Invisionware.Net
{
	/// <summary>
	/// Provides methods for encoding and decoding HTML when processing Web requests.
	/// </summary>
	public static class WebUtility
	{
		/// <summary>
		/// Converts a string to an HTML-encoded string.
		/// </summary>
		/// <param name="value">The string to encode.</param>
		/// <returns>An encoded string.</returns>
		public static string HtmlEncode(string value)
		{
			if (string.IsNullOrEmpty(value)) return value;

			using (var writer = new StringWriter(CultureInfo.InvariantCulture))
			{
				HtmlEncode(value, writer);

				return writer.ToString();
			}
		}

		/// <summary>
		/// Converts a string into an HTML-encoded string, and writes the output to a <see cref="TextWriter" /> object.
		/// </summary>
		/// <param name="value">The string to encode.</param>
		/// <param name="output">A <see cref="TextWriter" /> output stream.</param>
		/// <exception cref="System.ArgumentNullException">output</exception>
		/// <exception cref="ArgumentNullException"><paramref name="value" /> is not <see langword="null" /> and <paramref name="output" /> is <see langword="null" />.</exception>
		public static void HtmlEncode(string value, TextWriter output)
		{
			// Very weird behavior that we don't throw on a null value, but 
			// do on empty, however, this mimics the platform implementation
			if (value == null) return;

			if (output == null) throw new ArgumentNullException(nameof(output));

			HtmlEncodingServices.Encode(value, output);
		}

		/// <summary>
		/// Converts a string that has been HTML-encoded into a decoded string.
		/// </summary>
		/// <param name="value">The string to decode.</param>
		/// <returns>The decoded string.</returns>
		public static string HtmlDecode(string value)
		{
			if (string.IsNullOrEmpty(value)) return value;

			using (var writer = new StringWriter(CultureInfo.InvariantCulture))
			{
				HtmlDecode(value, writer);

				return writer.ToString();
			}
		}

		/// <summary>
		/// Converts a string that has been HTML-encoded into a decoded string, and writes the output to a <see cref="TextWriter" /> object.
		/// </summary>
		/// <param name="value">The string to decode.</param>
		/// <param name="output">A <see cref="TextWriter" /> output stream.</param>
		/// <exception cref="System.ArgumentNullException">output</exception>
		/// <exception cref="ArgumentNullException"><paramref name="value" /> is not <see langword="null" /> and <paramref name="output" /> is <see langword="null" />.</exception>
		public static void HtmlDecode(string value, TextWriter output)
		{
			// Very weird behavior that we don't throw on a null value, but 
			// do on empty, however, this mimics the platform implementation
			if (value == null) return;

			if (output == null) throw new ArgumentNullException(nameof(output));

			HtmlEncodingServices.Decode(value, output);
		}

		/// <summary>
		/// UrlEncodes a string without the requirement for System.Web
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>System.String.</returns>
		// [Obsolete("Use System.Uri.EscapeDataString instead")]
		public static string UrlEncode(string text)
		{
			return System.Net.WebUtility.UrlEncode(text);

			// Sytem.Uri provides reliable parsing
			//return Uri.EscapeDataString(text);
		}

		/// <summary>
		/// UrlDecodes a string without requiring System.Web
		/// </summary>
		/// <param name="text">String to decode.</param>
		/// <returns>decoded string</returns>
		public static string UrlDecode(string text)
		{
			// pre-process for + sign space formatting since System.Uri doesn't handle it
			// plus literals are encoded as %2b normally so this should be safe
			text = text.Replace("+", " ");
			return Uri.UnescapeDataString(text);
		}

		/// <summary>
		/// Retrieves a value by key from a UrlEncoded string.
		/// </summary>
		/// <param name="urlEncoded">UrlEncoded String</param>
		/// <param name="key">Key to retrieve value for</param>
		/// <returns>returns the value or "" if the key is not found or the value is blank</returns>
		public static string GetUrlEncodedKey(string urlEncoded, string key)
		{
			urlEncoded = "&" + urlEncoded + "&";

			var index = urlEncoded.IndexOf("&" + key + "=", StringComparison.OrdinalIgnoreCase);
			if (index < 0)
				return "";

			var lnStart = index + 2 + key.Length;

			var index2 = urlEncoded.IndexOf("&", lnStart, StringComparison.Ordinal);
			if (index2 < 0)
				return "";

			return UrlDecode(urlEncoded.Substring(lnStart, index2 - lnStart));
		}
	}
}