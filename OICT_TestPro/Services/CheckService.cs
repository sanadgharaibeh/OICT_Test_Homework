using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OICT_Test.Services
{
    public class CheckService : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int timer = new Random().Next(1, 100);
            if (timer > 50) { 
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
