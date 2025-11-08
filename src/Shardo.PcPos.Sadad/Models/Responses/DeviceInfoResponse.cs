using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class DeviceInfoResponse
    {
        [JsonProperty("DeviceType")]
        public int? DeviceType { get; set; }
        [JsonProperty("DeviceTypeName")]
        public string? DeviceTypeName { get; set; }
        [JsonProperty("Header")]
        public string? Header { get; set; }
        [JsonProperty("IpAddress")]
        public string? IpAddress { get; set; }
        [JsonProperty("MerchantId")]
        public string? MerchantId { get; set; }
        [JsonProperty("MerchantName")]
        public string? MerchantName { get; set; }
        [JsonProperty("Port")]
        public string? Port { get; set; }
        [JsonProperty("TerminalId")]
        public string? TerminalId { get; set; }
    }
}
