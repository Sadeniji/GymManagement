using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId);
    Task RemoveSubscriptionAsync(Subscription subscription);
    Task<bool> SubscriptionExistsAsync(Guid id);
    Task<Subscription?> GetSubscriptionsByAdminIdAsync(Guid adminId);
    Task<List<Subscription>> ListSubscriptionsAsync();
    Task UpdateAsync(Subscription subscription);
}   