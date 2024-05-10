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
        public void CreateBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> { { "name", "New_Board_Name" } });
            var response = _client.Post(request);
            _createdBoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }
        [Test]
        public void CheckDeleteBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoradUrl)
                .AddUrlSegment("id", _createdBoardId);
            var response = _client.Delete(request);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(string.Empty, Is.EqualTo(JToken.Parse(response.Content).SelectToken("_value").ToString()));

            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddUrlSegment("member", UrlParamValues.UserName);

            response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);

            Assert.False(responseContent.Children().Select(token =>
                token.SelectToken("id").ToString()).Contains(_createdBoardId));
        }
    }
}
