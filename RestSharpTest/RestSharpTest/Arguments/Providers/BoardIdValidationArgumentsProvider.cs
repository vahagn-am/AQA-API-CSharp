using RestSharp;
using RestSharpTest.Arguments.Holders;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers
{
    public class BoardIdValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new BoardIdValidationArgumetsHolder()
                {
                    ErrorMessage = "invalid id",
                    StatusCode = HttpStatusCode.BadRequest,
                    PathParams = new [] { Parameter.CreateParameter("id", "invalid", ParameterType.UrlSegment)}
                }
            };

            yield return new object[]
            {
                new BoardIdValidationArgumetsHolder()
                {
                    ErrorMessage = "The requested resource was not found.",
                    StatusCode = HttpStatusCode.NotFound,
                    PathParams = new [] {Parameter.CreateParameter("id", "6639225e817a9e31b1a3f611", ParameterType.UrlSegment) }
                }
            };
        }
    }
}
