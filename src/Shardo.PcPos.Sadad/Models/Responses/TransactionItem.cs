using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class TransactionItem
    {
        [JsonProperty("Amount")] public string? Amount { get; set; }
        [JsonProperty("CardNo")] public string? CardNo { get; set; }
        [JsonProperty("ResponseCode")] public string? ResponseCode { get; set; }
        [JsonProperty("Rrn")] public string? Rrn { get; set; }
        [JsonProperty("TransactionNo")] public string? TransactionNo { get; set; }
        [JsonProperty("TransactionDate")] public string? TransactionDate { get; set; }
        [JsonProperty("TransactionTime")] public string? TransactionTime { get; set; }
        [JsonProperty("TransactionType")] public string? TransactionType { get; set; }
        [JsonProperty("OrderId")] public string? OrderId { get; set; }
    }
}
