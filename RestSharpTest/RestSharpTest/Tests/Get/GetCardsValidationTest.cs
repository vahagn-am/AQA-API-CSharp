using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Get
{

    public class GetCardsValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(AuthValidationArgumenstProvider))]
        public async Task CheckGetCardsWitInvalidAuth(AuthValidationArgumentsHolder arguments)
        {
            var request = RequestWithoutAuth(CardEndpoints.GetAllCardsURL, Method.Get)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            var response = await _client.ExecuteAsync(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        public async Task CheckGetCardsWithAnotherCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.GetSingleCardUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public async Task CheckGetSpecificCardInvalidId(CardIdValidationArgumentsHolder cardIdValidationArguments)
        {
            var request = RequestWithAuth(CardEndpoints.GetSingleCardUrl, Method.Get)
                .AddOrUpdateParameters(cardIdValidationArguments.PathParams);

            var response = await _client.ExecuteAsync(request);
            Assert.That(cardIdValidationArguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(cardIdValidationArguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForCard))]
        public async Task CheckGetSpecificCardWitInvalidAuth(AuthValidationArgumentsHolderForCard arguments)
        {
            var request = RequestWithoutAuth(CardEndpoints.GetSingleCardUrl, Method.Get)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);

            var response = await _client.ExecuteAsync(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        [Test]
        public async Task CheckGetSpecificCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.GetSingleCardUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}

