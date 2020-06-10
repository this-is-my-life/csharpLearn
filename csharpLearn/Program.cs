using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
// ReSharper disable InconsistentNaming

namespace csharpLearn
{
    class Program
    {
        private static CommandService command;
        private static DiscordSocketClient client;
        
        private static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var config = await File.ReadAllLinesAsync("config.txt");
            command = new CommandService();
            client = new DiscordSocketClient(); 

            client.Ready += Ready;
            client.MessageReceived += Message;

            await command.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            await client.LoginAsync(TokenType.Bot, config[0]);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private static Task Ready()
        {
            Console.WriteLine(client.CurrentUser + " is now online!");
            return Task.CompletedTask;
        }

        private static async Task Message(SocketMessage msg)
        {
            if (msg.Author.IsBot) return;

            if (!(msg is SocketUserMessage message)) return;

            var argPos = 0;
            if (!message.HasStringPrefix("c#", ref argPos)) return;
            
            var context = new SocketCommandContext(client, message);
            
            await command.ExecuteAsync(context, argPos, null);
        }
    }
}