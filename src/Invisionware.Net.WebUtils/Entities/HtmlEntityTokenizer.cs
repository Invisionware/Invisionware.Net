// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Text;

namespace Invisionware.Net.WebUtils.Entities
{
	// Breaks up a HTML string into content and entity tokens.
	//
	// HTML Entity Grammer
	// 
	// html:
	//    (content | entity)[0..N]
	//      
	// content:
	//    char (char[1..N except entity-start-char)
	//
	// entity:
	//    entity-start-char (text-entity | numeric-entity) entity-end-char
	//
	// entity-start-char
	//    &
	//
	// entity-end-char
	//    ;
	//
	// text-entity:
	//    char[1..N] except entity-end-char
	//
	// numeric-entity:
	//    numeric-entity-start-char (hex-entity | decimal-entity)
	//
	// numeric-entity-start-char:
	//    #
	//    
	// hex-entity:
	//    hex-entity-start-char hex-digit[1..N]
	//
	// hex-entity-start-char:
	//    x | X 
	//
	// decimal-entity:
	//    decimal-digit[1..N]
	//
	// hex-digit:
	//    0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | A | B | C | D | E | F | a | b | c | d | e | f
	//
	// decimal-digit:
	//    0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9
	//
	// char:
	//    any unicode character
	//
	/// <summary>
	/// Class HtmlEntityTokenizer.
	/// </summary>
	internal class HtmlEntityTokenizer
	{
		/// <summary>
		/// The _HTML
		/// </summary>
		private readonly string _html;
		/// <summary>
		/// The _text
		/// </summary>
		private readonly StringBuilder _text = new StringBuilder();
		/// <summary>
		/// The _index
		/// </summary>
		private int _index = -1;
		/// <summary>
		/// The _type
		/// </summary>
		private TokenType _type;

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlEntityTokenizer"/> class.
		/// </summary>
		/// <param name="html">The HTML.</param>
		public HtmlEntityTokenizer(string html)
		{
			Debug.Assert(html != null);

			_html = html;
		}

		/// <summary>
		/// Nexts this instance.
		/// </summary>
		/// <returns>System.Nullable&lt;SecurityToken&gt;.</returns>
		public Token? Next()
		{
			_text.Clear();

			int startIndex = _index;

			if (!ParseNext())
				return null;

			var token = new Token
							{
								Text = this._text.ToString(),
								StartIndex = startIndex + 1,
								EndIndex = this._index + 1,
								Type = this._type
							};

			return token;
		}

		/// <summary>
		/// Parses the next.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool ParseNext()
		{
			if (TryParseEntity())
			{
				return true;
			}

			if (TryParseContent())
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Tries the content of the parse.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool TryParseContent()
		{
			// Content can start with a '&' because it means that 
			// TryParseEntity determined that it wasn't an entity
			if (!this.Read())
			{
				return false;
			}

			this.ReadWhile(c => c != HtmlEncodingServices.EntityStartChar);
			this._type = TokenType.Content;

			return true;
		}

		/// <summary>
		/// Tries the parse entity.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool TryParseEntity()
		{
			if (Peek() != HtmlEncodingServices.EntityStartChar)
				return false;

			int? entityEndIndex = FindEndEntity();
			if (entityEndIndex == null)
				return false;       // Not an entity, doesn't end with ';'

			// Skip over '&'
			Skip();

			if (!TryParseNumericEntity())
			{
				ParseTextEntity();
			}

			// Skip over ';'
			Skip();
			return true;
		}

		/// <summary>
		/// Parses the text entity.
		/// </summary>
		private void ParseTextEntity()
		{
			ReadWhile(c => c != HtmlEncodingServices.EntityEndChar);
			_type = TokenType.TextEntity;
		}

		/// <summary>
		/// Reads the entity body.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool ReadEntityBody()
		{
			return ReadWhile(c => !IsEntityEnding(c));
		}

		/// <summary>
		/// Tries the parse numeric entity.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool TryParseNumericEntity()
		{
			if (SkipIf('#'))
			{
				if (TryParseHexEntity())
					return true;

				ParseDecimalEntity();
				return true;
			}
			
			return false;
		}

		/// <summary>
		/// Tries the parse hexadecimal entity.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool TryParseHexEntity()
		{
			if (SkipIf(HtmlEncodingServices.HexEntityStartChar1) || SkipIf(HtmlEncodingServices.HexEntityStartChar2))
			{
				ReadEntityBody();
				_type = TokenType.HexEntity;
				return true;
			}

			return false; // Not a hex-entity
		}

		/// <summary>
		/// Parses the decimal entity.
		/// </summary>
		private void ParseDecimalEntity()
		{
			ReadEntityBody();
			_type = TokenType.DecimalEntity;
		}

		/// <summary>
		/// Finds the end entity.
		/// </summary>
		/// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
		private int? FindEndEntity()
		{
			int lookAhead = 2;
			while (true)
			{
				char? c = Peek(lookAhead);
				if (c == null)
					return null;

				// Avoid '&quot&'
				if (c == HtmlEncodingServices.EntityStartChar)
					return null;

				if (c == HtmlEncodingServices.EntityEndChar)
				{
					// Avoid an empty entity such as '&;'
					if (lookAhead == 2)
						return null;

					return _index + lookAhead;
				}

				lookAhead++;
			}
		}

		/// <summary>
		/// Reads the while.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool ReadWhile(Func<char, bool> predicate)
		{
			char? c = Peek();
			if (c == null || !predicate(c.Value))
				return false;

			do
			{
				Read();

				c = Peek();
			} while (c != null && predicate(c.Value));

			return true;
		}

		/// <summary>
		/// Peeks the specified look ahead.
		/// </summary>
		/// <param name="lookAhead">The look ahead.</param>
		/// <returns>System.Nullable&lt;System.Char&gt;.</returns>
		private char? Peek(int lookAhead = 1)
		{
			int index = _index + lookAhead;

			if (index >= _html.Length)
				return null;

			return _html[index];
		}

		/// <summary>
		/// Reads this instance.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool Read()
		{
			char? c = Peek();

			if (c == null)
				return false;

			_text.Append(c.Value);

			Skip();
			return true;
		}

		/// <summary>
		/// Skips if.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool SkipIf(char symbol)
		{
			if (Peek() != symbol)
				return false;

			Skip();
			return true;
		}

		/// <summary>
		/// Skips this instance.
		/// </summary>
		private void Skip()
		{
			_index++;
		}

		/// <summary>
		/// Determines whether [is entity ending] [the specified c].
		/// </summary>
		/// <param name="c">The c.</param>
		/// <returns><c>true</c> if [is entity ending] [the specified c]; otherwise, <c>false</c>.</returns>
		private static bool IsEntityEnding(char c)
		{
			return c == HtmlEncodingServices.EntityEndChar || c == HtmlEncodingServices.EntityStartChar;
		}
	}
}
