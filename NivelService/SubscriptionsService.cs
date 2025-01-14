
using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;
using NivelService.Abstraction;
using Microsoft.Extensions.Logging;
using NLog;

namespace NivelService
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionsAccessor _subscriptionsAccessor;
        private readonly ILogger<SubscriptionsService> _logger;

        public SubscriptionsService(IMapper mapper, ISubscriptionsAccessor subscriptionsAccessor, ILogger<SubscriptionsService> logger)
        {
            _mapper = mapper;
            _subscriptionsAccessor = subscriptionsAccessor;
            _logger = logger;
        }

        public async Task<List<SubscriptionDTO>> GetSubscriptions()
        {
            _logger.LogInformation("Fetching all subscriptions.");
            var subscriptions = await _subscriptionsAccessor.GetSubscriptions();

            _logger.LogInformation("Fetched {Count} subscriptions.", subscriptions.Count);
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
            _logger.LogInformation("Creating a new subscription for UserId {UserId}.", subscriptionDTO.UserId);
            var toEntity = _mapper.Map<Subscription>(subscriptionDTO);

            var createdSubscription = await _subscriptionsAccessor.CreateSubscription(toEntity);
            var oldSubs = await _subscriptionsAccessor.GetByUserId(createdSubscription.UserId);
            await this.DeleteSubscription(oldSubs.SubscriptionId);

            _logger.LogInformation("Created subscription with Id {Id}.", createdSubscription.SubscriptionId);
            return _mapper.Map<SubscriptionDTO>(createdSubscription);
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
            _logger.LogInformation("Attempting to delete subscription with Id {Id}.", id);

            var subscription = await _subscriptionsAccessor.GetSubscription(id);
            if (subscription == null)
            {
                _logger.LogWarning("Subscription with Id {Id} not found for deletion.", id);
                throw new ArgumentException("Subscription not found.");
            }

            await _subscriptionsAccessor.DeleteSubscription(id);
            _logger.LogInformation("Deleted subscription with Id {Id}.", id);
        }

        public async Task<bool> SubscriptionExists(int id)
        {
            return await _subscriptionsAccessor.SubscriptionExists(id);
        }
    }
}
