
using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;
using NivelService.Abstraction;

namespace NivelService
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionsAccessor _subscriptionsAccessor;

        public SubscriptionsService(IMapper mapper, ISubscriptionsAccessor subscriptionsAccessor)
        {
            _mapper = mapper;
            _subscriptionsAccessor = subscriptionsAccessor;
        }

        public async Task<List<SubscriptionDTO>> GetSubscriptions()
        {
            var subscriptions = await _subscriptionsAccessor.GetSubscriptions();

            return subscriptions.Select(ent => _mapper.Map<SubscriptionDTO>(ent)).ToList();
        }

        public async Task<SubscriptionDTO> GetSubscription(int id)
        {
            return _mapper.Map<SubscriptionDTO>(await _subscriptionsAccessor.GetSubscription(id));
        }

        public async Task<SubscriptionDTO> GetByUserId(int id)
        {
            return _mapper.Map<SubscriptionDTO>(await _subscriptionsAccessor.GetByUserId(id));
        }

        public async Task<SubscriptionDTO> CreateSubscription(SubscriptionDTO subscriptionDTO)
        {
            var toEntity = _mapper.Map<Subscription>(subscriptionDTO);

            return _mapper.Map<SubscriptionDTO>(await _subscriptionsAccessor.CreateSubscription(toEntity));
        }

        public async Task<SubscriptionDTO> UpdateSubscription(SubscriptionDTO subscriptionDTO, int id)
        {
            var foundSubscription = await _subscriptionsAccessor.GetSubscription(id);
            if (foundSubscription == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(subscriptionDTO, foundSubscription);

            await _subscriptionsAccessor.UpdateSubscription(foundSubscription);

            return await GetSubscription(id);
        }

        public async Task DeleteSubscription(int id)
        {
            await _subscriptionsAccessor.DeleteSubscription(id);
        }

        public async Task<bool> SubscriptionExists(int id)
        {
            return await _subscriptionsAccessor.SubscriptionExists(id);
        }
    }
}
