using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Responses
{
    public sealed class InfoResponse
    {
        [JsonProperty("Description")]
        public string? Description { get; set; }
        [JsonProperty("Supported Devices")]
        public string? SupportedDevices { get; set; }
        [JsonProperty("Version")]
        public string? Version { get; set; }
    }
}
