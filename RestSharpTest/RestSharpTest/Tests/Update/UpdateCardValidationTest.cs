using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace RestSharpTest.Tests.Update
{
    public class UpdateCardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public void CheckUpdateCardWithInvalidId(CardIdValidationArgumentsHolder arguments )
        {
            var request = RequestWithAuth(CardEndpoints.UpdateCardUrl)
                .AddOrUpdateParameters(arguments.PathParams);
            var response = _client.Put(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForCard))]
        public void CheckUpdateCardWithoutAuth(AuthValidationArgumentsHolderForCard arguments ) {
            var request = RequestWithoutAuth(CardEndpoints.UpdateCardUrl)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedCardName" } });
            var response = _client.Put(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        [Test]
        public void CheckUpdatedCardWithAntherUserCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.DeleteCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.CardToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", "updatedCardName" } });
            var response = _client.Put(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }

    }
}
