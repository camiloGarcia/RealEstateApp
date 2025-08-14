using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DTOs;
using RealEstate.Domain.Interfaces;

namespace RealEstateAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var filter = new PropertyFilterDto
            {
                Name = name,
                Address = address,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Page = page,
                PageSize = pageSize
            };

            var properties = await _propertyService.GetFilteredPropertiesAsync(filter);
            var totalCount = await _propertyService.GetPropertiesCountAsync(filter);

            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Page", page.ToString());
            Response.Headers.Add("X-PageSize", pageSize.ToString());

            return Ok(properties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDto>> GetProperty(string id)
    {
        try
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound(new { error = "Property not found" });

            return Ok(property);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = await _propertyService.CreatePropertyAsync(createDto);
            return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyDto>> UpdateProperty(string id, [FromBody] UpdatePropertyDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = await _propertyService.UpdatePropertyAsync(id, updateDto);
            if (property == null)
                return NotFound(new { error = "Property not found" });

            return Ok(property);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProperty(string id)
    {
        try
        {
            var result = await _propertyService.DeletePropertyAsync(id);
            if (!result)
                return NotFound(new { error = "Property not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
}
