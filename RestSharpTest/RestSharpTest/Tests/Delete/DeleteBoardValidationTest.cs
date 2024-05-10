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

        public void CheckDeleteBoardWithInvalidId(BoardIdValidationArgumetsHolder arguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoradUrl)
                .AddOrUpdateParameters(arguments.PathParams);

            var response = _client.Delete(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        [Test]
        [TestCaseSource(typeof (AuthValidationArgumenstProvider))]
        public void CheckDeleteBoardWithoutAuth(AuthValidationArgumentsHolder argumets)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoradUrl)
                .AddOrUpdateParameters(argumets.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = _client.Get(request);

            Assert.That(argumets.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(argumets.ErrorMessage, Is.EqualTo(response.Content));
        }
        [Test]
        public void CheckDeleteBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoradUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = _client.Get(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }

    }
}
