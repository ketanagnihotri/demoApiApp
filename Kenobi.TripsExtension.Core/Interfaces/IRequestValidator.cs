using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;

namespace kenobi.TripsExtension.Interfaces
{
    public interface IRequestValidator
    {
        ResponseStatus ValidateWebHookRequest(WebhooksRequest request);
    }
}