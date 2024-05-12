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


        protected RestRequest RequestWithAuth(string url, Method method) =>
             RequestWithoutAuth(url, method)
            .AddOrUpdateParameters(UrlParamValues.AuthQueryParams);


        protected RestRequest RequestWithoutAuth(string url, Method method) =>
                new RestRequest(url, method);

        [OneTimeTearDown]
        protected static void DisposeRestClient() =>
            _client?.Dispose();
    }
}
