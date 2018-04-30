using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class Engineer : NPC, ISpeak, IModifyMap
    {
        public override int Id { get; set; }
        public override string Description { get; set; }
        public List<string> Messages { get; set; }
        public int[] _doorToUnlockCoords;

        public int[] DoorToUnlockCoords
        {
            get { return _doorToUnlockCoords; }
            set { _doorToUnlockCoords = value; }
        }

        public int[] getCoordsToUpdate()
        {
            if (DoorToUnlockCoords != null)
            {
                return DoorToUnlockCoords;
            }
            else
            {
                DoorToUnlockCoords = new int[2] { 1, 1 };
                return DoorToUnlockCoords;
            }
        }

        public string Speak()
        {
            if (Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return "You approach the survivor, but they have nothing to say...";
            }
        }

       

        private string GetMessage()
        {
            Random r = new Random();
            int messageIndex = r.Next(0, Messages.Count());
            return Messages[messageIndex];
        }
    }
}
