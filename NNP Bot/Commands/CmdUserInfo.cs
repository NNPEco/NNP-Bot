using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NNP_Bot.Commands
{
    public class CmdUserInfo : ModuleBase<SocketCommandContext>
    {
        [Command("저새끼정보")]
        public async Task userinfo([Remainder] SocketGuildUser user)
        {
            if (user.IsBot || user == null)
            {
                await ReplyAsync($"씨발년아 너 없는사람 만들지마 씨발 사람을 불러야지");
                return;
            }
            await Context.Message.DeleteAsync();
            string userroles = "";
            foreach (SocketRole role in ((SocketGuildUser)user).Roles)
            {
                userroles += role.Name + "\n";
            }
            var embed = new EmbedBuilder();
            embed.WithAuthor(user);
            embed.AddField("닉네임",$"{user.Username}#{user.Discriminator}", false);
            embed.AddField("소속그룹",$"{userroles}", false);
            embed.AddField("서버참여", $"{user.JoinedAt}");
            embed.WithColor(Color.Blue); 
            await Context.Channel.SendMessageAsync("",false,embed);
        }

    }
}
