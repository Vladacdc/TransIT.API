using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TransIT.BLL.Helpers
{
    /// <summary>
    /// Using this class to avoid duplication of code in services.
    /// The goal of this class is to split input search string into tokens.
    /// And it can be user as <see cref="IEnumerable{String}"/>
    /// </summary>
    /// <example> For example:
    /// <code>
    ///    foreach(var item in new SearchEntries(searchString))
    ///    {
    ///    }
    /// </code>
    /// </example>
    public class SearchEntries : IEnumerable<string>
    {
        private IEnumerable<string> _source;

        public SearchEntries(string input)
        {
            _source = input
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant());
        }

        public IEnumerator<string> GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_source).GetEnumerator();
    }
}
