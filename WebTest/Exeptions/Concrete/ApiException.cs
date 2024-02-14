namespace WebTest.Exeptions.Concrete
{
    public class ApiException(string message, int status) : Exception(message)
    {
        public int Status { get; set; } = status;
    }
}
