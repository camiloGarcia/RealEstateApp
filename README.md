# 🏠 Real Estate Application

Aplicación completa de bienes raíces con backend en .NET 9 y frontend en React.

## 🚀 Inicio Rápido

### Opción 1: Script Automático (Recomendado)
```powershell
# En PowerShell
.\start-app.ps1

# O en CMD
start-app.bat
```

### Opción 2: Inicio Manual

#### 1. Iniciar Backend
```bash
cd RealEstateAPI
dotnet run --urls "http://localhost:5010;https://localhost:7002"
```

#### 2. Iniciar Frontend (en otra terminal)
```bash
cd real-estate-frontend
npm start
```

## 📍 URLs de Acceso

- **Frontend**: http://localhost:3000
- **Backend HTTP**: http://localhost:5010
- **Backend HTTPS**: https://localhost:7002
- **API Properties**: http://localhost:5010/api/properties

## 🛠️ Tecnologías Utilizadas

### Backend
- .NET 9
- C#
- MongoDB
- Clean Architecture

### Frontend
- React 18
- TypeScript
- Axios
- CSS Modules

## 📁 Estructura del Proyecto

```
RealEstateApp/
├── RealEstateAPI/          # API .NET
├── RealEstate.Domain/      # Entidades y DTOs
├── RealEstate.Application/ # Servicios de aplicación
├── RealEstate.Infrastructure/ # Repositorios y MongoDB
├── real-estate-frontend/   # Aplicación React
└── start-app.ps1          # Script de inicio automático
```

## 🔧 Requisitos Previos

- .NET 9 SDK
- Node.js 18+
- MongoDB
- PowerShell (para el script automático)

## 📝 Características

- ✅ Lista de propiedades
- ✅ Filtros por nombre, dirección y precio
- ✅ Vista detallada de propiedades
- ✅ API RESTful
- ✅ Arquitectura limpia
- ✅ Base de datos MongoDB
- ✅ Frontend responsivo

## 🧪 Testing

```bash
# Backend
cd RealEstate.Tests
dotnet test

# Frontend
cd real-estate-frontend
npm test
```

## 📚 Documentación

- [API Documentation](RealEstateAPI/README.md)
- [Frontend Documentation](real-estate-frontend/README.md)
- [Database Schema](RealEstate.Infrastructure/README.md)
