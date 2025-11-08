using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class GetAccountsResponse
    {
        [JsonProperty("ResponseCode")]
        public string? ResponseCode { get; set; }
        [JsonProperty("ResponseCodeMessage")]
        public string? ResponseCodeMessage { get; set; }
        [JsonProperty("Accounts")]
        public System.Collections.Generic.List<AccountItem>? Accounts { get; set; }
        [JsonProperty("OrderId")]
        public string? OrderId { get; set; }
        [JsonProperty("PcPosStatus")]
        public string? PcPosStatus { get; set; }
        [JsonProperty("PcPosStatusCode")]
        public int? PcPosStatusCode { get; set; }
    }
    public sealed class AccountItem
    {
        [JsonProperty("Iban")]
        public string? Iban { get; set; }
        [JsonProperty("RowNo")]
        public string? RowNo { get; set; }
    }
}
