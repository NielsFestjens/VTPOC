using System.Threading.Tasks;

namespace POC.Infrastructure.Requests
{
    public interface IRequestHandler<TRequest, TResult>
    {
        Task<TResult> Handle(TRequest request);
    }
}