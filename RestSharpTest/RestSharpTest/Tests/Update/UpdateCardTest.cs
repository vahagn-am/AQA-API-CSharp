using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update
{
    public class UpdateCardTest : BaseTest
    {
        [Test]
        public async Task CheckUpdateCard()
        {
            var updatedCardName = "New Name" + DateTime.Now.ToLongDateString();
            var request = RequestWithAuth(CardEndpoints.UpdateCardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", updatedCardName } });
            var response = await _client.ExecuteAsync(request);

            var responseContent = JToken.Parse(response.Content);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(updatedCardName, Is.EqualTo(responseContent.SelectToken("name").ToString()));

            
            
            request = RequestWithAuth(CardEndpoints.GetSingleCardUrl, Method.Get)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate);
            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);
            Assert.That(updatedCardName, Is.EqualTo(responseContent.SelectToken("name").ToString()));

        }
    }
}
