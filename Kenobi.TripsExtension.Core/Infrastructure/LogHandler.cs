using System;
using System.Diagnostics;
using System.Net;
using Kenobi.Common.Metrics;
using Kenobi.Frameworks.Logging;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.Core.Infrastructure
{
    public class LogHandler : ILogHandler
    {
        public void InsertLog(LogModel log)
        {
            var entry = LogUtility.GetLogEntry();
            entry.Title = log.Title;
            entry.CallType = log.CallType;
            entry.ProviderId = log.ProviderId;
            entry.RequestObject = log.Request;
            entry.ResponseObject = log.Response;
            entry.TimeTaken = log.ResponseTime;
            entry.StatusType = log.Status;
            try
            {
                LogUtility.GetLogger().Write(entry, Constants.ServiceLevel);
            }
            catch (Exception ex)
            {
                try
                {
                    var meter = Metering.GetGlobalMeter();
                    meter.Meter(Constants.TripsExtensionLoggingFaultCount);

                    if (!EventLog.SourceExists(Constants.ApplicationName))
                        EventLog.CreateEventSource(Constants.ApplicationName, Constants.EventLogName);
                    EventLog.WriteEntry(Constants.ApplicationName, JsonConvert.SerializeObject(ex), EventLogEntryType.Error);
                    EventLog.WriteEntry(Constants.ApplicationName + "-" + log.CallType + "-Request", JsonConvert.SerializeObject(log.Request), EventLogEntryType.Error);
                    EventLog.WriteEntry(Constants.ApplicationName + "-" + log.CallType + "-Response", JsonConvert.SerializeObject(log.Response), EventLogEntryType.Error);
                }
                catch (Exception)
                {
                    //Do Nothing
                }
            }
        }

        public void LogException(Exception ex)
        {
            try
            {
                LogUtility.GetLogger().Write(ex.ToContextualEntry(), Constants.LogOnlyPolicy);
            }
            catch (Exception e)
            {
                try
                {
                    var meter = Metering.GetGlobalMeter();
                    meter.Meter(Constants.TripsExtensionExceptionLoggingFaultCount);

                    if (!EventLog.SourceExists(Constants.ApplicationName))
                        EventLog.CreateEventSource(Constants.ApplicationName, Constants.EventLogName);
                    EventLog.WriteEntry(Constants.ApplicationName, JsonConvert.SerializeObject(ex), EventLogEntryType.Error);
                    EventLog.WriteEntry(Constants.ApplicationName, JsonConvert.SerializeObject(e), EventLogEntryType.Error);
                }
                catch (Exception)
                {
                    //Do Nothing
                }
            }
        }
    }
}