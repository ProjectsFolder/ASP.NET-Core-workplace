using WebTest.Dto;

namespace WebTest.Domains.Interfaces
{
    public interface IRequestHandler<TRequest> : IHandler
        where TRequest : CommandBase
    {
        public void Handle(TRequest dto);
    }
}
