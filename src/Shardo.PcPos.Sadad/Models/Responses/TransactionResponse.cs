using Newtonsoft.Json;
using Shardo.PcPos.Sadad.Models.Enums;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class TransactionResponse
    {
        [JsonProperty("Amount")]
        public string? Amount { get; set; }
        [JsonProperty("ApprovalCode")]
        public string? ApprovalCode { get; set; }
        [JsonProperty("CardNo")]
        public string? CardNo { get; set; }
        [JsonProperty("Merchant")]
        public string? MerchantId { get; set; }
        [JsonProperty("OptionalField")]
        public string? OptionalField { get; set; }
        [JsonProperty("OrderId")]
        public string? OrderId { get; set; }
        [JsonProperty("PacketType")]
        public string? PacketType { get; set; }
        [JsonProperty("PcPosStatus")]
        public string? PcPosStatus { get; set; }
        [JsonProperty("PcPosStatusCode")]
        public PcPosStatusCode? PcPosStatusCode { get; set; }
        [JsonProperty("ProcessingCode")]
        public string? ProcessingCode { get; set; }
        [JsonProperty("ResponseCode")]
        public string? ResponseCode { get; set; }
        [JsonProperty("ResponseCodeMessage")]
        public string? ResponseCodeMessage { get; set; }
        [JsonProperty("Rrn")]
        public string? Rrn { get; set; }
        [JsonProperty("SaleId")]
        public string? SaleId { get; set; }
        [JsonProperty("Terminal")]
        public string? Terminal { get; set; }
        [JsonProperty("TerminalId")]
        public string? TerminalId { get; set; }
        [JsonProperty("TransactionDate")]
        public string? TransactionDate { get; set; }
        [JsonProperty("TransactionNo")]
        public string? TransactionNo { get; set; }
        [JsonProperty("TransactionTime")]
        public string? TransactionTime { get; set; }
    }
}
