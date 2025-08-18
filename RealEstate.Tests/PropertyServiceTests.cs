using NUnit.Framework;
using Moq;
using RealEstate.Application.Services;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Tests;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _mockRepository;
    private PropertyService _propertyService;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IPropertyRepository>();
        _propertyService = new PropertyService(_mockRepository.Object);
    }

    [Test]
    public async Task GetAllPropertiesAsync_ShouldReturnMappedDtos()
    {
        // Arrange
        var properties = new List<Property>
        {
            new Property
            {
                Id = "1",
                IdProperty = "p1",
                IdOwner = "owner1",
                Name = "Test Property 1",
                Address = "123 Test St",
                Price = 250000,
                ImageUrl = "test1.jpg"
            },
            new Property
            {
                Id = "2",
                IdProperty = "p2",
                IdOwner = "owner2",
                Name = "Test Property 2",
                Address = "456 Test Ave",
                Price = 350000,
                ImageUrl = "test2.jpg"
            }
        };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(properties);

        // Act
        var result = await _propertyService.GetAllPropertiesAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    Assert.That(result.First().Id, Is.EqualTo("1"));
        Assert.That(result.First().Name, Is.EqualTo("Test Property 1"));
        Assert.That(result.First().Price, Is.EqualTo(250000));
    }

    [Test]
    public async Task GetPropertyByIdAsync_WithValidId_ShouldReturnProperty()
    {
        // Arrange
        var property = new Property
        {
            Id = "1",
            IdProperty = "p1",
            IdOwner = "owner1",
            Name = "Test Property",
            Address = "123 Test St",
            Price = 250000,
            ImageUrl = "test.jpg"
        };

        _mockRepository.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(property);

        // Act
        var result = await _propertyService.GetPropertyByIdAsync("1");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo("1"));
        Assert.That(result.Name, Is.EqualTo("Test Property"));
    }

    [Test]
    public async Task GetPropertyByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync("invalid")).ReturnsAsync((Property?)null);

        // Act
        var result = await _propertyService.GetPropertyByIdAsync("invalid");

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreatePropertyAsync_ShouldReturnCreatedProperty()
    {
        // Arrange
        var createDto = new CreatePropertyDto
        {
            IdOwner = "owner1",
            Name = "New Property",
            Address = "789 New St",
            Price = 400000,
            CodeInternal = "C-001",
            Year = 2024,
            ImageUrl = "new.jpg"
        };

        var createdProperty = new Property
        {
            Id = "new-id",
            IdProperty = "np1",
            IdOwner = "owner1",
            Name = "New Property",
            Address = "789 New St",
            Price = 400000,
            CodeInternal = "C-001",
            Year = 2024,
            ImageUrl = "new.jpg"
        };

        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Property>())).ReturnsAsync(createdProperty);

        // Act
        var result = await _propertyService.CreatePropertyAsync(createDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo("new-id"));
        Assert.That(result.Name, Is.EqualTo("New Property"));
        Assert.That(result.Price, Is.EqualTo(400000));
    }

    [Test]
    public async Task UpdatePropertyAsync_WithValidId_ShouldReturnUpdatedProperty()
    {
        // Arrange
        var updateDto = new UpdatePropertyDto
        {
            Name = "Updated Property",
            Address = "Updated Address",
            Price = 500000,
            CodeInternal = "C-002",
            Year = 2025,
            ImageUrl = "updated.jpg"
        };

        var existingProperty = new Property
        {
            Id = "1",
            IdProperty = "p1",
            IdOwner = "owner1",
            Name = "Old Name",
            Address = "Old Address",
            Price = 250000,
            CodeInternal = "C-001",
            Year = 2024,
            ImageUrl = "old.jpg"
        };

        var updatedProperty = new Property
        {
            Id = "1",
            IdProperty = "p1",
            IdOwner = "owner1",
            Name = "Updated Property",
            Address = "Updated Address",
            Price = 500000,
            CodeInternal = "C-002",
            Year = 2025,
            ImageUrl = "updated.jpg"
        };

        _mockRepository.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(existingProperty);
        _mockRepository.Setup(r => r.UpdateAsync("1", It.IsAny<Property>())).ReturnsAsync(updatedProperty);

        // Act
        var result = await _propertyService.UpdatePropertyAsync("1", updateDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Updated Property"));
        Assert.That(result.Price, Is.EqualTo(500000));
    }

    [Test]
    public async Task UpdatePropertyAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var updateDto = new UpdatePropertyDto
        {
            Name = "Updated Property",
            Address = "Updated Address",
            Price = 500000,
            CodeInternal = "C-002",
            Year = 2025,
            ImageUrl = "updated.jpg"
        };

        _mockRepository.Setup(r => r.GetByIdAsync("invalid")).ReturnsAsync((Property?)null);

        // Act
        var result = await _propertyService.UpdatePropertyAsync("invalid", updateDto);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeletePropertyAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync("1")).ReturnsAsync(true);

        // Act
        var result = await _propertyService.DeletePropertyAsync("1");

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeletePropertyAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync("invalid")).ReturnsAsync(false);

        // Act
        var result = await _propertyService.DeletePropertyAsync("invalid");

        // Assert
        Assert.That(result, Is.False);
    }
}
