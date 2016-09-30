using System;
using Kenobi.TripsExtension.Entities;

namespace Kenobi.TripsExtension.Core.Interfaces
{
    public interface ILogHandler
    {
        void InsertLog(LogModel log);
        void LogException(Exception ex);
    }
}