using CleanStore.Application.SharedContext.Results;
using MediatR;

namespace CleanStore.Application.SharedContext.UseCases.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<ICommand, Result>
{
    
}