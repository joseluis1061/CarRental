namespace CleanArchitecture.Application.Abstraction.Messaging;

using CleanArchitecture.Domain.Abstractions;
using MediatR;

// Clase generica que representa una consulta (query) en el patrón CQRS.
// TResponse es el tipo de respuesta que se espera de la consulta.
// Implementa IRequest de MediatR, lo que permite que esta consulta sea manejada por un handler específico que devuelva un Result<TResponse>.
// El Result<TResponse> encapsula el resultado de la operación, incluyendo posibles errores o datos devueltos.
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}