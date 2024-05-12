using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Delete
{
    public class DeleteCardValidationTest : BaseTest
    {

        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public async Task CheckDeleteCardWithInvalidID(CardIdValidationArgumentsHolder arguments)
        {
            var request = RequestWithAuth(CardEndpoints.DeleteCardUrl, Method.Delete)
                .AddOrUpdateParameters(arguments.PathParams);
            var response = await _client.ExecuteAsync(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }
        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForDeletingCard))]
        public async Task CheckDeleteCardWitinvalidAuth(AuthValidationArgumentsHolderForDeletingCard arguments)
        {
            var request = RequestWithoutAuth(CardEndpoints.DeleteCardUrl, Method.Delete)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("url", UrlParamValues.ExistingCardId);
            var response = await _client.ExecuteAsync(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        public void CheckDeleteCardWithAnotherUserCredentials() { }
    }
}
