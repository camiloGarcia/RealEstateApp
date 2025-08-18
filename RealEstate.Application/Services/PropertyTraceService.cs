using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyTraceService : IPropertyTraceService
{
    private readonly IPropertyTraceRepository _propertyTraceRepository;

    public PropertyTraceService(IPropertyTraceRepository propertyTraceRepository)
    {
        _propertyTraceRepository = propertyTraceRepository;
    }

    public async Task<IEnumerable<PropertyTraceDto>> GetAllPropertyTracesAsync()
    {
        var traces = await _propertyTraceRepository.GetAllAsync();
        return traces.Select(trace => new PropertyTraceDto
        {
            IdPropertyTrace = trace.IdPropertyTrace,
            DateSale = trace.DateSale,
            Name = trace.Name,
            Value = trace.Value,
            Tax = trace.Tax,
            IdProperty = trace.IdProperty
        });
    }

    public async Task<IEnumerable<PropertyTraceDto>> GetPropertyTracesByPropertyIdAsync(string idProperty)
    {
        var traces = await _propertyTraceRepository.GetByPropertyIdAsync(idProperty);
        return traces.Select(trace => new PropertyTraceDto
        {
            IdPropertyTrace = trace.IdPropertyTrace,
            DateSale = trace.DateSale,
            Name = trace.Name,
            Value = trace.Value,
            Tax = trace.Tax,
            IdProperty = trace.IdProperty
        });
    }

    public async Task<PropertyTraceDto?> GetPropertyTraceByIdAsync(string idPropertyTrace)
    {
        var trace = await _propertyTraceRepository.GetByIdAsync(idPropertyTrace);
        if (trace == null) return null;

        return new PropertyTraceDto
        {
            IdPropertyTrace = trace.IdPropertyTrace,
            DateSale = trace.DateSale,
            Name = trace.Name,
            Value = trace.Value,
            Tax = trace.Tax,
            IdProperty = trace.IdProperty
        };
    }

    public async Task<PropertyTraceDto> CreatePropertyTraceAsync(CreatePropertyTraceDto createDto)
    {
        var propertyTrace = new PropertyTrace
        {
            IdPropertyTrace = Guid.NewGuid().ToString(),
            DateSale = createDto.DateSale,
            Name = createDto.Name,
            Value = createDto.Value,
            Tax = createDto.Tax,
            IdProperty = createDto.IdProperty
        };

        var createdTrace = await _propertyTraceRepository.CreateAsync(propertyTrace);
        
        return new PropertyTraceDto
        {
            IdPropertyTrace = createdTrace.IdPropertyTrace,
            DateSale = createdTrace.DateSale,
            Name = createdTrace.Name,
            Value = createdTrace.Value,
            Tax = createdTrace.Tax,
            IdProperty = createdTrace.IdProperty
        };
    }

    public async Task<PropertyTraceDto?> UpdatePropertyTraceAsync(string idPropertyTrace, UpdatePropertyTraceDto updateDto)
    {
        var existingTrace = await _propertyTraceRepository.GetByIdAsync(idPropertyTrace);
        if (existingTrace == null) return null;

        var updatedTrace = new PropertyTrace
        {
            IdPropertyTrace = existingTrace.IdPropertyTrace,
            DateSale = updateDto.DateSale,
            Name = updateDto.Name,
            Value = updateDto.Value,
            Tax = updateDto.Tax,
            IdProperty = existingTrace.IdProperty
        };

        var result = await _propertyTraceRepository.UpdateAsync(idPropertyTrace, updatedTrace);
        if (result == null) return null;

        return new PropertyTraceDto
        {
            IdPropertyTrace = result.IdPropertyTrace,
            DateSale = result.DateSale,
            Name = result.Name,
            Value = result.Value,
            Tax = result.Tax,
            IdProperty = result.IdProperty
        };
    }

    public async Task<bool> DeletePropertyTraceAsync(string idPropertyTrace)
    {
        return await _propertyTraceRepository.DeleteAsync(idPropertyTrace);
    }
}
