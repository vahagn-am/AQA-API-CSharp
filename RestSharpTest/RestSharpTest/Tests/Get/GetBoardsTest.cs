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
        public async Task CheckGetBoards()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl, Method.Get)
                //.AddQueryParameter("field", "id,name")
                .AddUrlSegment("member", UrlParamValues.UserName);
            var response = await _client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);

            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));

            // To compare JSON's
            //var expectedJson = JToken.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/jsconfig1.json"));
            //Assert.That(responseContent, Is.EqualTo(expectedJson));
            
            

        }
        [Test]
        public async Task CheckGetBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);
            var response = await _client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
    }
}
