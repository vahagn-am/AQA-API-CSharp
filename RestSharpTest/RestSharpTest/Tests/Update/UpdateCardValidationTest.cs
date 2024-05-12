using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Update
{
    public class UpdateCardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public async Task CheckUpdateCardWithInvalidId(CardIdValidationArgumentsHolder arguments )
        {
            var request = RequestWithAuth(CardEndpoints.UpdateCardUrl, Method.Put)
                .AddOrUpdateParameters(arguments.PathParams);
            var response = await _client.ExecuteAsync(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForCard))]
        public async Task CheckUpdateCardWithoutAuth(AuthValidationArgumentsHolderForCard arguments ) {
            var request = RequestWithoutAuth(CardEndpoints.UpdateCardUrl, Method.Put)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedCardName" } });
            var response = await _client.ExecuteAsync(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        [Test]
        public async Task CheckUpdatedCardWithAntherUserCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.DeleteCardUrl, Method.Put)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedCardName" } });
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }

    }
}
