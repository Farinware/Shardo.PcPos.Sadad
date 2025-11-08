using Shardo.PcPos.Sadad.Models.Enums;
namespace Shardo.PcPos.Sadad.Api
{
    public sealed class PcPosConnection
    {
        public ConnectionType Connection { get; private set; }
        public DeviceType? DeviceType { get; private set; }
        public string? DeviceIp { get; private set; }
        public string? DevicePort { get; private set; }
        public string? SerialPort { get; private set; }
        public string? TerminalId { get; private set; }
        public string? MerchantId { get; private set; }
        private PcPosConnection() { }
        public static PcPosConnection Lan(string deviceIp, string devicePort, DeviceType? deviceType = null, string? terminalId = null, string? merchantId = null)
            => new PcPosConnection { Connection = ConnectionType.Lan, DeviceIp = deviceIp, DevicePort = devicePort, DeviceType = deviceType, TerminalId = terminalId, MerchantId = merchantId };
        public static PcPosConnection Serial(string serialPort, DeviceType? deviceType = null, string? terminalId = null, string? merchantId = null)
            => new PcPosConnection { Connection = ConnectionType.Serial, SerialPort = serialPort, DeviceType = deviceType, TerminalId = terminalId, MerchantId = merchantId };
    }
}
