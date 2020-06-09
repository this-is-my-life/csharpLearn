using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace csharpLearn
{
    class Program
    {
        private CommandService command;
        private DiscordSocketClient client;
        
        private static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            string[] config = File.ReadAllLines("config.txt");
            command = new CommandService();
            client = new DiscordSocketClient(); 

            client.Ready += Ready;

            await command.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            await client.LoginAsync(TokenType.Bot, config[0]);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Ready()
        {
            Console.WriteLine(client.CurrentUser + " is now online!");
            return Task.CompletedTask;
        }
    }
}