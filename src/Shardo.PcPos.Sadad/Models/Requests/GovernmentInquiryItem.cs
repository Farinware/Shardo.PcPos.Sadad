using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class GovernmentInquiryItem
    {
        [JsonProperty("Index")]
        public int Index { get; set; }
        [JsonProperty("InquiryId")]
        public string InquiryId { get; set; } = default!;
        [JsonProperty("Amount")]
        public int Amount { get; set; }
    }
}
