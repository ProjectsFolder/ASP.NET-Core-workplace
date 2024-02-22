namespace WebTest.Domains
{
    public interface IRequestHandler<TRequest> : IHandler
        where TRequest : class
    {
        public void Handle(TRequest dto);
    }
}
