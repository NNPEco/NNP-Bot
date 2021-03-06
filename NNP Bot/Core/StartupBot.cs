﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Reflection;
using NNP_Bot.Services;

namespace NNP_Bot.Core
{
    public class StartupBot
    {
        public DiscordSocketClient _client;
        public CommandService _commands;
        public IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient(
                new DiscordSocketConfig{
                    LogLevel = LogSeverity.Verbose
                }
            );
            _commands = new CommandService(
                new CommandServiceConfig {
                    LogLevel = LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async,
            } 
            );
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton<AudioService>()
                .AddSingleton<Youtube_Api>()
                .BuildServiceProvider();
            
            string bottoken = "NTE4NzM4Mzg3NDA2OTQ2MzE5.DuVIdw.n9mfH1UU0C3-NtO_wi-G_zd__b4";

            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, bottoken);

            await _client.StartAsync();

           await Task.Delay(-1);
        }

        public Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandlecommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandlecommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                    Console.WriteLine("TTTTTTTTTTT");
                }
            }
        }
    }
}
