using LibrarieModele.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelService.Abstraction
{
    public interface ISubscriptionsService
    {
        Task<List<SubscriptionDTO>> GetSubscriptions();
        Task<SubscriptionDTO> GetSubscription(int id);
        Task CreateSubscription(SubscriptionDTO subscriptionDTO);
        Task<SubscriptionDTO> UpdateSubscription(SubscriptionDTO subscriptionDTO, int id);
        Task DeleteSubscription(int id);
        Task<bool> SubscriptionExists(int id);
    }
}
