using RestSharp;
using System.Net;

namespace RestSharpTest
{

    public class GetBoardsValidationTest
    {
        private static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("http://api.trello.com");

        private IRestRequest RequestWithAuth(string url) =>
             new RestRequest(url)
                .AddQueryParameter("key", "73999fcb6a39c4d0d33207fcef1db3c7")
                .AddQueryParameter("token", "ATTA89041bea7ea15ffc07c0c913a3b3bf5101fb5141ac089e9214be7e1183c3db02C77514F5");

        [Test]
        public void CheckGetBoardWithInvalidId()
        {
            var request = RequestWithAuth("/1/boards/{id}").AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "invalidId");
            var response = _client.Get(request);
            Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
            Assert.That("invalid id", Is.EqualTo(response.Content));

        }
        [Test]
        public void CheckGetBoardWithInvalidAuth()
        {
            var request = new RestRequest("/1/boards/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "6639225e817a9e31b1a3f61a");

            var response = _client.Get(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("unauthorized permission requested", Is.EqualTo(response.Content));

        }
        [Test]
        public void CheckGetBoardWithAnotherUserCredentials()
        {
            var request = new RestRequest("/1/boards/{id}")
                .AddQueryParameter("key", "8b32218e6887516d17c84253faf967b6")
                .AddQueryParameter("token", "AOTA89041bea7ea15ffc07c0c913a3b3bf5101fb5141ac089e9214be7e1183c3db02C77514F5")
                .AddUrlSegment("id", "6639225e817a9e31b1a3f61a");
            var response = _client.Get(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid app token", Is.EqualTo(response.Content));
        }
    }
}
