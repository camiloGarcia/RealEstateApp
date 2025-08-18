using AutoMapper;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Mapping;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyDto>();
        CreateMap<PropertyDto, Property>();
    }
}
