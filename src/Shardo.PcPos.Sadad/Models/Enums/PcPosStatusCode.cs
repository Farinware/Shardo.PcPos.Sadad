namespace Shardo.PcPos.Sadad.Models.Enums
{
    public enum PcPosStatusCode
    {
        NoResponse = 0,
        MaxRetryError = 1,
        AbortedByService = 2,
        AbortedByDeviceUser = 3,
        Connected = 4,
        InternalError = 5,
        ConcurrentRequestError = 6,
        Unknown = 999
    }
}
