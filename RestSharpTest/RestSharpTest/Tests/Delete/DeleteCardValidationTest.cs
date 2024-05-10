using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;
using System.Net;

namespace RestSharpTest.Tests.Delete
{
    public class DeleteCardValidationTest : BaseTest
    {

        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public void CheckDeleteCardWithInvalidID(CardIdValidationArgumentsHolder arguments)
        {
            var request = RequestWithAuth(CardEndpoints.DeleteCardUrl)
                .AddOrUpdateParameters(arguments.PathParams);
            var response = _client.Delete(request);

            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }
        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProviderForDeletingCard))]
        public void CheckDeleteCardWitinvalidAuth(AuthValidationArgumentsHolderForDeletingCard arguments)
        {
            var request = RequestWithoutAuth(CardEndpoints.DeleteCardUrl)
                .AddOrUpdateParameters(arguments.AuthParams)
                .AddUrlSegment("url", UrlParamValues.ExistingCardId);
            var response = _client.Delete(request);
            Assert.That(arguments.StatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(arguments.ErrorMessage, Is.EqualTo(response.Content));
        }

        public void CheckDeleteCardWithAnotherUserCredentials() { }
    }
}
