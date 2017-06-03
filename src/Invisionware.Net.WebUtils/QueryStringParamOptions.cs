using Invisionware.Serialization;
using System;

namespace Invisionware.Net.WebUtils
{
	/// <summary>
	/// Settings object for serializing the object to a QueryString
	/// </summary>
	/// <seealso cref="DictionarySerializeOptions" />
	public class QueryStringParamOptions : DictionarySerializeOptions
    {
        /// <summary>
        /// Gets or sets the name value seperator.
        /// </summary>
        /// <value>The name value seperator.</value>
        public string NameValueSeperator { get; set; } = "=";

        /// <summary>
        /// Gets or sets the query parm join callbackfunction.
        /// </summary>
        /// <value>The query parm join function.</value>
        /// <example>
        /// QueryParmJoinFunc = (key, keyValue) { if (key == "someParamName") return "or=" + keyValue; else return keyValue;
        /// </example>
        public Func<string, string, string> QueryParmJoinFunc { get; set; } = (key, keyValue) => keyValue;

    }
}