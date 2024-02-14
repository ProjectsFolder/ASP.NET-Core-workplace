namespace WebTest.Domains
{
    public interface IHandler<Request, Response>
        where Request : class
        where Response : class
    {
        public Response? Handle(Request? dto);
    }
}
