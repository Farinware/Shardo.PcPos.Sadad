using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shardo.PcPos.Sadad.Exceptions;
using Shardo.PcPos.Sadad.Models.Enums;
using Shardo.PcPos.Sadad.Models.Requests;
using Shardo.PcPos.Sadad.Models.Responses;
using Shardo.PcPos.Sadad.Utils;
namespace Shardo.PcPos.Sadad.Api
{
    public sealed class PcPosClient : IPcPosClient
    {
        private readonly HttpClient _http;
        private readonly SadadPcPosOptions _options;
        public PcPosClient(HttpClient http, IOptions<SadadPcPosOptions> options)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _options = options?.Value ?? new SadadPcPosOptions();
            if (_http.BaseAddress == null && !string.IsNullOrWhiteSpace(_options.BaseAddress))
                _http.BaseAddress = new Uri(_options.BaseAddress);
            _http.Timeout = _options.RequestTimeout;
        }
        //public PcPosClient(HttpClient http, SadadPcPosOptions options)
        //{
        //    _http = http ?? throw new ArgumentNullException(nameof(http));
        //    _options = options ?? new SadadPcPosOptions();
        //    if (_http.BaseAddress == null && !string.IsNullOrWhiteSpace(_options.BaseAddress))
        //        _http.BaseAddress = new Uri(_options.BaseAddress);
        //    _http.Timeout = _options.RequestTimeout;
        //}
        private static T MergeConnection<T>(PcPosConnection connection, T request) where T : PcPosRequestBase
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (request == null) throw new ArgumentNullException(nameof(request));
            request.ConnectionType = connection.Connection == ConnectionType.Lan ? "Lan" : "Serial";
            request.DeviceType = connection.DeviceType.HasValue ? (int?)connection.DeviceType.Value : null;
            request.DeviceIp = connection.DeviceIp;
            request.DevicePort = connection.DevicePort;
            request.SerialPort = connection.SerialPort;
            request.TerminalId = connection.TerminalId ?? request.TerminalId;
            request.MerchantId = connection.MerchantId ?? request.MerchantId;
            return request;
        }
        private static async Task<T?> ReadOrThrowAsync<T>(HttpResponseMessage resp)
        {
            var content = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                throw new SadadPcPosException($"HTTP error {(int)resp.StatusCode} when calling PcPos.", resp.StatusCode, content);
            if (string.IsNullOrWhiteSpace(content))
                return default;
            return JsonConvert.DeserializeObject<T>(content, Json.Settings);
        }
        private static StringContent ToJsonContent<T>(T payload)
            => new StringContent(JsonConvert.SerializeObject(payload, Json.Settings), System.Text.Encoding.UTF8, "application/json");
        public async Task<InfoResponse?> GetInfoAsync(CancellationToken cancellationToken = default)
        {
            using var resp = await _http.GetAsync("/api/GetInfo", cancellationToken);
            return await ReadOrThrowAsync<InfoResponse>(resp);
        }
        public async Task<IReadOnlyList<DeviceDiscoveryResult>> GetDevicesAsync(int? port = null, int? timeout = null, CancellationToken cancellationToken = default)
        {
            var p = port ?? 0;
            var t = timeout ?? 0;
            using var resp = await _http.GetAsync($"/api/GetDevices/{p}/{t}", cancellationToken);
            var list = await ReadOrThrowAsync<List<DeviceDiscoveryResult>>(resp);
            return list ?? new List<DeviceDiscoveryResult>();
        }
        public async Task<IReadOnlyList<string>> GetSerialPortsAsync(CancellationToken cancellationToken = default)
        {
            using var resp = await _http.GetAsync("/api/GetSerialPorts", cancellationToken);
            var list = await ReadOrThrowAsync<List<string>>(resp);
            return list ?? new List<string>();
        }
        public async Task<IReadOnlyList<SettingsItem>> GetSettingsAsync(CancellationToken cancellationToken = default)
        {
            using var resp = await _http.GetAsync("/api/GetSettings", cancellationToken);
            var list = await ReadOrThrowAsync<List<SettingsItem>>(resp);
            return list ?? new List<SettingsItem>();
        }
        public async Task<DeviceInfoResponse?> DeviceInfoAsync(PcPosConnection connection, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, new DeviceInfoRequest { });
            using var resp = await _http.PostAsync("/api/DeviceInfo", ToJsonContent(payload), cancellationToken);
            return await ReadOrThrowAsync<DeviceInfoResponse>(resp);
        }
        public async Task<TransactionResponse?> SaleAsync(PcPosConnection connection, SaleRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/Sale", ToJsonContent(payload), cancellationToken);
            var result = await ReadOrThrowAsync<TransactionResponse>(resp);
            if (result != null && string.IsNullOrEmpty(result.ResponseCodeMessage))
                result.ResponseCodeMessage = ResponseCodeMessages.TryGet(result.ResponseCode);
            return result;
        }
        public async Task<TransactionResponse?> AbortAsync(PcPosConnection connection, AbortRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/Abort", ToJsonContent(payload), cancellationToken);
            return await ReadOrThrowAsync<TransactionResponse>(resp);
        }
        public async Task<TransactionResponse?> BillPaymentAsync(PcPosConnection connection, BillPaymentRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/BillPayment", ToJsonContent(payload), cancellationToken);
            var result = await ReadOrThrowAsync<TransactionResponse>(resp);
            if (result != null && string.IsNullOrEmpty(result.ResponseCodeMessage))
                result.ResponseCodeMessage = ResponseCodeMessages.TryGet(result.ResponseCode);
            return result;
        }
        public async Task<TransactionResponse?> IdentifiedInquiryAsync(PcPosConnection connection, IdentifiedInquiryRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/IdentifiedInquiry", ToJsonContent(payload), cancellationToken);
            var result = await ReadOrThrowAsync<TransactionResponse>(resp);
            if (result != null && string.IsNullOrEmpty(result.ResponseCodeMessage))
                result.ResponseCodeMessage = ResponseCodeMessages.TryGet(result.ResponseCode);
            return result;
        }
        public async Task<TransactionResponse?> GovernmentIdentifiedInquiryAsync(PcPosConnection connection, GovernmentIdentifiedInquiryRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/GovernmentIdentifiedInquiry", ToJsonContent(payload), cancellationToken);
            var result = await ReadOrThrowAsync<TransactionResponse>(resp);
            if (result != null && string.IsNullOrEmpty(result.ResponseCodeMessage))
                result.ResponseCodeMessage = ResponseCodeMessages.TryGet(result.ResponseCode);
            return result;
        }
        public async Task<TransactionResponse?> CommodityBasketAsync(PcPosConnection connection, CommodityBasketRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/CommodityBasket", ToJsonContent(payload), cancellationToken);
            var result = await ReadOrThrowAsync<TransactionResponse>(resp);
            if (result != null && string.IsNullOrEmpty(result.ResponseCodeMessage))
                result.ResponseCodeMessage = ResponseCodeMessages.TryGet(result.ResponseCode);
            return result;
        }
        public async Task<TransactionResponse?> FoodSafetyAsync(PcPosConnection connection, FoodSafetyRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/FoodSafety", ToJsonContent(payload), cancellationToken);
            var result = await ReadOrThrowAsync<TransactionResponse>(resp);
            if (result != null && string.IsNullOrEmpty(result.ResponseCodeMessage))
                result.ResponseCodeMessage = ResponseCodeMessages.TryGet(result.ResponseCode);
            return result;
        }
        public async Task<TransactionReportResponse?> TransactionReportAsync(PcPosConnection connection, TransactionReportRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/TransactionReport", ToJsonContent(payload), cancellationToken);
            return await ReadOrThrowAsync<TransactionReportResponse>(resp);
        }
        public async Task<TransactionReportResponse?> SearchAsync(PcPosConnection connection, SearchRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/Search", ToJsonContent(payload), cancellationToken);
            return await ReadOrThrowAsync<TransactionReportResponse>(resp);
        }
        public async Task<CardInfoResponse?> CardInfoAsync(PcPosConnection connection, CardInfoRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/CardInfo", ToJsonContent(payload), cancellationToken);
            return await ReadOrThrowAsync<CardInfoResponse>(resp);
        }
        public async Task<GetAccountsResponse?> GetAccountsAsync(PcPosConnection connection, GetAccountsRequest request, CancellationToken cancellationToken = default)
        {
            var payload = MergeConnection(connection, request);
            using var resp = await _http.PostAsync("/api/GetAccounts", ToJsonContent(payload), cancellationToken);
            return await ReadOrThrowAsync<GetAccountsResponse>(resp);
        }
        public async Task RestartAsync(CancellationToken cancellationToken = default)
        {
            using var resp = await _http.GetAsync("/api/Restart", cancellationToken);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                throw new SadadPcPosException("Could not restart PcPos REST service.", resp.StatusCode, body);
            }
        }
        public async Task<TransactionResponse?> MagicInquiryAsync(MagicInquiryRequest request, CancellationToken cancellationToken = default)
        {
            request.ConnectionType = "Serial";
            using var resp = await _http.PostAsync("/api/MagicInquiry", ToJsonContent(request), cancellationToken);
            return await ReadOrThrowAsync<TransactionResponse>(resp);
        }
    }
}
