using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
using System.Collections;

namespace RestSharpTest.Arguments.Providers
{
    public class CardBodyValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new CardBodyValidationArgumetsHolder()
            {
                BodyParams = new Dictionary<string, object>
                {
                    {"name", 123 },
                    {"idList", UrlParamValues.ExistingListId }
                },
                ErrorMessage = "invalid value for name"
            };
            yield return new CardBodyValidationArgumetsHolder()
            {
                BodyParams = new Dictionary<string, object>
                {
                    {"name", "New Card" },
                },
                ErrorMessage = "invalid value for idList"
            };
            yield return new CardBodyValidationArgumetsHolder()
            {
                BodyParams = new Dictionary<string, object>
                {
                    {"name", "New Card" },
                    {"idList", "Invalid idList" }
                },
                ErrorMessage = "invalid value for idList"
            };
        }
    }
}
