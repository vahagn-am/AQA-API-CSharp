using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers
{
    public class AuthValidationArgumentsProviderForCard : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            //yield return new AuthValidationArgumentsHolderForCard
            //{
            //    AuthParams = ArraySegment<Parameter>.Empty,
            //    ErrorMessage = "unauthorized card permission requested",
            //    StatusCode = HttpStatusCode.Unauthorized
            //};
            //yield return new AuthValidationArgumentsHolderForCard
            //{
            //    AuthParams = new[] { new Parameter("key", UrlParamValues.ValidKey, ParameterType.QueryString) },
            //    ErrorMessage = "unauthorized card permission requested",
            //    StatusCode = HttpStatusCode.Unauthorized
            //};
            yield return new AuthValidationArgumentsHolderForCard
            {
                AuthParams = new[] { new Parameter("token", UrlParamValues.ValidToken, ParameterType.QueryString) },
                ErrorMessage = "invalid app key",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
    }
}
