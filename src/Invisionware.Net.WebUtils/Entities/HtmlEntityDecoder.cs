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
	/// Class HtmlEntityDecoder.
	/// </summary>
	internal class HtmlEntityDecoder
	{
		/// <summary>
		/// The _HTML
		/// </summary>
		private readonly string _html;
		/// <summary>
		/// The _entity to character map
		/// </summary>
		private readonly IDictionary<string, char> _entityToCharMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlEntityDecoder"/> class.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="entityToCharMap">The entity to character map.</param>
		public HtmlEntityDecoder(string html, IDictionary<string, char> entityToCharMap)
		{
			Debug.Assert(html != null);
			Debug.Assert(entityToCharMap != null);

			_html = html;
			_entityToCharMap = entityToCharMap;
		}

		/// <summary>
		/// Decodes the specified writer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public void Decode(TextWriter writer)
		{
			var tokenizer = new HtmlEntityTokenizer(_html);

			Token? token;
			while ((token = tokenizer.Next()) != null)
			{
				WriteToken(token.Value, writer);
			}
		}

		/// <summary>
		/// Writes the token.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="writer">The writer.</param>
		private void WriteToken(Token token, TextWriter writer)
		{
			switch (token.Type)
			{
				case TokenType.Content:
					WriteContent(token.Text, writer);
					break;

				case TokenType.TextEntity:
					WriteTextEntity(token, writer);
					break;

				case TokenType.DecimalEntity:
					WriteDecimalEntity(token, writer);
					break;

				case TokenType.HexEntity:
					WriteHexEntity(token, writer);
					break;

			}
		}

		/// <summary>
		/// Writes the content.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="writer">The writer.</param>
		private static void WriteContent(string text, TextWriter writer)
		{
			writer.Write(text, writer);
		}

		/// <summary>
		/// Writes the text entity.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="writer">The writer.</param>
		private void WriteTextEntity(Token token, TextWriter writer)
		{
            if (_entityToCharMap.TryGetValue(token.Text, out char c))
            {   // We recognize the entity
                writer.Write(c);
            }
            else
            {
                // Do not recognize, treat it as content
                WriteAsContext(token, writer);
            }
        }

		/// <summary>
		/// Writes the decimal entity.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="writer">The writer.</param>
		private void WriteDecimalEntity(Token token, TextWriter writer)
		{
			WriteNumericEntity(token, writer, NumberStyles.Integer);
		}

		/// <summary>
		/// Writes the hexadecimal entity.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="writer">The writer.</param>
		private void WriteHexEntity(Token token, TextWriter writer)
		{
			WriteNumericEntity(token, writer, NumberStyles.AllowHexSpecifier);
		}

		/// <summary>
		/// Writes the numeric entity.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="writer">The writer.</param>
		/// <param name="styles">The styles.</param>
		private void WriteNumericEntity(Token token, TextWriter writer, NumberStyles styles)
		{
            if (ushort.TryParse(token.Text, styles, CultureInfo.InvariantCulture, out ushort value))
            {
                writer.Write((char)value);
            }
            else
            {   // Failed to parse, write it as content
                WriteAsContext(token, writer);
            }
        }

		/// <summary>
		/// Writes as context.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="writer">The writer.</param>
		private void WriteAsContext(Token token, TextWriter writer)
		{
			WriteContent(_html.Substring(token.StartIndex, token.EndIndex - token.StartIndex), writer);
		}
	}
}
