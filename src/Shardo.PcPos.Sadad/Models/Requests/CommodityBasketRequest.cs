using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class CommodityBasketRequest : PcPosRequestBase
    {
        [JsonProperty("Amount")]
        public string? Amount { get; set; }
    }
}
