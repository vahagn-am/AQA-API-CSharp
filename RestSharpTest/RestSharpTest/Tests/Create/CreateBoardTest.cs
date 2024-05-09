using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create
{
    public class CreateBoardTest : BaseTest
    {
        private string _createBoardId;

        [Test]
        public void CheckCreateBoard()
        {
            var BoardName = "New Board" + DateTime.Now.ToLongDateString();

            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> { { "name",  BoardName } });
            var response = _client.Post(request);

            var responseContent = JToken.Parse(response.Content);
            _createBoardId = responseContent.SelectToken("id").ToString();

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(BoardName,Is.EqualTo(responseContent.SelectToken("name").ToString()));


            // Creting Get Request and checking if new Board exists
            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("member", UrlParamValues.UserName);

            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);

            Assert.That(responseContent.Children().Select(token =>
                token.SelectToken("name").ToString()).Contains(BoardName));
        }

        [TearDown]
        public void DeleteCreatedBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoradUrl)
                .AddUrlSegment("id", _createBoardId);
            var response = _client.Delete(request);
            Assert.That(HttpStatusCode.OK,Is.EqualTo( response.StatusCode));
        }
    }
}
