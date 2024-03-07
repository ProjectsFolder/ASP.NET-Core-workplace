// See https://aka.ms/new-console-template for more information
using ConsoleClient.Commands;
using ConsoleClient.Services;

CommandService commands = new();
commands.AddCommand(new TestCommand());
commands.AddCommand(new MigrateCommand());

Console.WriteLine("Press (x) to exit...");

while (true)
{
    Console.WriteLine("Enter command:");
    var command = Console.ReadLine();
    if (command == null || command == "")
    {
        continue;
    }

    if (command[..1].Equals("x", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    commands.Execute(command);
}

Console.WriteLine("Application exited.");
