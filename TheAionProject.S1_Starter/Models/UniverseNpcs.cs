using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public static partial class UniverseNpcs
    {
        //
        // list of NPCs
        public static List<NPC> NPCs = new List<NPC>()
        {
            new Survivor()
            {
                Name = "Test Npc",
                Icon = "P",
                Messages = new List<string>{
                    "Hello!",
                    "Greetings!",
                    "Hello there!"
                }
            }
        };
    }
}
