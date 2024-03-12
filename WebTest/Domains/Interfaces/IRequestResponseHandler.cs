using WebTest.Dto;
using WebTest.Http.Responses;

namespace WebTest.Domains.Interfaces
{
    public interface IRequestResponseHandler<TRequest, TResponse> : IHandler
        where TRequest : CommandBase
        where TResponse : class
    {
        public SuccessDto<TResponse> Handle(TRequest dto);
    }
}
