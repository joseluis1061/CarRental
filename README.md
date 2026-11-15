# CleanArchitecture

Proyecto de aprendizaje en **C# / .NET 10** que aplica **Clean Architecture** y **Domain-Driven Design (DDD)** sobre un dominio de **alquiler de vehículos**.

> Estado actual: están implementadas las capas **Domain** y **Application**. Las capas de Infrastructure y API aún no existen.

## Estructura

```
ClenArchitecture.slnx
src/CleanArchitecture/
├── CleanArchitecture.Domain        # Núcleo del negocio (sin dependencias de infraestructura)
└── CleanArchitecture.Application   # Casos de uso (referencia a Domain)
```

La dependencia va siempre hacia adentro: `Application → Domain`.

### Domain

| Carpeta | Contenido |
|---|---|
| `Abstractions` | `Entity` (con eventos de dominio), `Result`, `Error`, `IDomainEvent`, `IUnitOfWork` |
| `Alquileres` | Agregado `Alquiler`, `DateRange`, `AlquilerStatus`, `PrecioDetalle`, `PrecioService`, errores y eventos |
| `Vehiculos` | `Vehiculo`, `Vin`, `Modelo`, `Direccion`, `Accesorio` |
| `Users` | `User`, `Name`, `Apellido`, `Email` y su evento de creación |
| `Reviews` | `Review`, `Rating`, `Comentario`, errores y eventos |
| `Shared` | Value objects `Moneda` y `TipoMoneda` |

Conceptos aplicados:
- **Entidades y agregados** con constructor privado y métodos de fábrica (`Alquiler.Reservar`).
- **Value objects** (`Moneda`, `DateRange`, `Email`, `Vin`, ...).
- **Eventos de dominio** (`AlquilerReservado`, `AlquilerConfirmado`, `AlquilerRechazado`, `AlquilerCancelado`, `AlquilerCompletado`, ...).
- **Patrón Result** para devolver errores de negocio sin lanzar excepciones.
- **Servicio de dominio** `PrecioService`: calcula el precio total a partir de los días, el mantenimiento y un recargo por accesorios.
- **Repositorios** definidos como interfaces (`IAlquilerRepository`, `IUserRepository`, `IVehiculoRepository`) y `IUnitOfWork`.

### Ciclo de vida de un alquiler

```
Reservado ──► Confirmado ──► Completado
    │             │
    ▼             ▼
Rechazado     Cancelado   (solo antes de que inicie el periodo)
```

### Application

- Abstracciones CQRS sobre **MediatR**: `ICommand`, `ICommandHandler`, `IQuery`, `IQueryHandler`.
- `DependencyInjection.AddApplication()` registra los handlers de MediatR y `PrecioService`.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Uso

```bash
dotnet restore
dotnet build
```

Para usar la capa Application desde una API o aplicación host:

```csharp
builder.Services.AddApplication();
```

## Tecnologías

- .NET 10 / C#
- MediatR 14 (y MediatR.Contracts en Domain)

## Hoja de ruta

- [ ] Capa Infrastructure (EF Core, repositorios, Unit of Work)
- [ ] Capa API (endpoints / controllers)
- [ ] Commands y queries en Application
- [ ] Pruebas unitarias del dominio
