using Microsoft.Extensions.DependencyInjection;
using Service.Abstractions;

namespace Service.Injector
{
    public static class EmailingInjector
    {
        public static void AddEmailService(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
        }
    }
}
