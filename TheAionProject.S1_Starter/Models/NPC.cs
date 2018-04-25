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
        public string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
}
