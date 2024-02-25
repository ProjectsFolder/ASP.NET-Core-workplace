namespace WebTest.Domains.Interfaces
{
    public interface IResponseHandler<TResponse> : IHandler
        where TResponse : class
    {
        public TResponse Handle();
    }
}
