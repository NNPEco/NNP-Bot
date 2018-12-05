using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NNP_Bot.Commands
{
    public class CmdHelp : ModuleBase<SocketCommandContext>
    {
        [Command("도움말", RunMode = RunMode.Async)]
        public async Task HelpAsync()
        {
            var embed = new EmbedBuilder();
            embed.AddField("!음악", "    -노래 들을수있음 ㅇㅇ", false);
            embed.AddField("!스킵", "    -듣고있는 노래 스킵할수있음", false);
            embed.AddField("!들어와", "  -니가 있는 음성쳇에 들어감", false);
            embed.AddField("!껒영", "    -함해봐 씨발", false);
            embed.WithAuthor(Context.User);
            embed.WithColor(Color.Blue);
            await Context.Channel.SendMessageAsync("",false,embed);
        }
    }
}
