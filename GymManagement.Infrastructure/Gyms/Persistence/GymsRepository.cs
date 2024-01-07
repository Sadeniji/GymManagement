﻿using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.Gyms.AddAsync(gym);
    }

    public async Task<Gym?> GetGymByIdAsync(Guid id)
    {
        return await _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
    }

    public async Task<bool> GymExistsAsync(Guid id)
    {
        return await _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
    }

    public async Task<List<Gym>> ListGymsBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _dbContext
            .Gyms
            .Where(gym => gym.SubscriptionId == subscriptionId)
            .ToListAsync();
    }

    public Task UpdateGymAsync(Gym gym)
    {
        _dbContext.Update(gym);
        return Task.CompletedTask;
    }

    public Task RemoveGymAsync(Gym gym)
    {
        _dbContext.Remove(gym);
        return Task.CompletedTask;
    }

    public Task RemoveGymsRangeAsync(List<Gym> gyms)
    {
        _dbContext.RemoveRange(gyms);
        return Task.CompletedTask;
    }
}