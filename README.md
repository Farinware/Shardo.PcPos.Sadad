# Shardo.PcPos.Sadad

کتابخانهٔ دات‌نت برای اتصال به **PcPos REST API** (سداد)؛ سازگار با **netstandard2.0;netstandard2.1;netcoreapp3.0;netcoreapp3.1;net5.0;net6.0;net7.0;net8.0;net9.0**، تمیز، ماژولار و قابل تزریق در DI.

---

## ویژگی‌ها
- پوشش مستقیم تمام متدهای **PcPos REST API**
- سازگار با **HttpClient** و **Dependency Injection**
- مدل‌های ورودی/خروجی تایپ‌شده
- پشتیبانی از **چند حسابی (تسهیم)** `MultiAccount`
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
var conn = PcPosConnection.Lan("172.16.33.180", "8888", DeviceType.BlueBird, "C0000301", "M00000000000152");

var sale = new SaleRequest
{
    OrderId = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
    SaleId = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
    Amount = "1000",
    AdvertisementData = "TEST"
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

## راهنمای کامل توابع

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
