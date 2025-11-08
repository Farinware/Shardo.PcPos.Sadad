using Newtonsoft.Json;
namespace Shardo.PcPos.Sadad.Utils
{
    internal static class Json
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.None
        };
    }
}
