using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers
{
    public class AuthValidationArgumenstProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new AuthValidationArgumentsHolder
            {
                AuthParams = ArraySegment<Parameter>.Empty,
                ErrorMessage = "unauthorized permission requested",
                StatusCode = HttpStatusCode.Unauthorized
            };
            yield return new AuthValidationArgumentsHolder
            {
                AuthParams = new[] { Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString) },
                ErrorMessage = "unauthorized permission requested",
                StatusCode = HttpStatusCode.Unauthorized
            };
            yield return new AuthValidationArgumentsHolder
            {
                AuthParams = new[] { Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString) },
                ErrorMessage = "invalid app key",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
    }
}
