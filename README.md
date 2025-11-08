# Shardo.Sadad

کتابخانهٔ دات‌نت برای اتصال به **PcPos REST API** (سداد)؛ سازگار با **.NET 8**، تمیز، ماژولار و قابل تزریق در DI.

> این README برای انتشار در GitHub آماده شده است. تمام عناوین، مثال‌ها و راهنما **فارسی** است.

---

## فهرست مطالب
- [ویژگی‌ها](#ویژگیها)
- [نصب](#نصب)
- [راه‌اندازی سریع](#راهاندازی-سریع)
- [اتصالات (LAN/Serial)](#اتصالات-lanserial)
- [Enums و مدل‌های کلیدی](#enums-و-مدلهای-کلیدی)
- [راهنمای کامل توابع (فارسی)](#راهنمای-کامل-توابع-فارسی)
- [نکات و عیب‌یابی](#نکات-و-عیبیابی)

---

## ویژگی‌ها
- پوشش مستقیم تمام متدهای **PcPos REST API**
- سازگار با **HttpClient** و **Dependency Injection**
- مدل‌های ورودی/خروجی تایپ‌شده
- پشتیبانی از **چندساًحابی (تسهیم)** و ساخت رشتهٔ `MultiAccount`
- مپ پیام‌های متداول `ResponseCode` به فارسی

> این کتابخانه صرفاً کلاینت است و سرور PcPos باید از قبل نصب/اجرا شده باشد.

---

## نصب

### 1) اضافه کردن پروژه به Solution
پوشهٔ `src/Shardo.Sadad` را به Solution خود اضافه کنید و رفرنس دهید:
```xml
<ProjectReference Include="src/Shardo.Sadad/Shardo.Sadad.csproj" />
```

### 2) ثبت سرویس در DI
```csharp
using Shardo.Sadad.Api.Extensions;

builder.Services.AddSadadPcPos(o =>
{
    o.BaseAddress = "http://localhost:8000";
    o.RequestTimeout = TimeSpan.FromSeconds(180);
});
```

---

## راه‌اندازی سریع
```csharp
using Shardo.Sadad.Api;
using Shardo.Sadad.Models.Enums;
using Shardo.Sadad.Models.Requests;

var client = provider.GetRequiredService<IPcPosClient>();
var conn = PcPosConnection.Lan("172.24.32.212", "8888", DeviceType.BlueBird, "C0000301", "M00000000000152");

var sale = new SaleRequest
{
    OrderId = "435261",
    RetryTimeOut = "5000, 5000, 5000",
    ResponseTimeout = "180000, 5000, 5000",
    Amount = "50000",
    MultiAccount = MultiAccountBuilder.ByRows(("1","10"), ("2","30")).Build(),
    DivideTypeEnum = DivideType.Percentage
};

var result = await client.SaleAsync(conn, sale);
```

---

## اتصالات (LAN/Serial)

### LAN
وقتی دستگاه POS روی شبکه است، اتصال با IP/Port انجام می‌شود:
```csharp
var lan = PcPosConnection.Lan("172.24.32.212", "8888", DeviceType.BlueBird, "C0000301", "M00000000000152");
```

### Serial
برای دستگاه‌های سریال، فقط نام پورت کافی است:
```csharp
var serial = PcPosConnection.Serial("COM1", DeviceType.Magic, "001", "011900133");
```

> فیلدهای `ConnectionType` باید دقیقاً `"Lan"` یا `"Serial"` در Payload باشند. این تبدیل به‌صورت خودکار در کلاینت انجام می‌شود.

---

## Enums و مدل‌های کلیدی

### DeviceType
| مقدار | برند |
|---|---|
| 0 | BlueBird |
| 1 | Magic |
| 2 | Bitel |
| 3 | Pax |
| 4 | Castle |

### DivideType
| مقدار | معنا |
|---|---|
| 3 | تسهیم درصدی |
| 4 | تسهیم مبلغی |

در کتابخانه از `DivideTypeEnum` استفاده کنید؛ خودکار به `"3"` یا `"4"` در JSON نگاشت می‌شود.

### TransactionType
| مقدار | معنا |
|---|---|
| 0 | ناموفق |
| 1 | موفق |
| 2 | همه |

در متد گزارش تراکنش از `TransactionTypeEnum` استفاده کنید.

### PcPosStatusCode
| مقدار | معنا |
|---|---|
| 0 | پاسخی دریافت نشد |
| 1 | حداکثر دفعات تکرار |
| 2 | لغو از سمت سرویس |
| 3 | لغو توسط کاربر دستگاه |
| 4 | ارتباط موفق با دستگاه |
| 5 | خطای داخلی |
| 6 | خطای درخواست همزمان |
| 999 | ناشناخته |

---

## راهنمای کامل توابع (فارسی)

> نگاشت نام تابع‌ها به مسیرهای REST و توضیح فارسی عملیات. برای اختصار، در هر آیتم یک نمونهٔ حداقلی آمده است.

### لیست سریع
| تابع کتابخانه | مسیر REST | توضیح کوتاه |
|---|---|---|
| `GetInfoAsync()` | `GET /api/GetInfo` | دریافت مشخصات سرویس |
| `GetDevicesAsync(port,timeout)` | `GET /api/GetDevices/{port}/{timeout}` | کشف دستگاه‌های LAN/Serial |
| `GetSerialPortsAsync()` | `GET /api/GetSerialPorts` | فهرست پورت‌های سریال فعال روی سیستم |
| `GetSettingsAsync()` | `GET /api/GetSettings` | تنظیمات Config سرویس |
| `SaleAsync(...)` | `POST /api/Sale` | خرید |
| `AbortAsync(...)` | `POST /api/Abort` | لغو عملیات جاری |
| `DeviceInfoAsync(...)` | `POST /api/DeviceInfo` | دریافت مشخصات دستگاه |
| `BillPaymentAsync(...)` | `POST /api/BillPayment` | پرداخت قبض |
| `IdentifiedInquiryAsync(...)` | `POST /api/IdentifiedInquiry` | خرید شناسه‌دار با استعلام |
| `GovernmentIdentifiedInquiryAsync(...)` | `POST /api/GovernmentIdentifiedInquiry` | خرید شناسه‌دار سازمانی (تک/چند شناسه) |
| `CommodityBasketAsync(...)` | `POST /api/CommodityBasket` | خرید بسته سبد کالا |
| `FoodSafetyAsync(...)` | `POST /api/FoodSafety` | خرید بسته امنیت غذایی |
| `TransactionReportAsync(...)` | `POST /api/TransactionReport` | گزارش ۰ تا ۱۳ تراکنش اخیر |
| `SearchAsync(...)` | `POST /api/Search` | جستجو بر اساس `OrderId` در تراکنش‌های دستگاه |
| `CardInfoAsync(...)` | `POST /api/CardInfo` | دریافت مشخصات کارت (برای سناریوهای خاص) |
| `GetAccountsAsync(...)` | `POST /api/GetAccounts` | لیست حساب‌های متصل به دستگاه |
| `RestartAsync()` | `GET /api/Restart` | راه‌اندازی مجدد سرویس REST |
| `MagicInquiryAsync(...)` | `POST /api/MagicInquiry` | استعلام بر اساس RRN برای Magic (Serial) |

### نمونه‌ها

<details>
<summary><b>GetInfoAsync</b> – وضعیت سرویس</summary>

```csharp
var info = await client.GetInfoAsync();
```
</details>

<details>
<summary><b>GetDevicesAsync</b> – کشف دستگاه‌ها</summary>

```csharp
var devices = await client.GetDevicesAsync(0, 0);
```
</details>

<details>
<summary><b>GetSerialPortsAsync</b> – پورت‌های سریال</summary>

```csharp
var ports = await client.GetSerialPortsAsync();
```
</details>

<details>
<summary><b>GetSettingsAsync</b> – تنظیمات سرویس</summary>

```csharp
var settings = await client.GetSettingsAsync();
```
</details>

<details>
<summary><b>DeviceInfoAsync</b> – مشخصات دستگاه</summary>

```csharp
var res = await client.DeviceInfoAsync(conn, new DeviceInfoRequest { OrderId = "9512" });
```
</details>

<details>
<summary><b>SaleAsync</b> – خرید</summary>

```csharp
var sale = await client.SaleAsync(conn, new SaleRequest
{
    OrderId = "435261",
    RetryTimeOut = "5000, 5000, 5000",
    ResponseTimeout = "180000, 5000, 5000",
    Amount = "50000",
    MultiAccount = MultiAccountBuilder.ByRows(("1","10"), ("2","30")).Build(),
    DivideTypeEnum = DivideType.Percentage
});
```
</details>

<details>
<summary><b>AbortAsync</b> – لغو عملیات</summary>

```csharp
var abort = await client.AbortAsync(conn, new AbortRequest());
```
</details>

<details>
<summary><b>BillPaymentAsync</b> – پرداخت قبض</summary>

```csharp
var bill = await client.BillPaymentAsync(conn, new BillPaymentRequest
{
    OrderId = "435261",
    BillId = "6039628301226",
    PayId  = "189840835"
});
```
</details>

<details>
<summary><b>IdentifiedInquiryAsync</b> – خرید شناسه‌دار</summary>

```csharp
var ident = await client.IdentifiedInquiryAsync(conn, new IdentifiedInquiryRequest
{
    OrderId = "435261",
    InquiryId = "139522000000014335"
});
```
</details>

<details>
<summary><b>GovernmentIdentifiedInquiryAsync</b> – خرید شناسه‌دار سازمانی</summary>

```csharp
var gov = await client.GovernmentIdentifiedInquiryAsync(conn, new GovernmentIdentifiedInquiryRequest
{
    OrderId = "435261",
    InquiryId = "102110000220000121100000000000",
    Amount = "110000"
});
```
چندشناسه:
```csharp
var many = await client.GovernmentIdentifiedInquiryAsync(conn, new GovernmentIdentifiedInquiryRequest
{
    OrderId = "435261",
    InquiryIds = new()
    {
        new() { Index = 1, InquiryId = "102110000220000121100000000000", Amount = 35000 },
        new() { Index = 2, InquiryId = "102110000220000121100000000001", Amount = 140000 },
    },
    Amount = "175000"
});
```
</details>

<details>
<summary><b>CommodityBasketAsync</b> – سبد کالا</summary>

```csharp
var basket = await client.CommodityBasketAsync(conn, new CommodityBasketRequest
{
    OrderId = "435261",
    Amount = "110000"
});
```
</details>

<details>
<summary><b>FoodSafetyAsync</b> – امنیت غذایی</summary>

```csharp
var food = await client.FoodSafetyAsync(conn, new FoodSafetyRequest
{
    OrderId = "435261",
    Amount = "110000"
});
```
</details>

<details>
<summary><b>TransactionReportAsync</b> – گزارش تراکنش‌ها</summary>

```csharp
var report = await client.TransactionReportAsync(conn, new TransactionReportRequest
{
    OrderId = "435261",
    Count = 13,
    TransactionTypeEnum = TransactionType.All
});
```
</details>

<details>
<summary><b>SearchAsync</b> – جستجو بر اساس OrderId</summary>

```csharp
var found = await client.SearchAsync(conn, new SearchRequest { OrderId = "78965641" });
```
</details>

<details>
<summary><b>CardInfoAsync</b> – دریافت مشخصات کارت</summary>

```csharp
var card = await client.CardInfoAsync(conn, new CardInfoRequest { OrderId = "435261" });
```
</details>

<details>
<summary><b>GetAccountsAsync</b> – لیست حساب‌های متصل</summary>

```csharp
var accounts = await client.GetAccountsAsync(conn, new GetAccountsRequest { OrderId = "435261" });
```
</details>

<details>
<summary><b>RestartAsync</b> – راه‌اندازی مجدد سرویس REST</summary>

```csharp
await client.RestartAsync();
```
</details>

<details>
<summary><b>MagicInquiryAsync</b> – استعلام RRN (Magic/Serial)</summary>

```csharp
var magic = await client.MagicInquiryAsync(new MagicInquiryRequest
{
    TerminalId = "001",
    MerchantId = "011900133",
    SerialPort = "COM1",
    RRN = "144700170050"
});
```
</details>

---

## نکات و عیب‌یابی
- `Content-Type` در تست Postman باید `application/json` باشد.
- در jQuery اگر از AJAX استفاده می‌کنید، ممکن است نیاز به `application/x-www-form-urlencoded` باشد.
- برای دستگاه‌های Serial، مقادیر پورت و تنظیمات در Config سرویس باید صحیح باشد.
- در حالت HTTPS باید گواهی معتبر در سیستم نصب شود.
- در صورت فعال بودن Firewall سیستم‌عامل، اجازهٔ دسترسی سرویس به پورت‌ها را بدهید.
