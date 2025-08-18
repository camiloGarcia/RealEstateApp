using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(string idOwner);
    Task<Owner> CreateAsync(Owner owner);
    Task<Owner?> UpdateAsync(string idOwner, Owner owner);
    Task<bool> DeleteAsync(string idOwner);
}
