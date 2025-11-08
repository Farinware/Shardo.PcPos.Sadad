using Shardo.PcPos.Sadad.Models.Requests;
using Shardo.PcPos.Sadad.Models.Responses;
namespace Shardo.PcPos.Sadad.Api
{
    public interface IPcPosClient
    {
        Task<InfoResponse?> GetInfoAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DeviceDiscoveryResult>> GetDevicesAsync(int? port = null, int? timeout = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<string>> GetSerialPortsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<SettingsItem>> GetSettingsAsync(CancellationToken cancellationToken = default);
        Task<DeviceInfoResponse?> DeviceInfoAsync(PcPosConnection connection, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> SaleAsync(PcPosConnection connection, SaleRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> AbortAsync(PcPosConnection connection, AbortRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> BillPaymentAsync(PcPosConnection connection, BillPaymentRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> IdentifiedInquiryAsync(PcPosConnection connection, IdentifiedInquiryRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> GovernmentIdentifiedInquiryAsync(PcPosConnection connection, GovernmentIdentifiedInquiryRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> CommodityBasketAsync(PcPosConnection connection, CommodityBasketRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse?> FoodSafetyAsync(PcPosConnection connection, FoodSafetyRequest request, CancellationToken cancellationToken = default);
        Task<TransactionReportResponse?> TransactionReportAsync(PcPosConnection connection, TransactionReportRequest request, CancellationToken cancellationToken = default);
        Task<TransactionReportResponse?> SearchAsync(PcPosConnection connection, SearchRequest request, CancellationToken cancellationToken = default);
        Task<CardInfoResponse?> CardInfoAsync(PcPosConnection connection, CardInfoRequest request, CancellationToken cancellationToken = default);
        Task<GetAccountsResponse?> GetAccountsAsync(PcPosConnection connection, GetAccountsRequest request, CancellationToken cancellationToken = default);
        Task RestartAsync(CancellationToken cancellationToken = default);
        Task<TransactionResponse?> MagicInquiryAsync(MagicInquiryRequest request, CancellationToken cancellationToken = default);
    }
}
