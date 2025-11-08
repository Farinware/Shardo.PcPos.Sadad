using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class SettingsItem
    {
        [JsonProperty("LockItem")]
        public bool? LockItem { get; set; }
        [JsonProperty("DeviceType")]
        public string? DeviceType { get; set; }
        [JsonProperty("DeviceTypeName")]
        public string? DeviceTypeName { get; set; }
        [JsonProperty("MerchantId")]
        public string? MerchantId { get; set; }
        [JsonProperty("SerialPort")]
        public string? SerialPort { get; set; }
        [JsonProperty("Tag")]
        public string? Tag { get; set; }
        [JsonProperty("TerminalId")]
        public string? TerminalId { get; set; }
        [JsonProperty("BaudRate")]
        public string? BaudRate { get; set; }
        [JsonProperty("StopBits")]
        public string? StopBits { get; set; }
    }
}
