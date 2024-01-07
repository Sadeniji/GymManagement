using GymManagement.Domain.Admins;

namespace GymManagement.Application.Common.Interfaces;

public interface IAdminsRepository
{
    Task<Admin?> GetAdminByIdAsync(Guid adminId);
    Task UpdateAdminAsync(Admin admin);
}