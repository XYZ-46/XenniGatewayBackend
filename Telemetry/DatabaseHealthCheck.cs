﻿using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Telemetry
{
    internal class DatabaseHealthCheck(XenniDB _xenniDB) : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var _state = HealthCheckResult.Healthy();
            try
            {
                using var cmd = _xenniDB.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "select 1";
                _xenniDB.Database.OpenConnectionAsync(cancellationToken).Wait(cancellationToken);
                cmd.ExecuteNonQueryAsync(cancellationToken).Wait(cancellationToken);
            }
            catch 
            {
                _state = HealthCheckResult.Unhealthy();
            }

            return Task.FromResult(_state);
        }
    }
}
