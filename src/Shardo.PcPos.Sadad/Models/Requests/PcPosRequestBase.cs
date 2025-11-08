using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public class PcPosRequestBase
    {
        [JsonProperty("ConnectionType")]
        public string? ConnectionType { get; set; }
        [JsonProperty("DeviceType")]
        public int? DeviceType { get; set; }
        [JsonProperty("DeviceIp")]
        public string? DeviceIp { get; set; }
        [JsonProperty("DevicePort")]
        public string? DevicePort { get; set; }
        [JsonProperty("SerialPort")]
        public string? SerialPort { get; set; }
        [JsonProperty("TerminalId")]
        public string? TerminalId { get; set; }
        [JsonProperty("MerchantId")]
        public string? MerchantId { get; set; }
        [JsonProperty("OrderId")]
        public string? OrderId { get; set; }
        [JsonProperty("SaleId")]
        public string? SaleId { get; set; }
        [JsonProperty("RetryTimeOut")]
        public string? RetryTimeOut { get; set; }
        [JsonProperty("ResponseTimeout")]
        public string? ResponseTimeout { get; set; }
        [JsonProperty("AdvertisementData")]
        public string? AdvertisementData { get; set; }
    }
}
