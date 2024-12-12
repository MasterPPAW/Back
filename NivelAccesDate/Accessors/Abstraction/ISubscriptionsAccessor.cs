
using LibrarieModele;

namespace NivelAccesDate.Accessors.Abstraction
{
    public interface ISubscriptionsAccessor
    {
        Task<List<Subscription>> GetSubscriptions();
        Task<Subscription> GetSubscription(int id);
        Task<Subscription> GetByUserId(int id);
        Task<Subscription> CreateSubscription(Subscription subscription);
        Task UpdateSubscription(Subscription subscription);
        Task DeleteSubscription(int id);
        Task<bool> SubscriptionExists(int id);
    }
}
