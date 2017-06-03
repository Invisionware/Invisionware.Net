// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------

namespace Invisionware.Net.WebUtils.Entities
{
	/// <summary>
	/// Struct SecurityToken
	/// </summary>
	internal struct Token
	{
		/// <summary>
		/// The start index
		/// </summary>
		public int StartIndex;
		/// <summary>
		/// The end index
		/// </summary>
		public int EndIndex;
		/// <summary>
		/// The text
		/// </summary>
		public string Text;
		/// <summary>
		/// The type
		/// </summary>
		public TokenType Type;
	}
}
