using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class MagicInquiryRequest : PcPosRequestBase
    {
        [JsonProperty("RRN")]
        public string? RRN { get; set; }
    }
}
