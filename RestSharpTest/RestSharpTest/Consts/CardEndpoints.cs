namespace RestSharpTest.Consts
{
    public class CardEndpoints
    {
        public const string GetAllCardsURL = "/1/lists/{list_id}/cards";
        public const string GetSingleCardUrl = "/1/cards/{id}";

        public const string CreateCardUrl = "1/cards";
        public const string DeleteCardUrl = "1/cards/{id}";

        public const string UpdateCardUrl = "1/cards/{id}";
    }
}
