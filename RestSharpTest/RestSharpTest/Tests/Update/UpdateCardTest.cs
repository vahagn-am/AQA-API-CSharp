using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update
{
    public class UpdateCardTest : BaseTest
    {
        [Test]
        public void CheckUpdateCard()
        {
            var updatedCardName = "New Name" + DateTime.Now.ToLongDateString();
            var request = RequestWithAuth(CardEndpoints.UpdateCardUrl)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", updatedCardName } });

            var response = _client.Put(request);
            var responseContent = JToken.Parse(response.Content);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(updatedCardName, Is.EqualTo(responseContent.SelectToken("name").ToString()));

            
            
            request = RequestWithAuth(CardEndpoints.GetSingleCardUrl)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate);
            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);
            Assert.That(updatedCardName, Is.EqualTo(responseContent.SelectToken("name").ToString()));

        }
    }
}
