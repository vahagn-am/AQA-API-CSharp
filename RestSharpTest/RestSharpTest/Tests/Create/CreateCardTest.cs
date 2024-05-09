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
        public void CheckCreateCard()
        {
            var CardName = "New Card" + DateTime.Now.ToShortDateString();
            var request = RequestWithAuth(CardEndpoints.CreateCardUrl)
                .AddJsonBody(new Dictionary<string, string> { 
                    { "idList", UrlParamValues.ExistingListId },
                    { "name", CardName } 
                });
            var response = _client.Post(request);
            var responseContent = JToken.Parse(response.Content);
            _CreatedCardId = responseContent.SelectToken("id").ToString();

            Assert.That(CardName, Is.EqualTo(responseContent.SelectToken("name").ToString()));
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));

            //Creating Get request to check if new Card exists
            request = RequestWithAuth(CardEndpoints.GetAllCardsURL)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            response = _client.Get(request);

            responseContent = JToken.Parse(response.Content);

            Assert.True(responseContent.Children().Select(token => 
                token.SelectToken("name").ToString()).Contains(CardName));

        }
        [TearDown]
        public void DeleteCreatedCard()
        {
            var request = RequestWithAuth(CardEndpoints.DeleteCardUrl)
                .AddUrlSegment("card_id", _CreatedCardId);
            var response = _client.Delete(request);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));

        }
    }
}
