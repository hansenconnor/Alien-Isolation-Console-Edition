using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public abstract class NPC : Character
    {
        public abstract int Id { get; set; } 
        public abstract string Description { get; set; }
        public bool _hasItemsToGive { get; set; }
        public string _icon;
        private int[] _coords;

        public bool HasItemsToGive
        {
            get { return _hasItemsToGive; }
            set { _hasItemsToGive = value; }
        }

        public int[] Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
}
