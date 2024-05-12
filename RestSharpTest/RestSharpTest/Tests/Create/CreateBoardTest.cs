using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;
using System.Threading.Tasks;


namespace RestSharpTest.Tests.Create
{
    public class CreateBoardTest : BaseTest
    {
        private string _createBoardId;

        [Test]
        public async Task CheckCreateBoard()
        {
            var BoardName = "New Board" + DateTime.Now.ToLongDateString();

            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> { { "name",  BoardName } });
            var response = await _client.ExecuteAsync(request);

            var responseContent = JToken.Parse(response.Content);
            _createBoardId = responseContent.SelectToken("id").ToString();

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(BoardName,Is.EqualTo(responseContent.SelectToken("name").ToString()));


            // Creting Get Request and checking if new Board exists
            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl, Method.Get)
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("member", UrlParamValues.UserName);

            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);

            Assert.That(responseContent.Children().Select(token =>
                token.SelectToken("name").ToString()).Contains(BoardName));
        }

        [TearDown]
        public async Task DeleteCreatedBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoradUrl, Method.Delete)
                .AddUrlSegment("id", _createBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.OK,Is.EqualTo( response.StatusCode));
        }
    }
}
