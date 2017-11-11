using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.IO;

namespace Chara.Commands
{
    [Group("char")]
    public class Char : ModuleBase<SocketCommandContext>
    {
        User u;
        String send;
        Modules.HelperClass helper = new Modules.HelperClass();
        [Command("makeChar")]
        public async Task makChar(string n)
        {
            u = helper.deserialize(Context.User.Username);
            send = u.addChar(n);
            helper.serialize(u, Context.User.Username);
            await Context.Channel.SendMessageAsync(send);
        }
        [Command("makeChar")]
        public async Task makChar()
        {
            u = helper.deserialize(Context.User.Username);
            u.setCommand("makeChar");
            helper.serialize(u, Context.User.Username);
            await Context.Channel.SendMessageAsync("Please input a name for your character.");
        }
        [Command("setDefault")]
        public async Task setDef(string n)
        {
            u = helper.deserialize(Context.User.Username);
            send = u.setDefault(n);
            helper.serialize(u, Context.User.Username);
            await Context.Channel.SendMessageAsync(send);
        }
        [Command("makeStat")]
        public async Task makStat(string cha, string s, int i, int m=-1000)
        {
            u = helper.deserialize(Context.User.Username);
            if (u.getChar(cha) != null)
            {
                send = u.getChar(cha).addStat(s, i, m);
                helper.serialize(u, Context.User.Username);
            }
            else
                send = "There is no character by the name of " + cha + ".";
            await Context.Channel.SendMessageAsync(send);
        }
        [Command("makeStat")]
        public async Task makStat()
        {
            u = helper.deserialize(Context.User.Username);
            if (u.getDefault() != null)
            {
                u.setCommand("makeStat");
                helper.serialize(u, Context.User.Username);
                await Context.Channel.SendMessageAsync("Please input a stat name, a stat value, and an optional stat modifier.");
            }
            else
                await Context.Channel.SendMessageAsync("You do not have a default character selected.");
        }
        [Command("getStat")]
        public async Task geStat(string s)
        {
            u = helper.deserialize(Context.User.Username);
            if (u.getDefault() == null)
                send = "You have no default character selected.";
            else if (u.getDefault().getStat(s) == null)
                send = "The character " + u.getDefault().getName() + " does not have stat " + s + ".";
            else send = u.getDefault().getStat(s).ToString();
            await Context.Channel.SendMessageAsync(send);
        }
    }
}
