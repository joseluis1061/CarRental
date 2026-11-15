namespace CleanArchitecture.Application.Abstraction.Messaging;
using CleanArchitecture.Domain.Abstractions;
using MediatR;

// Clase generica que representa un manejador de consultas (query handler) en el patrón CQRS.
// TQuery es el tipo de consulta que este handler manejará, y debe implementar IQuery<TResponse>.
// TResponse es el tipo de respuesta que se espera de la consulta.
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}