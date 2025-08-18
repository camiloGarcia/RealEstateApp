using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IOwnerService
{
    Task<IEnumerable<OwnerDto>> GetAllOwnersAsync();
    Task<OwnerDto?> GetOwnerByIdAsync(string idOwner);
    Task<OwnerDto> CreateOwnerAsync(CreateOwnerDto createDto);
    Task<OwnerDto?> UpdateOwnerAsync(string idOwner, UpdateOwnerDto updateDto);
    Task<bool> DeleteOwnerAsync(string idOwner);
}
