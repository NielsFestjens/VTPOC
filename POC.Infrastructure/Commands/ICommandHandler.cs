using System.Threading.Tasks;

namespace POC.Infrastructure.Commands
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command);
    }
}