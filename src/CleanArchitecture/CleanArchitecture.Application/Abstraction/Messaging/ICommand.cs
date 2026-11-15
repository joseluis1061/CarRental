namespace CleanArchitecture.Application.Abstraction.Messaging;
using MediatR;
using CleanArchitecture.Domain.Abstractions;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand : IRequest<Result>
{
}