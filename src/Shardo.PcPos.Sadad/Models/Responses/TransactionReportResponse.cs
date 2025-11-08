using Newtonsoft.Json;
using Shardo.PcPos.Sadad.Models.Enums;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class TransactionReportResponse
    {
        [JsonProperty("OrderId")]
        public string? OrderId { get; set; }
        [JsonProperty("PcPosStatus")]
        public string? PcPosStatus { get; set; }
        [JsonProperty("PcPosStatusCode")]
        public PcPosStatusCode? PcPosStatusCode { get; set; }
        [JsonProperty("Transactions")]
        public System.Collections.Generic.List<TransactionItem>? Transactions { get; set; }
    }
}
