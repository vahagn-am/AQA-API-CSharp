using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System.Net;

namespace RestSharpTest
{

    public class GetBoardsTest
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
        public void CheckGetBoards()
        {
            var request = RequestWithAuth("/1/members/{member}/boards")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("member", "vahagnqa");
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth("/1/boards/{id}")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", "6639225e817a9e31b1a3f61a");
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCards()
        {
            var request = RequestWithAuth("/1/lists/{list_id}/cards")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", "6639225e817a9e31b1a3f621");
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        public void CheckGetCard()
        {
            var request = RequestWithAuth("/1/cards/{id}")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", "6639ca0fc3ec23d350a126a9");
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.That("Test Card1", Is.EqualTo(responseContent.SelectToken("name").ToString()));
        }
    }
}
