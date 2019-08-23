using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TransIT.BLL
{
    /// <summary>
    /// Using this class to avoid duplication of code in services.
    /// The goal of this class is to split input search string into tokens.
    /// And it can be used as <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <example> For example:
    /// <code>
    ///    foreach(var item in new SearchTokenCollection(searchString))
    ///    {
    ///    }
    /// </code>
    /// </example>
    public class SearchTokenCollection : IEnumerable<string>
    {
        private readonly IEnumerable<string> _source;

        public SearchTokenCollection(string input)
        {
            _source = input
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant());
        }

        public IEnumerator<string> GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_source).GetEnumerator();
    }
}