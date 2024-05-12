using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Create
{
    public class CreatedCradValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardBodyValidationArgumentsProvider))]
        public async Task CheckCreateCardWithInvalidName(CardBodyValidationArgumetsHolder arguments)
        {
            var request = RequestWithAuth(CardEndpoints.CreateCardUrl, Method.Post)
                .AddJsonBody(arguments.BodyParams);
            var response = await _client.ExecuteAsync(request);

            Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForCard))]

        public async Task CheckCreateCardWithInvalidAuth(AuthValidationArgumentsHolderForCard arguments)
        {
            var CardName = "New Card";
            var request = RequestWithoutAuth(CardEndpoints.CreateCardUrl, Method.Post)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddJsonBody(new Dictionary<string, object> {
                    { "name", CardName },
                    {"idList", UrlParamValues.ExistingListId }
                });
            var response = await _client.ExecuteAsync(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage,
                        Is.EqualTo(response.Content));

        }

        [Test]
        public async Task CheckCreateCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.CreateCardUrl, Method.Post)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddJsonBody(new Dictionary<string, object>
                {
                    {"name", "New Card" },
                    {"idList", UrlParamValues.ExistingListId }
                });
            var response = await _client.ExecuteAsync(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}
