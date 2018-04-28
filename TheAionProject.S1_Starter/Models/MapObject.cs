using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class MapObject : GameObject
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override int RoomId { get; set; }
        public override string Icon { get; set; }

        private MapObjectType _type;

        public MapObjectType Type
        {
            get { return _type; }
            set { _type = value; }
        }


        private int[] _coords;

        public int[] Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

    }
}
