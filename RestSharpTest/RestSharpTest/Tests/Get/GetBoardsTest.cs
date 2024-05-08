using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get
{

    public class GetBoardsTest : BaseTest
    {


        [Test]
        public void CheckGetBoards()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("member", UrlParamValues.UserName);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
    }
}
