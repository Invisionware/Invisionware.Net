using System;
using Invisionware.Serialization.JsonNET;

namespace Invisionware.Net.WebUtils
{
	/// <summary>
	/// Settings object for serializing the object to a QueryString
	/// </summary>
	/// <seealso cref="DictionarySerializeOptions" />
	public class QueryStringParamOptions : DictionarySerializeOptions
    {
        /// <summary>
        /// Gets or sets the name value separator.
        /// </summary>
        /// <value>The name value separator.</value>
        public string NameValueSepartor { get; set; } = "=";

        /// <summary>
        /// Gets or sets the query param join callbackfunction.
        /// </summary>
        /// <value>The query param join function.</value>
        /// <example>
        /// QueryParmJoinFunc = (key, keyValue) { if (key == "someParamName") return "or=" + keyValue; else return keyValue;
        /// </example>
        public Func<string, string, string> QueryParamJoinFunc { get; set; } = (key, keyValue) => keyValue;

    }
}