using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chara
{
    [Serializable]
    public class Stat
    {
        private string name;
        private int stat;
        private int modifier;

        public Stat(string n, int s, int m = -1000)
        {
            name = n;
            stat = s;
            if (m == 1000)
                modifier = s;
            else modifier = m;
        }

        public int getMod(){return modifier;}
        public int getStat(){ return stat; }
        public string getName(){return name;}
        public void setMod(int m) { modifier = m; }
        public void setStat(int s) { stat = s; }
        public void setName(string s) { name = s; }
        public override String ToString()
        {
            return "The stat " + name + "'s value is " + stat + " and its modifier is " + modifier + ".";
        }
    }
}
