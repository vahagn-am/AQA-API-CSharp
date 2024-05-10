using System.Net;
using RestSharp;

namespace RestSharpTest.Arguments.Holders
{
    public class AuthValidationArgumentsHolderForDeletingCard
    {

            public IEnumerable<Parameter> AuthParams { get; set; }
            public string ErrorMessage { get; set; }
            public HttpStatusCode StatusCode { get; set; }

    }
}
