using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create
{
    public class CreateCardTest : BaseTest
    {
        private string _CreatedCardId;

        [Test]
        public async Task CheckCreateCard()
        {
            var CardName = "New Card" + DateTime.Now.ToShortDateString();
            var request = RequestWithAuth(CardEndpoints.CreateCardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> { 
                    { "idList", UrlParamValues.ExistingListId },
                    { "name", CardName } 
                });
            var response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);
            _CreatedCardId = responseContent.SelectToken("id").ToString();

            Assert.That(CardName, Is.EqualTo(responseContent.SelectToken("name").ToString()));
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));

            //Creating Get request to check if new Card exists
            request = RequestWithAuth(CardEndpoints.GetAllCardsURL, Method.Get)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            response = await _client.ExecuteAsync(request);

            responseContent = JToken.Parse(response.Content);

            Assert.True(responseContent.Children().Select(token => 
                token.SelectToken("name").ToString()).Contains(CardName));

        }
        [TearDown]
        public async Task DeleteCreatedCard()
        {
            var request = RequestWithAuth(CardEndpoints.DeleteCardUrl, Method.Delete)
                .AddUrlSegment("id", _CreatedCardId);
            var response = await _client.ExecuteAsync(request);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));

        }
    }
}
