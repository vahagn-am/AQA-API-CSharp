using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create
{
    public class CreateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardNameValidationArgumentsProvider))]

        public async Task CheckCreateBoardWithInvalidName(IDictionary<string, object> arguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post).
            AddJsonBody(arguments);
            var response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);
            var errorMessage = responseContent.SelectToken("message").ToString();

            Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
            Assert.That("invalid value for name",Is.EqualTo(errorMessage));
        }
        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]   
        public async Task CheckCreateBoardWithInvalidAuth(AuthValidationArgumentsHolder arguments)
        {
            var Boardname = "New Board";
            var request = RequestWithoutAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddJsonBody(new Dictionary<string, string> { { "name", Boardname} });
            var response = await _client.ExecuteAsync(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }



    }
}
