namespace Shardo.PcPos.Sadad
{
    public sealed class SadadPcPosOptions
    {
        public string BaseAddress { get; set; } = "http://localhost:8050";
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(180);
        public string DefaultContentType { get; set; } = "application/json";
    }
}
