## RealEstate API

### Requisitos previos
- .NET 9
- MongoDB (local o remoto)

### Configuración de la base de datos
Solo debes agregar la cadena de conexión de MongoDB en el archivo `appsettings.json` del proyecto API, por ejemplo:

```json
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "RealEstateDb"
}
```

Al iniciar la API, la base de datos y las colecciones se crearán automáticamente si no existen. Además, se inicializarán datos de ejemplo y los índices necesarios.

### Base URL
`http://localhost:5010/api`

### Endpoints principales

#### GET /properties
Lista paginada y filtrada.

Query params:
- `name` (string, opcional)
- `address` (string, opcional)
- `minPrice` (decimal, opcional)
- `maxPrice` (decimal, opcional)
- `page` (int, default 1)
- `pageSize` (int, default 10)

Headers de respuesta:
- `X-Total-Count` total de elementos
- `X-Page` página actual
- `X-PageSize` tamaño de página

Response ejemplo:
```json
[
  {
    "id": "665e...",
    "idProperty": "P-001",
    "idOwner": "OWN-1",
    "name": "Modern House",
    "address": "123 Main St",
    "price": 250000,
    "codeInternal": "INT-001",
    "year": 2022,
    "imageUrl": "https://.../house.jpg"
  }
]
```

#### GET /properties/{id}
Obtiene detalle.

#### POST /properties
Crea propiedad.
Body (JSON):
```json
{
  "idOwner": "OWN-1",
  "name": "Modern House",
  "address": "123 Main St",
  "price": 250000,
  "codeInternal": "INT-001",
  "year": 2022,
  "imageUrl": "https://.../house.jpg"
}
```

#### PUT /properties/{id}
Actualiza campos de la propiedad.

#### DELETE /properties/{id}
Elimina propiedad.

### Errores
Formato genérico (middleware global):
```json
{
  "error": "Internal server error",
  "message": "...",
  "traceId": "..."
}
```

### Notas
- API usa camelCase.
- Filtros aplican regex case-insensitive en `name` y `address` y comparación numérica en `price`.
- La base de datos se inicializa automáticamente al iniciar la API si la cadena de conexión está configurada correctamente.
