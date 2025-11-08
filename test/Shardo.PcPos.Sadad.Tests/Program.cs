
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shardo.PcPos.Sadad.Api;
using Shardo.PcPos.Sadad.Api.Extensions;
using Shardo.PcPos.Sadad.Models.Enums;
using Shardo.PcPos.Sadad.Models.Requests;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(s =>
    {
        s.AddSadadPcPos(o =>
        {
            o.BaseAddress = "http://localhost:8050";
            o.RequestTimeout = TimeSpan.FromSeconds(180);
        });
    })
    .Build();

using var scope = host.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<IPcPosClient>();

var lan = PcPosConnection.Lan("172.16.33.180", "8888", DeviceType.BlueBird, "", "");

var pcPosinfo = await client.GetInfoAsync();

var tttt = await client.DeviceInfoAsync(lan);

var saleLan = new SaleRequest
{
    OrderId = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
    SaleId = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
    Amount = "1000",
    AdvertisementData = "TEST"
};

var resLan = await client.SaleAsync(lan, saleLan);
Console.WriteLine($"LAN StatusCode={resLan?.PcPosStatusCode}");