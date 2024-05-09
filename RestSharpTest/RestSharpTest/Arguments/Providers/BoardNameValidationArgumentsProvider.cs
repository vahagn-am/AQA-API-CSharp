using System.Collections;
using System.Collections.Immutable;

namespace RestSharpTest.Arguments.Providers
{
    public class BoardNameValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new Dictionary<string, object> { { "name", 12345} }; 
            yield return ImmutableDictionary<string, object>.Empty;
        }
    }
}
