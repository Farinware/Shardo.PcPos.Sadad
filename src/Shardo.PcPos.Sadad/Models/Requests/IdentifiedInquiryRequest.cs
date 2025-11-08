using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class IdentifiedInquiryRequest : PcPosRequestBase
    {
        [JsonProperty("InquiryId")]
        public string? InquiryId { get; set; }
    }
}
