using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests
{
    public class BaseTest
    {
        protected static IRestClient _client;

        [OneTimeSetUp]
        protected static void InitializeRestClient() =>
            _client = new RestClient("http://api.trello.com");


        protected IRestRequest RequestWithAuth(string url) =>
             RequestWithoutAuth(url)
            .AddOrUpdateParameters(UrlParamValues.AuthQueryParams);


        protected IRestRequest RequestWithoutAuth(string url) =>
                new RestRequest(url);
    }
}
