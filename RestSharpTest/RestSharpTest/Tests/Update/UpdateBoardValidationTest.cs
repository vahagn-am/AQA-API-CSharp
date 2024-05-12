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
        public async Task CheckUpdatedBoardWithInvalidId(BoardIdValidationArgumetsHolder arguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddOrUpdateParameters(arguments.PathParams);
            var response = await _client.ExecuteAsync(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]
        public async Task CheckUpdateBoardWithInvalidAuth(AuthValidationArgumentsHolder authValidationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddOrUpdateParameters(authValidationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedName" } });
            var response = await _client.ExecuteAsync(request);
            Assert.That(authValidationArguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(authValidationArguments.ErrorMessage, Is.EqualTo(response.Content));

        }
        [Test]
        public async Task CheckUpdateBoardWithAnoherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedName" } });
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}
