using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update
{
    public class UpdateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
        public void CheckUpdatedBoardWithInvalidId(BoardIdValidationArgumetsHolder arguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl)
                .AddOrUpdateParameters(arguments.PathParams);
            var response = _client.Put(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]
        public void CheckUpdateBoardWithInvalidAuth(AuthValidationArgumentsHolder authValidationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl)
                .AddOrUpdateParameters(authValidationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedName" } });
            var response = _client.Put(request);
            Assert.That(authValidationArguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(authValidationArguments.ErrorMessage, Is.EqualTo(response.Content));

        }
        [Test]
        public void CheckUpdateBoardWithAnoherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedName" } });
            var response = _client.Put(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}
