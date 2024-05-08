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
        public void CheckGetCardsWitInvalidAuth(AuthValidationArgumentsHolder arguments)
        {
            var request = RequestWithoutAuth(CardEndpoints.GetAllCardsURL)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            var response = _client.Get(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        public void CheckGetCardsWithAnotherCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.GetSingleCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public void CheckGetSpecificCardInvalidId(CardIdValidationArgumentsHolder cardIdValidationArguments)
        {
            var request = RequestWithAuth(CardEndpoints.GetSingleCardUrl)
                .AddOrUpdateParameters(cardIdValidationArguments.PathParams);

            var response = _client.Get(request);
            Assert.That(cardIdValidationArguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(cardIdValidationArguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForCard))]
        public void CheckGetSpecificCardWitInvalidAuth(AuthValidationArgumentsHolderForCard arguments)
        {
            var request = RequestWithoutAuth(CardEndpoints.GetSingleCardUrl)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);

            var response = _client.Get(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        [Test]
        public void CheckGetSpecificCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.GetSingleCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}

