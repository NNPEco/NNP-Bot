using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using NNP_Bot.Module;
using NNP_Bot.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using EmergenceGuardian.FFmpeg;
using System.Threading.Tasks;

namespace NNP_Bot.Commands
{
    public class CmdMusic : ModuleBase<SocketCommandContext>
    {
        private readonly AudioService _service;

        public CmdMusic(AudioService service)
        {
            _service = service;
        }
        [Command("play", RunMode = RunMode.Async)]
        public async Task play(string url)
        {
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            await _service.JoinAudio(Context.Guild, channel);

            await _service.SendAudioAsync(Context.Guild, Context.Message.Channel, url);
        }
        [Command("음악")]
        public async Task MusicAsync(string url)
        {
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            await _service.JoinAudio(Context.Guild, channel);
            await _service.SendAudioAsync(Context.Guild, Context.Channel, url);
            await ReplyAsync("니가해 씨발");

        }
        [Command("스킵")]
        public async Task SkipAsync()
        {
            if((Context.User as IVoiceState).VoiceChannel == null)
            {
                await ReplyAsync("음성 채널에 들어가고 불러 씨발;;");
                return;
            }
            await ReplyAsync("니가해 씨발");
        }
        [Command("들어와")]
        public async Task JoinAsync()
        {
            await ReplyAsync($"{Context.User.Mention} 씨발뭐뭐뭐뭐무머멈머ㅓ 왜부름??? ㅅㅂ ㅈㄴ부르네 여긴또 뭔채널이야;; {(Context.User as IVoiceState).VoiceChannel.Name}?? 이딴곳에 부르지마라;; ㅈㄴ 귀찮네");
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
        }
        [Command("껒영")]
        public async Task LeaveAsync()
        {
            if ((Context.User as IVoiceState).VoiceChannel == null)
            {
                await ReplyAsync("음성 채널에 들어가고 불러 씨발;;");
                return;
            }
            await ReplyAsync($"{Context.User.Mention} 응 찌발ㅠㅠ.... 쨔짐...");
            await _service.LeaveAudio(Context.Guild);
        }
    }
}
