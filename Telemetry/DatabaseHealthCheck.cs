using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Telemetry
{
    internal class DatabaseHealthCheck(XenniDB _xenniDB) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var _state = HealthCheckResult.Healthy();
            try
            {
                using var cmd = _xenniDB.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "select 1";
                cmd.CommandType = System.Data.CommandType.Text;
                await _xenniDB.Database.OpenConnectionAsync(cancellationToken);
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }
            catch
            {
                _state = HealthCheckResult.Unhealthy();
            }

            return _state;
        }
    }
}
