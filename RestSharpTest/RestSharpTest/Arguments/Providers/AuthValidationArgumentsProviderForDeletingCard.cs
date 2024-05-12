using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers
{
    public class AuthValidationArgumentsProviderForDeletingCard : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new AuthValidationArgumentsHolderForDeletingCard
            {
                AuthParams = ArraySegment<Parameter>.Empty,
                ErrorMessage = "invalid id",
                StatusCode = HttpStatusCode.BadRequest
            };
            yield return new AuthValidationArgumentsHolderForDeletingCard
            {
                AuthParams = new[] { Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString) },
                ErrorMessage = "invalid id",
                StatusCode = HttpStatusCode.BadRequest
            };
            yield return new AuthValidationArgumentsHolderForDeletingCard
            {
                AuthParams = new[] { Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString) },
                ErrorMessage = "invalid app key",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
    }
}
