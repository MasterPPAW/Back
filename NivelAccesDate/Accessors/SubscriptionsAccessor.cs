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
            return await _context.Subscriptions.Where(m => m.IsDeleted == false).ToListAsync();
        }

        public async Task<Subscription> GetSubscription(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(m => m.SubscriptionId == id && m.IsDeleted == false);
        }

        public async Task<Subscription> GetByUserId(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(m => m.UserId == id && m.IsDeleted == false);
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task UpdateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(m => m.SubscriptionId == id && m.IsDeleted == false);
            if (subscription != null)
            {
                //_context.Subscriptions.Remove(subscription);
                subscription.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> SubscriptionExists(int id)
        {
            return await _context.Subscriptions.AnyAsync(u => u.SubscriptionId == id);
        }
    }
}
