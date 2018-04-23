using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class NPC : Character
    {
        private bool _interactable;
        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }


        public bool Interactable
        {
            get { return _interactable; }
            set { _interactable = value; }
        }
    }
}
