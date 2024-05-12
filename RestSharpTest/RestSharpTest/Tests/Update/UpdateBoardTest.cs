using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update
{
    public class UpdateBoardTest : BaseTest
    {
        [Test]
        public async Task CheckUpdateBoard()
        {
            var updatedName = "Updated name" + DateTime.Now.ToShortDateString();
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", updatedName} });
            var response = await _client.ExecuteAsync(request);

            var responseContent = JToken.Parse(response.Content);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(updatedName, Is.EqualTo(responseContent.SelectToken("name").ToString()));

            request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate);
            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);

            Assert.That(updatedName, Is.EqualTo(responseContent.SelectToken("name").ToString()));

        } 
    }
}
