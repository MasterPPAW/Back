using LibrarieModele;
using Microsoft.EntityFrameworkCore;

using NivelAccesDate.Accessors.Abstraction;

using Repository_CodeFirst;

namespace NivelAccesDate.Accessors
{
    public class SubscriptionsAccessor : ISubscriptionsAccessor
    {
        private readonly FitnessDBContext _context;

        public SubscriptionsAccessor(FitnessDBContext context)
        {
            _context = context;
        }

        public async Task<List<Subscription>> GetSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscription(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(m => m.SubscriptionId == id);
        }

        public async Task CreateSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(m => m.SubscriptionId == id);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> SubscriptionExists(int id)
        {
            return await _context.Subscriptions.AnyAsync(u => u.SubscriptionId == id);
        }
    }
}
