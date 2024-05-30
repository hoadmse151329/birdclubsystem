using Microsoft.Extensions.Caching.Distributed;

namespace WebAppMVC.Services.HostedServices
{
    public class ApplicationStopClearDataService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger<ApplicationStopClearDataService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ApplicationStopClearDataService(IHostApplicationLifetime appLifetime, ILogger<ApplicationStopClearDataService> logger, IServiceProvider serviceProvider)
        {
            _appLifetime = appLifetime;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStopping.Register(OnApplicationStopping);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnApplicationStopping()
        {
            _logger.LogInformation("Application is stopping... Clearing sessions.");

            // Clear the session data here
            /*using (var scope = _serviceProvider.CreateScope())
            {
                var sessionStore = scope.ServiceProvider.GetRequiredService<IDistributedCache>();

                // Clear all sessions
                // Implement specific logic for clearing sessions depending on your session management strategy

                // Example: If using SQL Server or another distributed cache, clear keys based on your session pattern
                // This example assumes you are using a distributed cache that allows key enumeration

                // Note: Adjust the following lines to your actual distributed cache implementation
                 sessionStore.re;

                // Add your session clearing logic here
            }*/
        }
    }
}
