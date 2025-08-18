using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IPropertyTraceService
{
    Task<IEnumerable<PropertyTraceDto>> GetAllPropertyTracesAsync();
    Task<IEnumerable<PropertyTraceDto>> GetPropertyTracesByPropertyIdAsync(string idProperty);
    Task<PropertyTraceDto?> GetPropertyTraceByIdAsync(string idPropertyTrace);
    Task<PropertyTraceDto> CreatePropertyTraceAsync(CreatePropertyTraceDto createDto);
    Task<PropertyTraceDto?> UpdatePropertyTraceAsync(string idPropertyTrace, UpdatePropertyTraceDto updateDto);
    Task<bool> DeletePropertyTraceAsync(string idPropertyTrace);
}
