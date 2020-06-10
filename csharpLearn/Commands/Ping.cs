using System.Threading.Tasks;
using Discord;
using Discord.Commands;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace csharpLearn
{
    [Group("ping")]
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command]
        [Summary("Ping")]
        public async Task Pong()
        {
            var embed = new EmbedBuilder()
                .WithTitle("pong!")
                .WithDescription(Context.Client.Latency + "ms")
                .WithColor(0x6bedd4)
                .Build();
            
            await ReplyAsync("", embed: embed);
        }
    }
}