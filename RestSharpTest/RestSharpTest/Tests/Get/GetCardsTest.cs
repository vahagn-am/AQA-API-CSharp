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
        public void CheckGetCards()
        {
            var request = RequestWithAuth(CardEndpoints.GetAllCardsURL)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        public void CheckGetSpecificCard()
        {
            var request = RequestWithAuth(CardEndpoints.GetSingleCardUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.That("Test Card1", Is.EqualTo(responseContent.SelectToken("name").ToString()));
        }
    }
}
