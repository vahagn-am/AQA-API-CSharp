using RestSharp;
using RestSharpTest.Arguments.Holders;
using System.Collections;
using System.Net;

namespace RestSharpTest.Arguments.Providers
{
    public class CardIdValidationArgumentsProvider : IEnumerable

    {
        public IEnumerator GetEnumerator()
        {
            yield return new CardIdValidationArgumentsHolder()
            {
                ErrorMessage = "invalid id",
                PathParams = new[] { new Parameter("id", "invalid id", ParameterType.UrlSegment) },
                StatusCode = HttpStatusCode.BadRequest
            };

            yield return new CardIdValidationArgumentsHolder()
            {
                    ErrorMessage = "The requested resource was not found.",
                    PathParams = new [] {new Parameter("id", "6639ca0fc3ec23d350a126a0", ParameterType.UrlSegment)},
                    StatusCode = HttpStatusCode.NotFound
            };

            //yield return new Object[] -- Note: this ws initial code !!!
            //{
            //    new CardIdValidationArgumentsHolder()
            //    {
            //        ErrorMessage = "invalid id",
            //        PathParams = new [] {new Parameter("id", "invalid id", ParameterType.UrlSegment)},
            //        StatusCode = HttpStatusCode.BadRequest
            //    }

            //};
        }
    }
}
