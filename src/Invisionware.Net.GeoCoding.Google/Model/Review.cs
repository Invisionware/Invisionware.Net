// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : shawn.anderson
// Created          : 06-28-2015
//
// Last Modified By : shawn.anderson
// Last Modified On : 06-28-2015
// ***********************************************************************
// <copyright file="Review.cs" company="Invisionware">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Invisionware.Net.GeoCoding.Google.Model
{
	/// <summary>
	/// Class Review.
	/// </summary>
	[DebuggerDisplay("Author Name = {AuthorName}, Rating = {Rating}, Time = {Time}")]
	internal class Review
	{
		/// <summary>
		/// Gets or sets the aspects.
		/// </summary>
		/// <value>The aspects.</value>
		[JsonProperty("aspects")]
		public IList<Aspect> Aspects { get; set; }
		/// <summary>
		/// Gets or sets the name of the author.
		/// </summary>
		/// <value>The name of the author.</value>
		[JsonProperty("author_name")]
		public string AuthorName { get; set; }
		/// <summary>
		/// Gets or sets the author URL.
		/// </summary>
		/// <value>The author URL.</value>
		[JsonProperty("author_url")]
		public string AuthorUrl { get; set; }
		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		/// <value>The language.</value>
		[JsonProperty("language")]
		public string Language { get; set; }
		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		[JsonProperty("rating")]
		public int Rating { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		[JsonProperty("text")]
		public string Text { get; set; }
		/// <summary>
		/// Gets or sets the time.
		/// </summary>
		/// <value>The time.</value>
		[JsonProperty("time")]
		public object Time { get; set; }
	}
}