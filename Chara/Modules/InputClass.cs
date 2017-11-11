using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Chara.Modules
{
    internal class InputClass
    {
        private User u;
        private HelperClass helper = new HelperClass();
        SocketCommandContext Context;
        String[] input;
        public InputClass()
        {

        }

        internal async Task runCommandAsync(User u, String v, SocketCommandContext Context)
        {
            this.u = u; this.Context = Context;
            int argPos = 0;
            if(Context.Message.ToString().ToLower().Equals("cancel"))
            {
                u.setCommand(null);
                helper.serialize(u, Context.User.Username);
                await Context.Channel.SendMessageAsync("Input cancelled.");
            }
            else if(Context.Message.HasCharPrefix('/', ref argPos))
            {
                await Context.Channel.SendMessageAsync("Do not use commands as an input.");
            }
            else if(v == "makeChar")
            {
                input = Context.Message.ToString().Split();
                if (input.Length != 1)
                    await Context.Channel.SendMessageAsync("Please input only a name.");
                else
                {
                    await Context.Channel.SendMessageAsync(u.addChar(input[0]));
                    u.setCommand(null);
                    helper.serialize(u, Context.User.Username);
                }
            } 
            else if(v == "makeStat")
            {
                input = Context.Message.ToString().Split();
                if (input.Length < 2 || input.Length > 3)
                    await Context.Channel.SendMessageAsync("Please input a stat name, a stat value, and an option stat modifier.");
                else
                {
                    if (input.Length == 2)
                        await Context.Channel.SendMessageAsync(u.addStat(u.getDefault().getName(), input[0], Convert.ToInt32(input[1])));
                    else await Context.Channel.SendMessageAsync(u.addStat(u.getDefault().getName(), input[0], Convert.ToInt32(input[1]), Convert.ToInt32(input[2])));
                    u.setCommand(null);
                    helper.serialize(u, Context.User.Username);
                }
            }
        }
    }
}