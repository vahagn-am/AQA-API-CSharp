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

        public void CheckCreateBoardWithInvalidName(IDictionary<string, object> arguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl).
            AddJsonBody(arguments);
            var response = _client.Post(request);

            Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
            Assert.That("{\"message\":\"invalid value for name\",\"error\":\"ERROR\"}", 
                Is.EqualTo( response.Content));
        }
        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]   
        public void CheckCreateBoardWithInvalidAuth(AuthValidationArgumentsHolder arguments)
        {
            var Boardname = "New Board";
            var request = RequestWithoutAuth(BoardsEndpoints.CreateBoardUrl)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddJsonBody(new Dictionary<string, string> { { "name", Boardname} });
            var response = _client.Post(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }



    }
}
