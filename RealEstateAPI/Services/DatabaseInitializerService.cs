using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealEstate.Domain.Interfaces;

namespace RealEstateAPI.Services;

public class DatabaseInitializerService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializerService> _logger;

    public DatabaseInitializerService(IServiceProvider serviceProvider, ILogger<DatabaseInitializerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando inicialización de la base de datos...");

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var propertyRepository = scope.ServiceProvider.GetRequiredService<IPropertyRepository>();
            var dbContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

            // Asegurar índices necesarios para filtros
            await MongoIndexInitializer.EnsureIndexesAsync(dbContext);

            // Verificar si ya existen propiedades
            var existingProperties = await propertyRepository.GetAllAsync();
            
            if (!existingProperties.Any())
            {
                _logger.LogInformation("No se encontraron propiedades. Creando datos de ejemplo...");
                await SeedSampleData(propertyRepository);
                _logger.LogInformation("Datos de ejemplo creados exitosamente.");
            }
            else
            {
                _logger.LogInformation($"La base de datos ya contiene {existingProperties.Count()} propiedades.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error durante la inicialización de la base de datos");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SeedSampleData(IPropertyRepository propertyRepository)
    {
        var sampleProperties = new List<Property>
        {
            new Property
            {
                IdOwner = "owner-001",
                Name = "Casa Moderna en Las Condes",
                Address = "Av. Apoquindo 1234, Las Condes, Santiago",
                Price = 85000000,
                ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800&h=600&fit=crop"
            },
            new Property
            {
                IdOwner = "owner-002",
                Name = "Departamento Premium en Providencia",
                Address = "Av. Providencia 5678, Providencia, Santiago",
                Price = 65000000,
                ImageUrl = "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?w=800&h=600&fit=crop"
            },
            new Property
            {
                IdOwner = "owner-003",
                Name = "Casa Familiar en Ñuñoa",
                Address = "Av. Grecia 9012, Ñuñoa, Santiago",
                Price = 95000000,
                ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800&h=600&fit=crop"
            },
            new Property
            {
                IdOwner = "owner-004",
                Name = "Loft Industrial en Bellavista",
                Address = "Constitución 3456, Bellavista, Santiago",
                Price = 45000000,
                ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800&h=600&fit=crop"
            },
            new Property
            {
                IdOwner = "owner-005",
                Name = "Casa de Campo en Pirque",
                Address = "Camino El Volcán 7890, Pirque, Región Metropolitana",
                Price = 120000000,
                ImageUrl = "https://images.unsplash.com/photo-1518780664697-55e3ad937233?w=800&h=600&fit=crop"
            }
        };

        foreach (var property in sampleProperties)
        {
            await propertyRepository.CreateAsync(property);
        }
    }
}
