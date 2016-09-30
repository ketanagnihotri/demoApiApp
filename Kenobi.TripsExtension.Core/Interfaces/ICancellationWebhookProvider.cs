using System.Collections.Generic;
using System.Threading.Tasks;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;

namespace Kenobi.TripsExtension.Core.Interfaces
{
    public interface ICancellationWebhookProvider
    {
        Task<WebhooksResponse> InitCancellationWebhook(WebhooksRequest request, Dictionary<string, string> headers);
    }
}