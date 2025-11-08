using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class BillPaymentRequest : PcPosRequestBase
    {
        [JsonProperty("BillId")]
        public string? BillId { get; set; }
        [JsonProperty("PayId")]
        public string? PayId { get; set; }
    }
}
