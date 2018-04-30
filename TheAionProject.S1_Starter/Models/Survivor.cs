using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class Survivor : NPC, ISpeak, IGiveItem
    {
        public override int Id { get; set; }
        public override string Description { get; set; }
        public List<string> Messages { get; set; }

        public List<TravelerObject> _itemsToGive;

        public List<TravelerObject> ItemsToGive
        {
            get { return _itemsToGive; }
            set { _itemsToGive = value; }
        }

        public List<TravelerObject> GiveItems()
        {
            if (ItemsToGive != null)
            {
                return ItemsToGive;
            }
            else
            {
                return null;
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
