using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace Chara
{
    public class CommandHandler
    {
        private DiscordSocketClient _discord;

        private CommandService _commands;

        private IServiceProvider _services;

        private Modules.HelperClass helper = new Modules.HelperClass();

        private Modules.InputClass input = new Modules.InputClass();

        private User u;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _discord = client;

            _commands = new CommandService();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            _discord.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
                return;

            var context = new SocketCommandContext(_discord, msg);
            u = helper.deserialize(context.User.Username);
            int argPos = 0;

            if(u.getCommand()!=null)
            {
                await input.runCommandAsync(u, u.getCommand(), context);
            }
            else if (msg.HasCharPrefix('/', ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos); 
                //pass in the dependencyMap, which lets you call the logic code in the commandHandler
                
                if(!result.IsSuccess  && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
