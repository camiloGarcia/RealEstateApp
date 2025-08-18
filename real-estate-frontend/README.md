# Real Estate Frontend

Este proyecto es la interfaz web para la aplicación de gestión de propiedades inmobiliarias. Permite visualizar, filtrar y consultar detalles de propiedades almacenadas en el sistema.

## Tecnologías
- ReactJS
- TypeScript
- Axios (para consumo de API)
- CSS/Styled Components
- Testing Library (para pruebas unitarias)

## Instalación
1. Ve al directorio del frontend:
   ```bash
   cd real-estate-frontend
   ```
2. Instala las dependencias:
   ```bash
   npm install
   ```

## Ejecución
Para iniciar la aplicación en modo desarrollo:
```bash
npm start
```
La aplicación estará disponible en `http://localhost:3000`.

## Configuración de API
El frontend está configurado para consumir la API en `http://localhost:5010/api`. Asegúrate de que el backend esté corriendo y que CORS esté habilitado.

## Pruebas
Para ejecutar las pruebas unitarias:
```bash
npm test
```

## Funcionalidades
- Listado de propiedades
- Filtros por nombre, dirección y rango de precio
- Vista de detalles de propiedad
- Diseño responsivo

## Estructura de carpetas
- `src/components`: Componentes reutilizables
- `src/pages`: Páginas principales
- `src/services`: Lógica de consumo de API
- `src/types`: Tipos y modelos de datos

## Autor
Desarrollado para prueba técnica SR DEVELOPER FULLSTACK
