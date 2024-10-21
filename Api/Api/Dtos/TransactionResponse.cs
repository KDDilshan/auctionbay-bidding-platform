namespace Api.Dtos
{
    public class TransactionResponse
    {
        public string Id { get; set; }
        public string Auction {  get; set; }
        public string Nft { get; set; }
        public DateTime Date { get; set; }
        public long Amount { get; set; }
    }
}
