using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
    Task<Gym?> GetGymByIdAsync(Guid id);
    Task<bool> GymExistsAsync(Guid id);
    Task<List<Gym>> ListGymsBySubscriptionIdAsync(Guid subscriptionId);
    Task UpdateGymAsync(Gym gym);
    Task RemoveGymAsync(Gym gym);
    Task RemoveGymsRangeAsync(List<Gym> gyms);
}