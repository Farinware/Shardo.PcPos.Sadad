using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class DeviceDiscoveryResult
    {
        [JsonProperty("ComPort")]
        public string? ComPort { get; set; }
        [JsonProperty("ConnectionType")]
        public int? ConnectionType { get; set; }
        [JsonProperty("ConnectionTypeName")]
        public string? ConnectionTypeName { get; set; }
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
