using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get
{

    public class GetCardsTest : BaseTest
    {
        [Test]
        public async Task CheckGetCards()
        {
            var request = RequestWithAuth(CardEndpoints.GetAllCardsURL, Method.Get)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            var response = await _client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        public async Task CheckGetSpecificCard()
        {
            var request = RequestWithAuth(CardEndpoints.GetSingleCardUrl, Method.Get)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = await _client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.That("Test Card1", Is.EqualTo(responseContent.SelectToken("name").ToString()));
        }
    }
}
