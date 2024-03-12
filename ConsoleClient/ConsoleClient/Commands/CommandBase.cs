namespace ConsoleClient.Commands
{
    internal abstract class CommandBase
    {
        public abstract Task<Result> Execute(Dictionary<string, string>? args = null);

        public abstract string GetCommand();

        protected void Print(string message)
        {
            var command = GetCommand();
            var time = DateTime.Now;
            Console.WriteLine($"[{time}] {command}: {message}");
        }
    }

    public enum Result
    {  
        Success = 0,
        Error = 1,
    }
}
