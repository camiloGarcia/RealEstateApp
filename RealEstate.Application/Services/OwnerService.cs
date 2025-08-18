using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<OwnerDto>> GetAllOwnersAsync()
    {
        var owners = await _ownerRepository.GetAllAsync();
        return owners.Select(owner => new OwnerDto
        {
            IdOwner = owner.IdOwner,
            Name = owner.Name,
            Address = owner.Address,
            Photo = owner.Photo,
            Birthday = owner.Birthday
        });
    }

    public async Task<OwnerDto?> GetOwnerByIdAsync(string idOwner)
    {
        var owner = await _ownerRepository.GetByIdAsync(idOwner);
        if (owner == null) return null;

        return new OwnerDto
        {
            IdOwner = owner.IdOwner,
            Name = owner.Name,
            Address = owner.Address,
            Photo = owner.Photo,
            Birthday = owner.Birthday
        };
    }

    public async Task<OwnerDto> CreateOwnerAsync(CreateOwnerDto createDto)
    {
        var owner = new Owner
        {
            IdOwner = Guid.NewGuid().ToString(),
            Name = createDto.Name,
            Address = createDto.Address,
            Photo = createDto.Photo,
            Birthday = createDto.Birthday
        };

        var createdOwner = await _ownerRepository.CreateAsync(owner);
        
        return new OwnerDto
        {
            IdOwner = createdOwner.IdOwner,
            Name = createdOwner.Name,
            Address = createdOwner.Address,
            Photo = createdOwner.Photo,
            Birthday = createdOwner.Birthday
        };
    }

    public async Task<OwnerDto?> UpdateOwnerAsync(string idOwner, UpdateOwnerDto updateDto)
    {
        var existingOwner = await _ownerRepository.GetByIdAsync(idOwner);
        if (existingOwner == null) return null;

        var updatedOwner = new Owner
        {
            IdOwner = existingOwner.IdOwner,
            Name = updateDto.Name,
            Address = updateDto.Address,
            Photo = updateDto.Photo,
            Birthday = updateDto.Birthday
        };

        var result = await _ownerRepository.UpdateAsync(idOwner, updatedOwner);
        if (result == null) return null;

        return new OwnerDto
        {
            IdOwner = result.IdOwner,
            Name = result.Name,
            Address = result.Address,
            Photo = result.Photo,
            Birthday = result.Birthday
        };
    }

    public async Task<bool> DeleteOwnerAsync(string idOwner)
    {
        return await _ownerRepository.DeleteAsync(idOwner);
    }
}
