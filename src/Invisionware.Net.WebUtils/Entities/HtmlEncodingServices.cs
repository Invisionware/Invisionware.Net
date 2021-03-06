﻿// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace Invisionware.Net.WebUtils.Entities
{
	/// <summary>
	/// Class HtmlEncodingServices.
	/// </summary>
	internal static class HtmlEncodingServices
	{
		/// <summary>
		/// The character to entity map
		/// </summary>
		private static readonly Dictionary<char, HtmlEntity> CharToEntityMap = new Dictionary<char, HtmlEntity>();
		/// <summary>
		/// The entity to character map
		/// </summary>
		private static readonly Dictionary<string, char> EntityToCharMap = new Dictionary<string, char>();
		/// <summary>
		/// The entity start character
		/// </summary>
		internal const char EntityStartChar = '&';
		/// <summary>
		/// The entity end character
		/// </summary>
		internal const char EntityEndChar = ';';
		/// <summary>
		/// The numeric entity start character
		/// </summary>
		internal const char NumericEntityStartChar = '#';
		/// <summary>
		/// The hexadecimal entity start char1
		/// </summary>
		internal const char HexEntityStartChar1 = 'x';
		/// <summary>
		/// The hexadecimal entity start char2
		/// </summary>
		internal const char HexEntityStartChar2 = 'X';

		/// <summary>
		/// Encodes the specified HTML.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="writer">The writer.</param>
		public static void Encode(string html, TextWriter writer)
		{
			var encoder = new HtmlEntityEncoder(CharToEntityMap);
			encoder.Encode(html, writer);
		}

		/// <summary>
		/// Decodes the specified HTML.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="writer">The writer.</param>
		public static void Decode(string html, TextWriter writer)
		{
			var parser = new HtmlEntityDecoder(html, EntityToCharMap);
			parser.Decode(writer);
		}

		/// <summary>
		/// Initializes static members of the <see cref="HtmlEncodingServices"/> class.
		/// </summary>
		static HtmlEncodingServices()
		{
			// Note: To mimic the platform's implementation of WebUtility, we don't HTML encode every character;
			// only the default ones, and ASCII characters between 160 and 255 inclusive. We also encode certain 
			// characters using their hex value, and not their named value to also mimic the platforms implementation.
			RegisterEntity('&',      "amp",     encode:true, writeAsDecimal:false);
			RegisterEntity('<',      "lt",      encode:true, writeAsDecimal:false);
			RegisterEntity('>',      "gt",      encode:true, writeAsDecimal:false);
			RegisterEntity('"',      "quot",    encode:true, writeAsDecimal:false);
			RegisterEntity('\'',     "apos",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a0', "nbsp",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a1', "iexcl",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a2', "cent",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a3', "pound",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a4', "curren",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a5', "yen",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a6', "brvbar",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a7', "sect",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a8', "uml",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00a9', "copy",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00aa', "ordf",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ab', "laquo",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ac', "not",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ad', "shy",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ae', "reg",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00af', "macr",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b0', "deg",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b1', "plusmn",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b2', "sup2",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b3', "sup3",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b4', "acute",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b5', "micro",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b6', "para",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b7', "middot",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b8', "cedil",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00b9', "sup1",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ba', "ordm",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00bb', "raquo",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00bc', "frac14",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00bd', "frac12",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00be', "frac34",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00bf', "iquest",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c0', "Agrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c1', "Aacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c2', "Acirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c3', "Atilde",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c4', "Auml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c5', "Aring",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c6', "AElig",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c7', "Ccedil",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c8', "Egrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00c9', "Eacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ca', "Ecirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00cb', "Euml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00cc', "Igrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00cd', "Iacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ce', "Icirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00cf', "Iuml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d0', "ETH",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d1', "Ntilde",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d2', "Ograve",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d3', "Oacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d4', "Ocirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d5', "Otilde",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d6', "Ouml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d7', "times",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d8', "Oslash",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00d9', "Ugrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00da', "Uacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00db', "Ucirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00dc', "Uuml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00dd', "Yacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00de', "THORN",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00df', "szlig",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e0', "agrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e1', "aacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e2', "acirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e3', "atilde",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e4', "auml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e5', "aring",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e6', "aelig",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e7', "ccedil",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e8', "egrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00e9', "eacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ea', "ecirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00eb', "euml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ec', "igrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ed', "iacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ee', "icirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ef', "iuml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f0', "eth",     encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f1', "ntilde",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f2', "ograve",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f3', "oacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f4', "ocirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f5', "otilde",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f6', "ouml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f7', "divide",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f8', "oslash",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00f9', "ugrave",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00fa', "uacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00fb', "ucirc",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00fc', "uuml",    encode:true, writeAsDecimal:true);
			RegisterEntity('\x00fd', "yacute",  encode:true, writeAsDecimal:true);
			RegisterEntity('\x00fe', "thorn",   encode:true, writeAsDecimal:true);
			RegisterEntity('\x00ff', "yuml",    encode:true, writeAsDecimal:true);
			RegisterEntity('Œ',      "OElig",   encode:false, writeAsDecimal:false);
			RegisterEntity('œ',      "oelig",   encode:false, writeAsDecimal:false);
			RegisterEntity('Š',      "Scaron",  encode:false, writeAsDecimal:false);
			RegisterEntity('š',      "scaron",  encode:false, writeAsDecimal:false);
			RegisterEntity('Ÿ',      "Yuml",    encode:false, writeAsDecimal:false);
			RegisterEntity('ƒ',      "fnof",    encode:false, writeAsDecimal:false);
			RegisterEntity('ˆ',      "circ",    encode:false, writeAsDecimal:false);
			RegisterEntity('˜',      "tilde",   encode:false, writeAsDecimal:false);
			RegisterEntity('Α',      "Alpha",   encode:false, writeAsDecimal:false);
			RegisterEntity('Β',      "Beta",    encode:false, writeAsDecimal:false);
			RegisterEntity('Γ',      "Gamma",   encode:false, writeAsDecimal:false);
			RegisterEntity('Δ',      "Delta",   encode:false, writeAsDecimal:false);
			RegisterEntity('Ε',      "Epsilon", encode:false, writeAsDecimal:false);
			RegisterEntity('Ζ',      "Zeta",    encode:false, writeAsDecimal:false);
			RegisterEntity('Η',      "Eta",     encode:false, writeAsDecimal:false);
			RegisterEntity('Θ',      "Theta",   encode:false, writeAsDecimal:false);
			RegisterEntity('Ι',      "Iota",    encode:false, writeAsDecimal:false);
			RegisterEntity('Κ',      "Kappa",   encode:false, writeAsDecimal:false);
			RegisterEntity('Λ',      "Lambda",  encode:false, writeAsDecimal:false);
			RegisterEntity('Μ',      "Mu",      encode:false, writeAsDecimal:false);
			RegisterEntity('Ν',      "Nu",      encode:false, writeAsDecimal:false);
			RegisterEntity('Ξ',      "Xi",      encode:false, writeAsDecimal:false);
			RegisterEntity('Ο',      "Omicron", encode:false, writeAsDecimal:false);
			RegisterEntity('Π',      "Pi",      encode:false, writeAsDecimal:false);
			RegisterEntity('Ρ',      "Rho",     encode:false, writeAsDecimal:false);
			RegisterEntity('Σ',      "Sigma",   encode:false, writeAsDecimal:false);
			RegisterEntity('Τ',      "Tau",     encode:false, writeAsDecimal:false);
			RegisterEntity('Υ',      "Upsilon", encode:false, writeAsDecimal:false);
			RegisterEntity('Φ',      "Phi",     encode:false, writeAsDecimal:false);
			RegisterEntity('Χ',      "Chi",     encode:false, writeAsDecimal:false);
			RegisterEntity('Ψ',      "Psi",     encode:false, writeAsDecimal:false);
			RegisterEntity('Ω',      "Omega",   encode:false, writeAsDecimal:false);
			RegisterEntity('α',      "alpha",   encode:false, writeAsDecimal:false);
			RegisterEntity('β',      "beta",    encode:false, writeAsDecimal:false);
			RegisterEntity('γ',      "gamma",   encode:false, writeAsDecimal:false);
			RegisterEntity('δ',      "delta",   encode:false, writeAsDecimal:false);
			RegisterEntity('ε',      "epsilon", encode:false, writeAsDecimal:false);
			RegisterEntity('ζ',      "zeta",    encode:false, writeAsDecimal:false);
			RegisterEntity('η',      "eta",     encode:false, writeAsDecimal:false);
			RegisterEntity('θ',      "theta",   encode:false, writeAsDecimal:false);
			RegisterEntity('ι',      "iota",    encode:false, writeAsDecimal:false);
			RegisterEntity('κ',      "kappa",   encode:false, writeAsDecimal:false);
			RegisterEntity('λ',      "lambda",  encode:false, writeAsDecimal:false);
			RegisterEntity('μ',      "mu",      encode:false, writeAsDecimal:false);
			RegisterEntity('ν',      "nu",      encode:false, writeAsDecimal:false);
			RegisterEntity('ξ',      "xi",      encode:false, writeAsDecimal:false);
			RegisterEntity('ο',      "omicron", encode:false, writeAsDecimal:false);
			RegisterEntity('π',      "pi",      encode:false, writeAsDecimal:false);
			RegisterEntity('ρ',      "rho",     encode:false, writeAsDecimal:false);
			RegisterEntity('ς',      "sigmaf",  encode:false, writeAsDecimal:false);
			RegisterEntity('σ',      "sigma",   encode:false, writeAsDecimal:false);
			RegisterEntity('τ',      "tau",     encode:false, writeAsDecimal:false);
			RegisterEntity('υ',      "upsilon", encode:false, writeAsDecimal:false);
			RegisterEntity('φ',      "phi",     encode:false, writeAsDecimal:false);
			RegisterEntity('χ',      "chi",     encode:false, writeAsDecimal:false);
			RegisterEntity('ψ',      "psi",     encode:false, writeAsDecimal:false);
			RegisterEntity('ω',      "omega",   encode:false, writeAsDecimal:false);
			RegisterEntity('ϑ',      "thetasym",encode:false, writeAsDecimal:false);
			RegisterEntity('ϒ',      "upsih",   encode:false, writeAsDecimal:false);
			RegisterEntity('ϖ',      "piv",     encode:false, writeAsDecimal:false);
			RegisterEntity(' ',      "ensp",    encode:false, writeAsDecimal:false);
			RegisterEntity(' ',      "emsp",    encode:false, writeAsDecimal:false);
			RegisterEntity(' ',      "thinsp",  encode:false, writeAsDecimal:false);
			RegisterEntity('‌',       "zwnj",    encode:false, writeAsDecimal:false);
			RegisterEntity('‍',       "zwj",     encode:false, writeAsDecimal:false);
			RegisterEntity('‎',       "lrm",     encode:false, writeAsDecimal:false);
			RegisterEntity('‏',       "rlm",     encode:false, writeAsDecimal:false);
			RegisterEntity('–',      "ndash",   encode:false, writeAsDecimal:false);
			RegisterEntity('—',      "mdash",   encode:false, writeAsDecimal:false);
			RegisterEntity('‘',      "lsquo",   encode:false, writeAsDecimal:false);
			RegisterEntity('’',      "rsquo",   encode:false, writeAsDecimal:false);
			RegisterEntity('‚',      "sbquo",   encode:false, writeAsDecimal:false);
			RegisterEntity('“',      "ldquo",   encode:false, writeAsDecimal:false);
			RegisterEntity('”',      "rdquo",   encode:false, writeAsDecimal:false);
			RegisterEntity('„',      "bdquo",   encode:false, writeAsDecimal:false);
			RegisterEntity('†',      "dagger",  encode:false, writeAsDecimal:false);
			RegisterEntity('‡',      "Dagger",  encode:false, writeAsDecimal:false);
			RegisterEntity('•',      "bull",    encode:false, writeAsDecimal:false);
			RegisterEntity('…',      "hellip",  encode:false, writeAsDecimal:false);
			RegisterEntity('‰',      "permil",  encode:false, writeAsDecimal:false);
			RegisterEntity('′',      "prime",   encode:false, writeAsDecimal:false);
			RegisterEntity('″',      "Prime",   encode:false, writeAsDecimal:false);
			RegisterEntity('‹',      "lsaquo",  encode:false, writeAsDecimal:false);
			RegisterEntity('›',      "rsaquo",  encode:false, writeAsDecimal:false);
			RegisterEntity('‾',      "oline",   encode:false, writeAsDecimal:false);
			RegisterEntity('⁄',      "frasl",   encode:false, writeAsDecimal:false);
			RegisterEntity('€',      "euro",    encode:false, writeAsDecimal:false);
			RegisterEntity('ℑ',      "image",   encode:false, writeAsDecimal:false);
			RegisterEntity('℘',      "weierp",  encode:false, writeAsDecimal:false);
			RegisterEntity('ℜ',      "real",    encode:false, writeAsDecimal:false);
			RegisterEntity('™',      "trade",   encode:false, writeAsDecimal:false);
			RegisterEntity('ℵ',     "alefsym",  encode:false, writeAsDecimal:false);
			RegisterEntity('←',      "larr",    encode:false, writeAsDecimal:false);
			RegisterEntity('↑',      "uarr",    encode:false, writeAsDecimal:false);
			RegisterEntity('→',      "rarr",    encode:false, writeAsDecimal:false);
			RegisterEntity('↓',      "darr",    encode:false, writeAsDecimal:false);
			RegisterEntity('↔',      "harr",    encode:false, writeAsDecimal:false);
			RegisterEntity('↵',      "crarr",   encode:false, writeAsDecimal:false);
			RegisterEntity('⇐',      "lArr",   encode:false, writeAsDecimal:false);
			RegisterEntity('⇑',      "uArr",    encode:false, writeAsDecimal:false);
			RegisterEntity('⇒',      "rArr",   encode:false, writeAsDecimal:false);
			RegisterEntity('⇓',      "dArr",    encode:false, writeAsDecimal:false);
			RegisterEntity('⇔',      "hArr",   encode:false, writeAsDecimal:false);
			RegisterEntity('∀',      "forall", encode:false, writeAsDecimal:false);
			RegisterEntity('∂',      "part",    encode:false, writeAsDecimal:false);
			RegisterEntity('∃',      "exist",  encode:false, writeAsDecimal:false);
			RegisterEntity('∅',      "empty",   encode:false, writeAsDecimal:false);
			RegisterEntity('∇',      "nabla",   encode:false, writeAsDecimal:false);
			RegisterEntity('∈',      "isin",    encode:false, writeAsDecimal:false);
			RegisterEntity('∉',      "notin",   encode:false, writeAsDecimal:false);
			RegisterEntity('∋',      "ni",      encode:false, writeAsDecimal:false);
			RegisterEntity('∏',      "prod",    encode:false, writeAsDecimal:false);
			RegisterEntity('∑',      "sum",     encode:false, writeAsDecimal:false);
			RegisterEntity('−',      "minus",   encode:false, writeAsDecimal:false);
			RegisterEntity('∗',      "lowast",  encode:false, writeAsDecimal:false);
			RegisterEntity('√',      "radic",   encode:false, writeAsDecimal:false);
			RegisterEntity('∝',      "prop",   encode:false, writeAsDecimal:false);
			RegisterEntity('∞',      "infin",   encode:false, writeAsDecimal:false);
			RegisterEntity('∠',      "ang",     encode:false, writeAsDecimal:false);
			RegisterEntity('∧',      "and",     encode:false, writeAsDecimal:false);
			RegisterEntity('∨',      "or",      encode:false, writeAsDecimal:false);
			RegisterEntity('∩',      "cap",     encode:false, writeAsDecimal:false);
			RegisterEntity('∪',      "cup",     encode:false, writeAsDecimal:false);
			RegisterEntity('∫',      "int",     encode:false, writeAsDecimal:false);
			RegisterEntity('∴',      "there4", encode:false, writeAsDecimal:false);
			RegisterEntity('∼',      "sim",     encode:false, writeAsDecimal:false);
			RegisterEntity('≅',      "cong",    encode:false, writeAsDecimal:false);
			RegisterEntity('≈',      "asymp",   encode:false, writeAsDecimal:false);
			RegisterEntity('≠',      "ne",      encode:false, writeAsDecimal:false);
			RegisterEntity('≡',      "equiv",   encode:false, writeAsDecimal:false);
			RegisterEntity('≤',      "le",      encode:false, writeAsDecimal:false);
			RegisterEntity('≥',      "ge",      encode:false, writeAsDecimal:false);
			RegisterEntity('⊂',      "sub",     encode:false, writeAsDecimal:false);
			RegisterEntity('⊃',      "sup",     encode:false, writeAsDecimal:false);
			RegisterEntity('⊄',      "nsub",    encode:false, writeAsDecimal:false);
			RegisterEntity('⊆',      "sube",    encode:false, writeAsDecimal:false);
			RegisterEntity('⊇',      "supe",    encode:false, writeAsDecimal:false);
			RegisterEntity('⊕',      "oplus",   encode:false, writeAsDecimal:false);
			RegisterEntity('⊗',      "otimes",  encode:false, writeAsDecimal:false);
			RegisterEntity('⊥',      "perp",    encode:false, writeAsDecimal:false);
			RegisterEntity('⋅',      "sdot",    encode:false, writeAsDecimal:false);
			RegisterEntity('⌈',      "lceil",  encode:false, writeAsDecimal:false);
			RegisterEntity('⌉',      "rceil",  encode:false, writeAsDecimal:false);
			RegisterEntity('⌊',      "lfloor", encode:false, writeAsDecimal:false);
			RegisterEntity('⌋',      "rfloor", encode:false, writeAsDecimal:false);
			RegisterEntity('〈',      "lang",    encode:false, writeAsDecimal:false);
			RegisterEntity('〉',      "rang",    encode:false, writeAsDecimal:false);
			RegisterEntity('◊',      "loz",    encode:false, writeAsDecimal:false);
			RegisterEntity('♠',      "spades", encode:false, writeAsDecimal:false);
			RegisterEntity('♣',      "clubs",  encode:false, writeAsDecimal:false);
			RegisterEntity('♥',      "hearts", encode:false, writeAsDecimal:false);
			RegisterEntity('♦',      "diams",  encode:false, writeAsDecimal:false);
		}

		/// <summary>
		/// Registers the entity.
		/// </summary>
		/// <param name="c">The c.</param>
		/// <param name="entity">The entity.</param>
		/// <param name="encode">if set to <c>true</c> [encode].</param>
		/// <param name="writeAsDecimal">if set to <c>true</c> [write as decimal].</param>
		private static void RegisterEntity(char c, string entity, bool encode, bool writeAsDecimal)
		{
			if (encode)
				CharToEntityMap.Add(c, new HtmlEntity() { Entity = entity, WriteAsDecimal = writeAsDecimal });

			EntityToCharMap.Add(entity, c);
		}
	}
}
