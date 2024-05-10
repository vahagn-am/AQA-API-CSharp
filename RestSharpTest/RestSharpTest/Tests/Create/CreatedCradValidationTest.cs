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
        public void CheckCreateCardWithInvalidName(CardBodyValidationArgumetsHolder arguments)
        {
            var request = RequestWithAuth(CardEndpoints.CreateCardUrl)
                .AddJsonBody(arguments.BodyParams);
            var response = _client.Post(request);

            Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));

        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForCard))]

        public void CheckCreateCardWithInvalidAuth(AuthValidationArgumentsHolderForCard arguments)
        {
            var CardName = "New Card";
            var request = RequestWithoutAuth(CardEndpoints.CreateCardUrl)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddJsonBody(new Dictionary<string, object> {
                    { "name", CardName },
                    {"idList", UrlParamValues.ExistingListId }
                });
            var response = _client.Post(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage,
                        Is.EqualTo(response.Content));

        }

        [Test]
        public void CheckCreateCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardEndpoints.CreateCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddJsonBody(new Dictionary<string, object>
                {
                    {"name", "New Card" },
                    {"idList", UrlParamValues.ExistingListId }
                });
            var response = _client.Post(request);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
            Assert.That("invalid key", Is.EqualTo(response.Content));
        }
    }
}
