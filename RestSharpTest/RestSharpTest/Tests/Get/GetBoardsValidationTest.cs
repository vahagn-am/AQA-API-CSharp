using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;
using System.Threading.Tasks;

namespace RestSharpTest.Tests.Get
{

    public class GetBoardsValidationTest : BaseTest
    {

        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
        public async Task CheckGetBoardWithInvalidId(BoardIdValidationArgumetsHolder validationArgumetsHolder)
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddOrUpdateParameters(validationArgumetsHolder.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.That(validationArgumetsHolder.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(validationArgumetsHolder.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]
        public async Task CheckGetBoardWithInvalidAuth(AuthValidationArgumentsHolder authValidationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddOrUpdateParameters(authValidationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = await _client.ExecuteAsync(request);

            Assert.That(authValidationArguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(authValidationArguments.ErrorMessage, Is.EqualTo(response.Content));

        }
        [Test]
        public async Task CheckGetBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExisitngBoardId);

            var response = await _client.ExecuteAsync(request);

            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}
