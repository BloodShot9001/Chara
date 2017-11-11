using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Chara
{
    [Serializable]
    public class User
    {
        String user;
        String command = null; //this is to "simulate commands" for use with input without args
        Dictionary<String, Character> characters = new Dictionary<string, Character>();
        Character defaultChar = null;

        public User(String user)
        {
            this.user = user;
        }
        public String setDefault(String s)
        {
            if (characters.ContainsKey(s))
            {
                defaultChar = characters[s];
                return "Your default character is set to " + s + ".";
            }
            return "You have not created a character by the name of " + s + ".";
        }
        public Character getChar(String s)
        {
            if (characters.ContainsKey(s))
                return characters[s];
            return null;
        }
        public Character getDefault() { return defaultChar; }
        public String addChar(String s)
        {
            if (characters.ContainsKey(s))
                return "A character by that name already exists.";
            characters.Add(s, new Character(s));
            return "Created character \"" + s + "\".";
        }
        public String addStat(String cha, String s, int i, int m = -1000)
        {
            return characters[cha].addStat(s, i, m);
        }
        public String getCommand() { return command; }
        public void setCommand(String s)
        {
            command = s;
        }
    }
}
