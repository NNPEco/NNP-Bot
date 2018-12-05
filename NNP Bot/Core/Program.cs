using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using NNP_Bot.Core;

namespace NNP_Bot
{
    class Program
    {
        
        private static void Main(string[] args) => new StartupBot().RunBotAsync().GetAwaiter().GetResult();
    }
}
