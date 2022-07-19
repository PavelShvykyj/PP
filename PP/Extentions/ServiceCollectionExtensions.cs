using CoreTier.Configs;
using CoreTier.Interfaces;
using CoreTier.Services;

namespace PP.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEmailSevice(this IServiceCollection services, Action<EmailServiceConfig> options) 
        {
            var opt = new EmailServiceConfig();
            options(opt);
            var emailService = new EmailSevice();
            emailService.Configure(opt);
            services.AddSingleton<IEmailService>(emailService);
        }
    }
}
