using RestSharp;
using System.Net;

namespace RestSharpTest.Arguments.Holders
{
    public class BoardIdValidationArgumetsHolder
    {
        public IEnumerable<Parameter> PathParams { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
