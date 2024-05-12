using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete
{
    public class DeleteBoardTest : BaseTest
    {
        private string _createdBoardId;

        [SetUp]
        public async Task CreateBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> { { "name", "New_Board_Name" } });
            var response = await _client.ExecuteAsync(request);
            _createdBoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }
        [Test]
        public async Task CheckDeleteBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoradUrl, Method.Delete)
                .AddUrlSegment("id", _createdBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(string.Empty, Is.EqualTo(JToken.Parse(response.Content).SelectToken("_value").ToString()));

            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl, Method.Get)
                .AddUrlSegment("member", UrlParamValues.UserName);

            response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);

            Assert.False(responseContent.Children().Select(token =>
                token.SelectToken("id").ToString()).Contains(_createdBoardId));
        }
    }
}
