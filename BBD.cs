using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Threading.Tasks;
using System;

namespace DiscordBot_cs_bsic
{
    class BBD
    {
        static void Main(string[] args) => new BBD().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;


        public async Task RunBotAsync()
        {


            //create soket and command 
            client = new DiscordSocketClient();
            commands = new CommandService();

            //service collection swic
            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            //get Token in developers/applications
            string token = "ODU3MjM5Mjc3NzQ2NjUxMTg2.YNMsSg.9EZWRGR2GgpeiTB4i9zj7stWGm4";

            
            //create logMsg
            client.Log += clientLog;

            //Load command
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);

            //login token application
            await client.LoginAsync(TokenType.Bot, token);
            await client.SetGameAsync("base code bot discord with c sharp");

            // start bot
            await client.StartAsync();
            await Task.Delay(-1);
        }

       

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            //socket message user prefix
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(client, message);

            if (message.Author.IsBot)
                return;

            int argPos = 0;

            //prefix 
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition))
                    await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
        private Task clientLog(LogMessage arg)
        {
            //log arg
            Console.Title = ($"RUN CLIENT | {DateTime.Now.ToString("dd MM yyyy hh:mm")}");
            //ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

    }
}
