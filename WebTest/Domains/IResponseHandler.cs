namespace WebTest.Domains
{
    public interface IResponseHandler<TResponse> : IHandler
        where TResponse : class
    {
        public TResponse Handle();
    }
}
