using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class GovernmentIdentifiedInquiryRequest : PcPosRequestBase
    {
        [JsonProperty("InquiryId")]
        public string? InquiryId { get; set; }
        [JsonProperty("Amount")]
        public string? Amount { get; set; }
        [JsonProperty("InquiryIds")]
        public List<GovernmentInquiryItem>? InquiryIds { get; set; }
    }
}
