using Newtonsoft.Json;
using Shardo.PcPos.Sadad.Models.Enums;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class CardInfoResponse
    {
        [JsonProperty("CardInfo")]
        public string? CardInfo { get; set; }
        [JsonProperty("CardNo")]
        public string? CardNo { get; set; }
        [JsonProperty("MerchantId")]
        public string? MerchantId { get; set; }
        [JsonProperty("OrderId")]
        public string? OrderId { get; set; }
        [JsonProperty("PcPosStatus")]
        public string? PcPosStatus { get; set; }
        [JsonProperty("PcPosStatusCode")]
        public PcPosStatusCode? PcPosStatusCode { get; set; }
        [JsonProperty("TerminalId")]
        public string? TerminalId { get; set; }
    }
}
