using RestSharp;

namespace RestSharpTest.Consts
{
    public class UrlParamValues
    {
        public const string ExisitngBoardId = "6639225e817a9e31b1a3f61a";
        public const string UserName = "vahagnqa";
        public const string ExistingListId = "6639225e817a9e31b1a3f621";
        public const string ExistingCardId = "6639ca0fc3ec23d350a126a9";
        public const string ValidKey = "73999fcb6a39c4d0d33207fcef1db3c7";
        public const string ValidToken = "ATTA89041bea7ea15ffc07c0c913a3b3bf5101fb5141ac089e9214be7e1183c3db02C77514F5";


        public static readonly IEnumerable<Parameter> AuthQueryParams = new[]
        {
            new Parameter("key", ValidKey, ParameterType.QueryString),
            new Parameter("token", ValidToken, ParameterType.QueryString)

        };
        public static readonly IEnumerable<Parameter> AnotherUserAuthQueryParams = new[]
{
            new Parameter("key", "b32218e6887516d17c84253faf967b6", ParameterType.QueryString),
            new Parameter("token", "ATTA89041bea7ea15ffc07c0c913a3b3bf5101fb5141ac089e9214be7e1183c3db02C77514F7", ParameterType.QueryString)

        };



    }
}
