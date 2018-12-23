// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Invisionware.Net.WebUtils.Entities
{
	/// <summary>
	/// Class HtmlEntityEncoder.
	/// </summary>
	internal class HtmlEntityEncoder
	{
		/// <summary>
		/// The _char to entity map
		/// </summary>
		private readonly IDictionary<char, HtmlEntity> _charToEntityMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlEntityEncoder"/> class.
		/// </summary>
		/// <param name="charToEntityMap">The character to entity map.</param>
		public HtmlEntityEncoder(IDictionary<char, HtmlEntity> charToEntityMap)
		{
			Debug.Assert(charToEntityMap != null);

			_charToEntityMap = charToEntityMap;
		}

		/// <summary>
		/// Encodes the specified HTML.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="writer">The writer.</param>
		public void Encode(string html, TextWriter writer)
		{
			Debug.Assert(html != null);
			Debug.Assert(writer != null);

			foreach (char c in html)
			{
                if (_charToEntityMap.TryGetValue(c, out HtmlEntity entity))
                {
                    WriteEntity(c, entity, writer);
                }
                else
                {
                    WriteChar(c, writer);
                }
            }
		}

		/// <summary>
		/// Writes the character.
		/// </summary>
		/// <param name="c">The c.</param>
		/// <param name="writer">The writer.</param>
		private static void WriteChar(char c, TextWriter writer)
		{
			writer.Write(c);
		}

		/// <summary>
		/// Writes the entity.
		/// </summary>
		/// <param name="c">The c.</param>
		/// <param name="entity">The entity.</param>
		/// <param name="writer">The writer.</param>
		private static void WriteEntity(char c, HtmlEntity entity, TextWriter writer)
		{
			writer.Write(HtmlEncodingServices.EntityStartChar);

			if (entity.WriteAsDecimal)
			{
				WriteDecimalEntity(c, writer);
			}
			else
			{
				WriteTextEntity(entity, writer);
			}

			writer.Write(HtmlEncodingServices.EntityEndChar);
		}

		/// <summary>
		/// Writes the decimal entity.
		/// </summary>
		/// <param name="c">The c.</param>
		/// <param name="writer">The writer.</param>
		private static void WriteDecimalEntity(char c, TextWriter writer)
		{
			writer.Write(HtmlEncodingServices.NumericEntityStartChar);
			writer.Write(((int)c).ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Writes the text entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="writer">The writer.</param>
		private static void WriteTextEntity(HtmlEntity entity, TextWriter writer)
		{
			writer.Write(entity.Entity);
		}
	}
}
