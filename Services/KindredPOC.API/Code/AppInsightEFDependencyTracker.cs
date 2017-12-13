using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace KindredPOC.API.Code
{
    public class AppInsightEFDependencyTracker : IDbCommandInterceptor
    {
        private const string DependencyKind = "SQL";

        private readonly ConcurrentDictionary<DbCommand, DateTimeOffset> _commandStartTimes = new ConcurrentDictionary<DbCommand, DateTimeOffset>();
        private readonly TelemetryClient _telemetryClient;

        private readonly bool _logSqlCommand;

        public AppInsightEFDependencyTracker(bool logSqlCommand = false)
        {
            _logSqlCommand = logSqlCommand;
            _telemetryClient = new TelemetryClient();
        }

        private void OnStart(DbCommand command)
        {
            _commandStartTimes.TryAdd(command, DateTime.UtcNow);
        }

        private void OnFinished<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            DateTimeOffset startTime;

            _commandStartTimes.TryRemove(command, out startTime);
            try
            {
                Track(command, interceptionContext, startTime);
            }
            catch (Exception)
            {
            }
        }

        private void Track<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext, DateTimeOffset startTime)
        {
            var duration = TimeSpan.Zero;
            if (startTime != default(DateTimeOffset))
            {
                duration = DateTimeOffset.UtcNow - startTime;
            }

            var name = "SQL";
            if (command.Connection != null)
            {
                name = string.Format("{0} | {1}", command.Connection.DataSource, command.Connection.Database);
            }

            var commandName = string.Empty;
            if (_logSqlCommand)
            {
                commandName = command.CommandText;
            }

            var success = interceptionContext.Exception == null;

            _telemetryClient.TrackDependency(new DependencyTelemetry()
            {
                Name = name,
                Data =  commandName,
                Type = DependencyKind,
                Duration = duration,
                Timestamp = startTime,
                Success = success
            });
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            OnStart(command);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            OnStart(command);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            OnStart(command);
        }


        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            OnFinished(command, interceptionContext);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            OnFinished(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            OnFinished(command, interceptionContext);
        }
    }
}
