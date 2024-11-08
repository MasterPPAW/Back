using AutoMapper;

using LibrarieModele;
using LibrarieModele.DTOs;
using Microsoft.EntityFrameworkCore;

using Repository_CodeFirst;

namespace NivelAccesDate.Accessors
{
    public class SubscriptionsAccessor
    {
        private readonly FitnessDBContext _context;
        private readonly IMapper _mapper;
        public SubscriptionsAccessor(FitnessDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SubscriptionDTO>> GetSubscriptions()
        {
            var subs = await _context.Subscriptions.ToListAsync();

            return subs.Select(ent => _mapper.Map<SubscriptionDTO>(ent)).ToList();
        }

        public async Task<SubscriptionDTO> GetSubscription(int id)
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(m => m.SubscriptionId == id);

            return _mapper.Map<SubscriptionDTO>(sub);
        }

        public async Task CreateSubscription(SubscriptionDTO subscriptionDTO)
        {
            var toEntity = _mapper.Map<Subscription>(subscriptionDTO);

            await _context.Subscriptions.AddAsync(toEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscription(SubscriptionDTO subscriptionDTO)
        {
            var toEntity = _mapper.Map<Subscription>(subscriptionDTO);

            _context.Subscriptions.Update(toEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(int id)
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(m => m.SubscriptionId == id);
            if (sub != null)
            {
                _context.Subscriptions.Remove(sub);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> SubscriptionExistsAsync(int id)
        {
            return await _context.Subscriptions.AnyAsync(u => u.SubscriptionId == id);
        }
    }
}
