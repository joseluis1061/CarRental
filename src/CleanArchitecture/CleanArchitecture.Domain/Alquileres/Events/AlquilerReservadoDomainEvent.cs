namespace CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Abstractions;

// Un evento de dominio es un hecho que ocurrió en el sistema.
// Aquí definimos el evento que representa la creación de un Alquiler.
public sealed record AlquilerReservadoDomainEvent(Guid AlquilerId) : IDomainEvent;
