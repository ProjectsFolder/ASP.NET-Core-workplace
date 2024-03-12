using System.Text.RegularExpressions;
using ConsoleClient.Commands;

namespace ConsoleClient.Services
{
    internal sealed class CommandService
    {
        private readonly Dictionary<string, CommandBase> commands = [];

        public void AddCommand(CommandBase command)
        {
            if (command.GetCommand() == string.Empty)
            {
                throw new ArgumentException("Command name not found");
            }

            commands.Add(command.GetCommand(), command);
        }

        public void Execute(string command)
        {
            var commandPattern = @"^[^\s]+";
            var commandMatch = Regex.Match(command, commandPattern, RegexOptions.IgnoreCase);
            var resultCommand = commandMatch.ToString();

            var args = new Dictionary<string, string>();
            var argsPattern = @"([a-zA-Z]+)=([a-zA-Z0-9_]+)";
            var matches = Regex.Matches(command, argsPattern, RegexOptions.IgnoreCase);
            foreach (var match in matches.Cast<Match>())
            {
                args.Add(match.Groups[1].ToString(), match.Groups[2].ToString());
            }

            if (!commands.TryGetValue(resultCommand, out CommandBase? execute))
            {
                Console.WriteLine($"Command \"{resultCommand}\" not found");

                return;
            }

            try
            {
                var result = execute.Execute(args).Result;
                if (result != Result.Success)
                {
                    Console.WriteLine($"Command \"{resultCommand}\" not executed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
