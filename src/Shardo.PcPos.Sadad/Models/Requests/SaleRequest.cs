using Newtonsoft.Json;
using Shardo.PcPos.Sadad.Models.Enums;
namespace Shardo.PcPos.Sadad.Models.Requests
{
    public sealed class SaleRequest : PcPosRequestBase
    {
        [JsonProperty("Amount")]
        public string? Amount { get; set; }
        [JsonProperty("MultiAccount")]
        public string? MultiAccount { get; set; }
        [JsonIgnore]
        public DivideType? DivideTypeEnum { get; set; }
        private string? _divideType;
        [JsonProperty("DivideType")]
        public string? DivideType
        {
            get => DivideTypeEnum.HasValue ? ((int)DivideTypeEnum.Value).ToString() : _divideType;
            set
            {
                _divideType = value;
                if (int.TryParse(value, out var v)) DivideTypeEnum = (DivideType)v; else DivideTypeEnum = null;
            }
        }
    }
}
