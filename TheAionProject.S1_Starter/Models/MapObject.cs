using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class MapObject
    {
        public int _id;
        public string _name;
        public string _description;
        public string _icon;
        private int[] _coords;
        private MapObjectType _type;


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
        public int[] Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

        public MapObjectType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
