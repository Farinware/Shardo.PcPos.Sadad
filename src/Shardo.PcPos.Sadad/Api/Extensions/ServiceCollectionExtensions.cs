using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Shardo.PcPos.Sadad.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSadadPcPos(this IServiceCollection services, Action<SadadPcPosOptions>? configure = null)
        {
            if (configure != null) services.Configure(configure);
            else services.Configure<SadadPcPosOptions>(_ => { });
            services.AddHttpClient<IPcPosClient, PcPosClient>((sp, http) =>
            {
                var opt = sp.GetRequiredService<IOptions<SadadPcPosOptions>>().Value;
                if (!string.IsNullOrWhiteSpace(opt.BaseAddress))
                    http.BaseAddress = new Uri(opt.BaseAddress);
                http.Timeout = opt.RequestTimeout;
            });
            return services;
        }
    }
}
