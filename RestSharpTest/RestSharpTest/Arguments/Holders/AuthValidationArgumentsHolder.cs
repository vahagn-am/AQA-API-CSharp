using RestSharp;
using System.Net;

namespace RestSharpTest.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
