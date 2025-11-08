using Newtonsoft.Json;
using Shardo.PcPos.Sadad.Models.Enums;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class TransactionReportRequest : PcPosRequestBase
    {
        [JsonProperty("Count")]
        public int? Count { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionTypeEnum { get; set; }
        private string? _transactionType;
        [JsonProperty("TransactionType")]
        public string? TransactionType
        {
            get => TransactionTypeEnum.HasValue ? ((int)TransactionTypeEnum.Value).ToString() : _transactionType;
            set
            {
                _transactionType = value;
                if (int.TryParse(value, out var v)) TransactionTypeEnum = (TransactionType)v; else TransactionTypeEnum = null;
            }
        }
    }
}
