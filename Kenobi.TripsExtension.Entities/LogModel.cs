using Tavisca.Frameworks.Logging.Infrastructure;

namespace Kenobi.TripsExtension.Entities
{
    public class LogModel
    {
        public string CallType;
        public int ProviderId;
        public object Request;
        public object Response;
        public double ResponseTime;
        public string SessionId;
        public string Title;
        public StatusOptions Status;
    }
}