using System;
using System.Threading.Tasks;
using POC.Infrastructure.Commands;

namespace POC.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task Dispatch<TCommand>(TCommand command);
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Dispatch<TCommand>(TCommand command)
        {
            var requestHandler = (ICommandHandler<TCommand>)_serviceProvider.GetService(typeof(ICommandHandler<TCommand>));
            return requestHandler.Handle(command);
        }
    }
}