namespace WebTest.Domains.Interfaces
{
    public interface IRequestResponseHandler<TRequest, TResponse> : IHandler
        where TRequest : class
        where TResponse : class
    {
        public TResponse Handle(TRequest dto);
    }
}
