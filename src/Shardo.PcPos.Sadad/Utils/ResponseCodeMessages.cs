namespace Shardo.PcPos.Sadad.Utils
{
    public static class ResponseCodeMessages
    {
        private static readonly Dictionary<string, string> _map = new System.Collections.Generic.Dictionary<string, string>()
        {
            ["00"] = "تراکنش موفق",
            ["12"] = "تراکنش نامعتبر",
            ["13"] = "مبلغ نامعتبر",
            ["14"] = "شماره کارت نامعتبر",
            ["19"] = "تلاش مجدد",
            ["30"] = "قالب/فرمت اشتباه",
            ["51"] = "عدم موجودی کافی",
            ["54"] = "کارت منقضی است",
            ["55"] = "رمز نادرست",
            ["61"] = "مبلغ بیش از حد مجاز",
        };
        public static string? TryGet(string? responseCode) => responseCode != null && _map.TryGetValue(responseCode, out var txt) ? txt : null;
    }
}
