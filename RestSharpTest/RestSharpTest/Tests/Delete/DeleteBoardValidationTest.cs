using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete
{
    public class DeleteBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]

        public async Task CheckDeleteBoardWithInvalidId(BoardIdValidationArgumetsHolder arguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoradUrl, Method.Delete)
                .AddOrUpdateParameters(arguments.PathParams);

            var response = await _client.ExecuteAsync(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        [Test]
        [TestCaseSource(typeof (AuthValidationArgumenstProvider))]
        public async Task CheckDeleteBoardWithoutAuth(AuthValidationArgumentsHolder argumets)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoradUrl, Method.Get)
                .AddOrUpdateParameters(argumets.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = await _client.ExecuteAsync(request);

            Assert.That(argumets.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(argumets.ErrorMessage, Is.EqualTo(response.Content));
        }
        [Test]
        public async Task CheckDeleteBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoradUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = await _client.ExecuteAsync(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }

    }
}
