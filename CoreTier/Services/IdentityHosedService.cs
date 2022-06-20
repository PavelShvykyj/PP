using CoreTier.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    public class IdentityHosedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public IdentityHosedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope()) 
            {
                var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
                await identityService.SeedIdentityDataBaseAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
