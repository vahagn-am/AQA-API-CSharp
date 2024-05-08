using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get
{

    public class GetBoardsValidationTest : BaseTest
    {

        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
        public void CheckGetBoardWithInvalidId(BoardIdValidationArgumetsHolder validationArgumetsHolder)
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddOrUpdateParameters(validationArgumetsHolder.PathParams);
            var response = _client.Get(request);
            Assert.That(validationArgumetsHolder.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(validationArgumetsHolder.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]
        public void CheckGetBoardWithInvalidAuth(AuthValidationArgumentsHolder authValidationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
                .AddOrUpdateParameters(authValidationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = _client.Get(request);

            Assert.That(authValidationArguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(authValidationArguments.ErrorMessage, Is.EqualTo(response.Content));

        }
        [Test]
        public void CheckGetBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = _client.Get(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}
