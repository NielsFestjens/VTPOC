using System;
using System.Threading.Tasks;

namespace POC.Infrastructure.Requests
{
    public interface IRequestDispatcher
    {
        Task<TResult> Dispatch<TRequest, TResult>(TRequest request);
    }

    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> Dispatch<TRequest, TResult>(TRequest request)
        {
            var requestHandler = (IRequestHandler<TRequest, TResult>)_serviceProvider.GetService(typeof(IRequestHandler<TRequest, TResult>));
            return requestHandler.Handle(request);
        }
    }
}