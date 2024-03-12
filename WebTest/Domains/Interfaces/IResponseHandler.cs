using WebTest.Http.Responses;

namespace WebTest.Domains.Interfaces
{
    public interface IResponseHandler<TResponse> : IHandler
        where TResponse : class
    {
        public SuccessDto<TResponse> Handle();
    }
}
