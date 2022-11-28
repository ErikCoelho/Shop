using Shop.Domain.Commands.Contracts;

namespace Shop.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
