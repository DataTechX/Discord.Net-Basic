using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_cs_bsic.Commands
{
    //for commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class ExCommand : ModuleBase<SocketCommandContext>
    {
        
        [Command("test")]
        public async Task ExCommandAsync()
        {
            // get user info from the Context
            var msg = Context.User;

            // build embed
            var embed = new EmbedBuilder();

            embed.Color = new Color(0, 255, 35);
            embed.Title = "Test Commands";
            embed.Description = $"UserName **{msg}**";
            
            //send message to channel
            await ReplyAsync(null, false, embed.Build());
        }
    }
}
