namespace CleanArchitecture.Domain.Reviews.Events;
using CleanArchitecture.Domain.Abstractions;

// Un evento de dominio es un hecho que ocurrió en el sistema.
// Aquí definimos el evento que representa la creación de un usuario.
public sealed record ReviewCreateDomainEvent(Guid ReviewId) : IDomainEvent;
