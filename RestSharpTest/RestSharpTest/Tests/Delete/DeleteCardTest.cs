using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete
{
    public class DeleteCardTest : BaseTest
    {
        private string _createdCardId;

        [SetUp]
        public void CreateNewCard()
        {
            var request = RequestWithAuth(CardEndpoints.CreateCardUrl)
                .AddJsonBody(new Dictionary<string, string> {
                    { "idList", UrlParamValues.ExistingListId },
                    { "name", "Any Card Name" }
                });
            var response = _client.Post(request);
            _createdCardId = JToken.Parse(response.Content).SelectToken("id").ToString();

        }

        [Test]
        public void CheckDeleteCard()
        {
            var request = RequestWithAuth(CardEndpoints.DeleteCardUrl)
                .AddUrlSegment("id", _createdCardId);
            var response = _client.Delete(request);
            
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            JToken parsedToken = null;
            //Assert.That(parsedToken, Is.EqualTo(JToken.Parse(response.Content).SelectToken("_value")));
            Assert.That(JToken.Parse(response.Content).SelectToken("_value"), Is.Null);




            request = RequestWithAuth(CardEndpoints.GetAllCardsURL)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);

            response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);

            Assert.False(responseContent.Children().Select(token =>
                token.SelectToken("id").ToString()).Contains(_createdCardId));
        }

    }
}
